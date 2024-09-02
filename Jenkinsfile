pipeline {
    agent any

    environment {
        REPO_URL = 'https://github.com/Melvin3107/AppFinanzas.git'
        BOM_FILE = 'bom.xml'
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: "${env.REPO_URL}"
            }
        }

        stage('Build') {
            steps {
                script {
                    // Build the project
                    dir('AppFinanzas') {
                        bat 'dotnet build -c Release' // Use 'sh' for Unix-based systems
                    }
                }
            }
        }

        stage('Generate BOM') {
            steps {
                script {
                    // Generate BOM file
                    dir('AppFinanzas') {
                        bat 'dotnet tool restore'
                        bat 'dotnet tool run dependency-check --project "AppFinanzas" --out ${env.BOM_FILE}' // Adjust command as needed
                    }
                }
            }
        }
    }
    
    post {
        always {
            archiveArtifacts artifacts: "${env.BOM_FILE}", allowEmptyArchive: true
        }
    }
}
