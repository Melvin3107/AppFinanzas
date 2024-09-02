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
                        sh 'dotnet build -c Release'
                    }
                }
            }
        }

        stage('Generate BOM') {
            steps {
                script {
                    // Generate BOM file
                    dir('AppFinanzas') {
                        sh 'dotnet tool restore'
                        sh 'dotnet tool run dependency-check --project "AppFinanzas" --out ${env.BOM_FILE}' // Ajusta el comando según sea necesario
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
