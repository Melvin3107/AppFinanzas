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
                    dir('AppFinanzas') {
                        // Compila los proyectos .NET
                        sh 'dotnet build -c Release Api/Usuarios/Usuarios.csproj'
                        sh 'dotnet build -c Release Api/Gastos/Gastos.csproj'
                        sh 'dotnet build -c Release frontend/frontend.csproj'
                    }
                }
            }
        }
        stage('Generate BOM') {
            steps {
                script {
                    // Si ya tienes el archivo BOM, este paso puede ser omitido
                    // Aquí puedes añadir pasos para generar el archivo BOM si es necesario
                }
            }
        }
    }

    post {
        always {
            archiveArtifacts artifacts: '**/bin/Release/**', allowEmptyArchive: true
        }
    }
}
