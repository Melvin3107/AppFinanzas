pipeline {
    agent any

    environment {
        DOCKER_CLI_EXPERIMENTAL = 'enabled'
    }

    stages {
        stage('Checkout') {
            steps {
                // Clona el repositorio
                git branch: 'main', url: 'https://github.com/Melvin3107/AppFinanzas.git'
            }
        }

        stage('Build and Push Docker Images') {
            steps {
                script {
                    // Construye las imágenes Docker usando Docker Compose
                    sh 'docker-compose -f docker-compose.yml build'
                }
            }
        }

        stage('Run Tests') {
            steps {
                script {
                    // Levanta los servicios en segundo plano
                    sh 'docker-compose -f docker-compose.yml up -d'

                    // Espera a que los servicios estén listos (ajusta el tiempo según sea necesario)
                    sleep(time: 30, unit: 'SECONDS')

                    // Ejecuta las pruebas dentro del contenedor de pruebas
                    // Reemplaza 'test-service' con el nombre real de tu servicio de pruebas
                    sh 'docker-compose -f docker-compose.yml exec -T test-service dotnet test'

                    // Detiene y elimina los contenedores después de las pruebas
                    sh 'docker-compose -f docker-compose.yml down'
                }
            }
        }

        stage('Publish') {
            steps {
                script {
                    // Publica los servicios, si es necesario
                    sh 'docker-compose -f docker-compose.yml exec -T app-service dotnet publish --configuration Release --output /app/publish'
                }
            }
        }

        stage('Archive') {
            steps {
                // Archiva los artefactos de la compilación
                sh 'docker cp $(docker ps -q -f name=app-service):/app/publish ./publish'
                archiveArtifacts artifacts: 'publish/**', allowEmptyArchive: true
            }
        }

        stage('Cleanup') {
            steps {
                script {
                    // Limpia los servicios y las imágenes
                    sh 'docker-compose -f docker-compose.yml down'
                    sh 'docker system prune -af'
                }
            }
        }
    }

    post {
        always {
            // Limpieza después de la construcción
            cleanWs()
        }
    }
}





