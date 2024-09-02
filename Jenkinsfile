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
                sh 'dotnet restore'
            }
        }
        stage('Build') {
            steps {
                // Construir la aplicación
                sh 'dotnet build --configuration Release'
            }
        }
        stage('Test') {
            steps {
                // Ejecutar pruebas
                sh 'dotnet test --configuration Release'
            }
        }
        stage('Publish') {
            steps {
                // Publicar la aplicación
                sh 'dotnet publish --configuration Release --output ./publish'
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
