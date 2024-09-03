pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                // Clona el repositorio en el directorio de trabajo
                git branch: 'main', url: 'https://github.com/Melvin3107/AppFinanzas.git'
                
                // Verifica la estructura del directorio de trabajo
                sh 'ls -R'  // Esto lista todos los archivos y carpetas en el directorio de trabajo
            }
        }

        stage('Restore Dependencies') {
            steps {
                script {
                    echo 'Restoring dependencies for Api/Gastos...'
                    sh 'dotnet restore Api/Gastos/Gastos.csproj'
                    
                    echo 'Restoring dependencies for Api/Usuarios...'
                    sh 'dotnet restore Api/Usuarios/Usuarios.csproj'
                    
                    echo 'Restoring dependencies for frontend...'
                    sh 'dotnet restore frontend/frontend.csproj'
                }
            }
        }

        stage('Build') {
            steps {
                script {
                    echo 'Building the project for Api/Gastos...'
                    // Ajusta la ruta al archivo .csproj para la compilación y especifica el directorio de salida
                    sh 'dotnet build Api/Gastos/Gastos.csproj -c Release -o output/Gastos'
                    
                    echo 'Building the project for Api/Usuarios...'
                    // Ajusta la ruta al archivo .csproj para la compilación y especifica el directorio de salida
                    sh 'dotnet build Api/Usuarios/Usuarios.csproj -c Release -o output/Usuarios'
                    
                    echo 'Building the project for frontend...'
                    // Ajusta la ruta al archivo .csproj para la compilación y especifica el directorio de salida
                    sh 'dotnet build frontend/frontend.csproj -c Release -o output/frontend'
                }
            }
        }

        stage('Archive Artifacts') {
            steps {
                script {
                    echo 'Archiving build artifacts for Api/Gastos...'
                    // Ajusta la ruta a los archivos binarios generados por la compilación
                    archiveArtifacts artifacts: 'output/Gastos/*', allowEmptyArchive: true
                    
                    echo 'Archiving build artifacts for Api/Usuarios...'
                    // Ajusta la ruta a los archivos binarios generados por la compilación
                    archiveArtifacts artifacts: 'output/Usuarios/*', allowEmptyArchive: true
                    
                    echo 'Archiving build artifacts for frontend...'
                    // Ajusta la ruta a los archivos binarios generados por la compilación
                    archiveArtifacts artifacts: 'output/frontend/*', allowEmptyArchive: true
                }
            }
        }
    }
}
