pipeline {
    agent any

stages
{

	stage ('Clean workspace') {
	  steps {
		cleanWs()
	  }
	}

	stage ('Git Checkout') {
	  steps {
		  git branch: 'master', credentialsId: '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', url: 'https://github.com/kmoparthyHu/CalculatorApp.git'
		}
	  }

	stage('Restore packages') {
	  steps {
		bat "dotnet restore ${workspace}\\CalculatorApp\\CalculatorApp.sln"
	  }
	}

	stage('Clean') {
	  steps {
		bat "msbuild.exe ${workspace}\\CalculatorApp\\CalculatorApp.sln" /nologo /nr:false /p:platform=\"x64\" /p:configuration=\"release\" /t:clean"
	  }
	}

	stage('Increase version') {
		steps {
			echo "${env.BUILD_NUMBER}"
			powershell '''
			   $xmlFileName = "CalculatorApp\\Package.appxmanifest"     
			   [xml]$xmlDoc = Get-Content $xmlFileName
			   $version = $xmlDoc.Package.Identity.Version
			   $trimmedVersion = $version -replace '.[0-9]+$', '.'
			   $xmlDoc.Package.Identity.Version = $trimmedVersion + ${env:BUILD_NUMBER}
			   echo 'New version:' $xmlDoc.Package.Identity.Version
			   $xmlDoc.Save($xmlFileName)
			'''
		 }
	 }

	stage('Build') {
	 steps {
	  bat "msbuild.exe ${workspace}\\CalculatorApp\\CalculatorApp.sln /nologo /nr:false  /p:platform=\"x64\" /p:configuration=\"release\" /p:PackageCertificateKeyFile=<path-to-certificate-file>.pfx /t:clean;restore;rebuild"
	 }
	}

	stage('Running unit tests') {
	  steps {
		bat "dotnet add ${workspace}/CalculatorApp/CalculatorApp.Services.Tests/CalculatorApp.Services.Tests.csproj package JUnitTestLogger --version 1.1.0"
		bat "dotnet test ${workspace}/CalculatorApp/CalculatorApp.Services.Tests/CalculatorApp.Services.Tests.csproj --logger \"junit;LogFilePath=\"${WORKSPACE}\"/TestResults/1.0.0.\"${env.BUILD_NUMBER}\"/results.xml\" --configuration release --collect \"Code coverage\""
		powershell '''
		  $destinationFolder = \"$env:WORKSPACE/TestResults\"
		  if (!(Test-Path -path $destinationFolder)) {New-Item $destinationFolder -Type Directory}
		  $file = Get-ChildItem -Path \"$env:WORKSPACE/CalculatorApp/CalculatorApp.Services.Tests/TestResults/*/*.coverage\"
		  $file | Rename-Item -NewName testcoverage.coverage
		  $renamedFile = Get-ChildItem -Path \"$env:WORKSPACE/CalculatorApp/CalculatorApp.Services.Tests/TestResults/*/*.coverage\"
		  Copy-Item $renamedFile -Destination $destinationFolder
		'''            
	  }        
	}

	stage('Convert coverage file to xml coverage file') {
	  steps {
		bat "C:\\Users\\Administrator\\.dotnet\\tools\\CodeCoverage.exe analyze  /output:${WORKSPACE}\\TestResults\\xmlresults.coveragexml  ${WORKSPACE}\\TestResults\\testcoverage.coverage"
	  }
	}

	stage('Generate report') {
	  steps {
		  bat "C:\\Users\\Administrator\\.dotnet\\tools\\ReportGenerator.exe -reports:${WORKSPACE}\\TestResults\\xmlresults.coveragexml -targetdir:${WORKSPACE}\\CodeCoverage_${env.BUILD_NUMBER}
	  }
	}

	stage('Publish HTML report') {
	  steps {
		  publishHTML(target: [allowMissing: false, alwaysLinkToLastBuild: false, keepAll: false, reportDir: 'CodeCoverage_${BUILD_NUMBER}', reportFiles: 'index.html', reportName: 'HTML Report', reportTitles: 'Code Coverage Report'])
	  }
	}
}
post {
  always {
    archiveArtifacts artifacts: '**/*.msix', followSymlinks: false
    junit "TestResults/1.0.0.${env.BUILD_NUMBER}/results.xml"
  }
}
}