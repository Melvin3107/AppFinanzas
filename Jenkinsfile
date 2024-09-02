pipeline {
    agent any
    
    stages {
        stage('Checkout') {
            steps {
                // Clona el repositorio de Git
                git 'https://github.com/Melvin3107/AppFinanzas.git'
            }
        }
        stage('Build') {
            steps {
                script {
                    // Cambia al directorio raíz del proyecto
                    dir('AppFinanzas') {
                        // Compila los proyectos .NET
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
            // Archiva los artefactos construidos, ajusta los patrones de acuerdo a tus necesidades
            archiveArtifacts artifacts: '**/bin/Release/**/*', allowEmptyArchive: true
        }
    }
}
