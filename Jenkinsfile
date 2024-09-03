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
                    // Ruta relativa a la raíz del repositorio
                    sh 'dotnet restore AppFinanzas/Api/Gastos/Gastos.csproj'
                    
                    echo 'Restoring dependencies for Api/Usuarios...'
                    // Ruta relativa a la raíz del repositorio
                    sh 'dotnet restore AppFinanzas/Api/Usuarios/Usuarios.csproj'
                    
                    echo 'Restoring dependencies for frontend...'
                    // Ruta relativa a la raíz del repositorio
                    sh 'dotnet restore AppFinanzas/frontend/frontend.csproj'
                }
            }
        }

        stage('Build') {
            steps {
                script {
                    echo 'Building the project for Api/Gastos...'
                    // Ajusta la ruta al archivo .csproj para la compilación
                    sh 'dotnet build AppFinanzas/Api/Gastos/Gastos.csproj -c Release'
                    
                    echo 'Building the project for Api/Usuarios...'
                    // Ajusta la ruta al archivo .csproj para la compilación
                    sh 'dotnet build AppFinanzas/Api/Usuarios/Usuarios.csproj -c Release'
                    
                    echo 'Building the project for frontend...'
                    // Ajusta la ruta al archivo .csproj para la compilación
                    sh 'dotnet build AppFinanzas/frontend/frontend.csproj -c Release'
                }
            }
        }

        stage('Archive Artifacts') {
            steps {
                script {
                    echo 'Archiving build artifacts for Api/Gastos...'
                    // Ajusta la ruta a los archivos binarios generados por la compilación
                    archiveArtifacts artifacts: 'AppFinanzas/Api/Gastos/bin/Release/net8.0/*', allowEmptyArchive: true
                    
                    echo 'Archiving build artifacts for Api/Usuarios...'
                    // Ajusta la ruta a los archivos binarios generados por la compilación
                    archiveArtifacts artifacts: 'AppFinanzas/Api/Usuarios/bin/Release/net8.0/*', allowEmptyArchive: true
                    
                    echo 'Archiving build artifacts for frontend...'
                    // Ajusta la ruta a los archivos binarios generados por la compilación
                    archiveArtifacts artifacts: 'AppFinanzas/frontend/bin/Release/net8.0/*', allowEmptyArchive: true
                }
            }
        }
    }
}
