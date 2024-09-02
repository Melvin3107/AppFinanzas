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
                // Restaurar paquetes para todos los microservicios
                dir('Api/Usuarios') {
                    sh 'dotnet restore'
                }
                dir('Api/Gastos') {
                    sh 'dotnet restore'
                }
                dir('frontend') {
                    sh 'dotnet restore'
                }
            }
        }
        stage('Build') {
            steps {
                // Construir todos los microservicios
                dir('Api/Usuarios') {
                    sh 'dotnet build --configuration Release'
                }
                dir('Api/Gastos') {
                    sh 'dotnet build --configuration Release'
                }
                dir('frontend') {
                    sh 'dotnet build --configuration Release'
                }
            }
        }
        stage('Test') {
            steps {
                // Ejecutar pruebas para todos los microservicios
                dir('Api/Usuarios') {
                    sh 'dotnet test --configuration Release'
                }
                dir('Api/Gastos') {
                    sh 'dotnet test --configuration Release'
                }
                dir('frontend') {
                    sh 'dotnet test --configuration Release'
                }
            }
        }
        stage('Publish') {
            steps {
                // Publicar todos los microservicios
                dir('Api/Usuarios') {
                    sh 'dotnet publish --configuration Release --output ./publish/Usuarios'
                }
                dir('Api/Gastos') {
                    sh 'dotnet publish --configuration Release --output ./publish/Gastos'
                }
                dir('frontend') {
                    sh 'dotnet publish --configuration Release --output ./publish/frontend'
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


