pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                // Clona el repositorio desde GitHub
                git url: 'https://github.com/Melvin3107/AppFinanzas.git', branch: 'main'
            }
        }

        stage('Set Permissions') {
            steps {
                script {
                    // Otorga permisos de lectura y escritura a los archivos y directorios necesarios
                    sh 'chmod -R u+rwX AppFinanzas'
                }
            }
        }

        stage('Verify Directory') {
            steps {
                script {
                    // Verifica el directorio actual y la estructura de archivos
                    sh 'echo "Current directory:"'
                    sh 'pwd'
                    sh 'echo "Directory structure:"'
                    sh 'ls -R'
                }
            }
        }

        stage('Check .NET SDK') {
            steps {
                // Verifica la versión del .NET SDK
                sh 'dotnet --version'
            }
        }

        stage('Build') {
            steps {
                script {
                    // Cambia al directorio principal de la aplicación
                    dir('AppFinanzas') {
                        // Verifica la estructura de directorios en AppFinanzas
                        sh 'echo "Directory structure in AppFinanzas:"'
                        sh 'ls -R'

                        // Compila los proyectos .NET
                        sh 'dotnet build -c Release Api/Gastos/Gastos.csproj'
                        sh 'dotnet build -c Release Api/Usuarios/Usuarios.csproj'
                        sh 'dotnet build -c Release frontend/frontend.csproj'
                    }
                }
            }
        }

        stage('Test') {
            steps {
                script {
                    // Ejecuta pruebas unitarias si tienes pruebas configuradas
                    dir('AppFinanzas') {
                        sh 'dotnet test Api/Gastos/Gastos.csproj'
                        sh 'dotnet test Api/Usuarios/Usuarios.csproj'
                        sh 'dotnet test frontend/frontend.csproj'
                    }
                }
            }
        }

        stage('Publish') {
            steps {
                script {
                    // Publica los proyectos .NET si es necesario
                    dir('AppFinanzas') {
                        sh 'dotnet publish -c Release Api/Gastos/Gastos.csproj -o ./publish/Gastos'
                        sh 'dotnet publish -c Release Api/Usuarios/Usuarios.csproj -o ./publish/Usuarios'
                        sh 'dotnet publish -c Release frontend/frontend.csproj -o ./publish/frontend'
                    }
                }
            }
        }
    }
    
    post {
        always {
            // Acciones después de la construcción, como archivar artefactos
            archiveArtifacts artifacts: 'AppFinanzas/**/bin/Release/**'
            junit '**/test-results/*.xml'
        }
    }
}

