pipeline {
  agent any
  stages {
    stage('Get Code From SCM') {
      steps {
        git(url: 'https://github.com/kmoparthyHu/CalculatorApp.git', branch: 'master', credentialsId: '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', poll: true)
      }
    }

    stage('Restore Packages') {
      steps {
        bat 'dotnet restore CalculatorApp.sln'
      }
    }

    stage('Clean the build') {
      steps {
        bat 'msbuild.exe ${workspace}\\\\CalculatorApp\\\\CalculatorApp.sln" /nologo /nr:false /p:platform=\\"x64\\" /p:configuration=\\"release\\" /t:clean'
      }
    }

    stage('Build') {
      steps {
        bat 'msbuild.exe ${workspace}\\\\CalculatorApp\\\\CalculatorApp.sln /nologo /nr:false  /p:platform=\\"x64\\" /p:configuration=\\"release\\" /p:PackageCertificateKeyFile=<path-to-certificate-file>.pfx /t:clean;restore;rebuild'
      }
    }

  }
}