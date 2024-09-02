pipeline {
    agent any
    stages {
        stage('Checkout') {
            steps {
                git 'https://github.com/Melvin3107/AppFinanzas.git'
            }
        }
        stage('Build') {
            steps {
                script {
                    dir('AppFinanzas/frontend') {
                        sh 'dotnet build -c Release'
                    }
                }
            }
        }
        stage('Generate BOM') {
            steps {
                // Aquí puedes añadir pasos para generar el archivo BOM
            }
        }
    }
    post {
        always {
            archiveArtifacts artifacts: '**/bin/Release/**', allowEmptyArchive: true
        }
    }
}
