pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                // Clona el repositorio desde GitHub
                git 'https://github.com/Melvin3107/AppFinanzas.git'
            }
        }
        stage('Build') {
            steps {
                script {
                    // Cambia al directorio principal de tu aplicación
                    dir('AppFinanzas') {
                        // Compila los proyectos .NET en Release
                        sh 'dotnet build -c Release Api/Usuarios/Usuarios.csproj'
                        sh 'dotnet build -c Release Api/Gastos/Gastos.csproj'
                        sh 'dotnet build -c Release frontend/frontend.csproj'
                    }
                }
            }
        }
    }

    post {
        always {
            // Archiva los artefactos de compilación
            archiveArtifacts artifacts: '**/bin/Release/**', allowEmptyArchive: true
        }
    }
}
