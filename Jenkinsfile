pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                // Clona el repositorio desde GitHub
                git branch: 'main', url: 'https://github.com/Melvin3107/AppFinanzas.git'
            }
        }

        stage('Restore Dependencies') {
            steps {
                script {
                    // Restaura los paquetes necesarios para todos los proyectos
                    sh 'dotnet restore AppFinanzas/Api/Gastos/Gastos.csproj'
                    sh 'dotnet restore AppFinanzas/Api/Usuarios/Usuarios.csproj'
                    sh 'dotnet restore AppFinanzas/frontend/frontend.csproj'
                }
            }
        }

        stage('Build Projects') {
            steps {
                script {
                    // Compila los proyectos
                    sh 'dotnet build AppFinanzas/Api/Gastos/Gastos.csproj --configuration Release'
                    sh 'dotnet build AppFinanzas/Api/Usuarios/Usuarios.csproj --configuration Release'
                    sh 'dotnet build AppFinanzas/frontend/frontend.csproj --configuration Release'
                }
            }
        }

        stage('Publish Projects') {
            steps {
                script {
                    // Publica los proyectos y guarda los binarios en una carpeta específica
                    sh 'dotnet publish AppFinanzas/Api/Gastos/Gastos.csproj --configuration Release --output ./publish/Gastos'
                    sh 'dotnet publish AppFinanzas/Api/Usuarios/Usuarios.csproj --configuration Release --output ./publish/Usuarios'
                    sh 'dotnet publish AppFinanzas/frontend/frontend.csproj --configuration Release --output ./publish/frontend'
                }
            }
        }

        stage('Generate Build Summary') {
            steps {
                script {
                    // Crea un archivo de resumen con la información de la compilación
                    writeFile file: 'build_summary.txt', text: """
                        Build Summary:
                        ========================
                        - Date: ${new Date()}
                        - Projects Compiled:
                          - AppFinanzas/Api/Gastos
                          - AppFinanzas/Api/Usuarios
                          - AppFinanzas/frontend
                        - Output Directory: ./publish
                        ========================
                    """
                }
            }
        }

        stage('Archive Artifacts') {
            steps {
                // Archiva los binarios generados y el archivo de resumen
                archiveArtifacts artifacts: 'publish/**/*, build_summary.txt', allowEmptyArchive: true
            }
        }
    }

    post {
        always {
            // Limpia el workspace después de la ejecución
            cleanWs()
        }
    }
}
