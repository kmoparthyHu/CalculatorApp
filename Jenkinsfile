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
        bat 'dotnet clean'
      }
    }

    stage('SonarScanner Begin') {
      steps {
        bat 'dotnet C:\\\\Users\\\\Administrator\\\\.dotnet\\\\tools\\\\.store\\\\dotnet-sonarscanner\\\\5.11.0\\\\dotnet-sonarscanner\\\\5.11.0\\\\tools\\\\netcoreapp3.0\\\\any begin /k:"CalculatorApp" /d:sonar.login="sqp_58169d019975d4cff2023eef1b887b009eda4600"'
      }
    }

    stage('Build the code') {
      steps {
        bat 'dotnet build'
      }
    }

    stage('SonarScanner End') {
      steps {
        bat 'dotnet sonarscanner end /d:sonar.login="sqp_5f07446323d38f17c77b516c29553923beb8acb9"'
      }
    }

  }
}