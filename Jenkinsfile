pipeline {
    agent any
    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'  // Desactiva la telemetría de .NET CLI
        DOTNET_NOLOGO = 'true'             // Desactiva el logotipo de .NET CLI
    }
    stages {
        stage('Checkout') {
            steps {
                // Clonar el repositorio
                checkout scm
            }
        }
        stage('Restore') {
            steps {
                // Restaurar paquetes
                script {
                    bat 'dotnet restore'
                }
            }
        }
        stage('Build') {
            steps {
                // Construir la aplicación
                script {
                    bat 'dotnet build --configuration Release'
                }
            }
        }
        stage('Test') {
            steps {
                // Ejecutar pruebas
                script {
                    bat 'dotnet test --configuration Release'
                }
            }
        }
        stage('Publish') {
            steps {
                // Publicar la aplicación
                script {
                    bat 'dotnet publish --configuration Release --output ./publish'
                }
            }
        }
    }
    post {
        success {
            echo 'Pipeline completado con éxito.'
        }
        failure {
            echo 'Pipeline falló.'
        }
    }
}
