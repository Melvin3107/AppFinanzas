pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                // Clona el repositorio desde GitHub
                git url: 'https://github.com/Melvin3107/AppFinanzas.git', branch: 'main'
            }
        }
        stage('Build') {
            steps {
                // Restaura los paquetes NuGet y compila el proyecto
                sh 'dotnet restore'
                sh 'dotnet build --configuration Release'
            }
        }
        stage('Archive') {
            steps {
                // Archiva los archivos generados
                archiveArtifacts artifacts: '**/bin/Release/netcoreapp*/publish/*', allowEmptyArchive: true
            }
        }
    }
}