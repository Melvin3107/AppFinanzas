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
        stage('Build with Docker Compose') {
            steps {
                script {
                    // Ejecutar docker-compose para construir los servicios
                    sh 'docker-compose build'
                }
            }
        }
        stage('Run Tests and Publish') {
            steps {
                script {
                    // Ejecutar docker-compose para levantar los servicios
                    sh 'docker-compose up -d'

                    // Ejecutar las pruebas en los contenedores
                    sh 'docker-compose exec -T <service_name> dotnet test --configuration Release'

                    // Publicar los artefactos desde el contenedor
                    sh 'docker-compose exec -T <service_name> dotnet publish --configuration Release --output /app/publish'
                    
                    // Copiar el archivo publicado desde el contenedor al sistema de archivos del host
                    sh 'docker cp <container_id>:/app/publish ./publish'
                }
            }
        }
    }
    post {
        success {
            echo 'Pipeline completado con éxito.'
            archiveArtifacts artifacts: 'publish/**/*', allowEmptyArchive: true
        }
        failure {
            echo 'Pipeline falló.'
        }
    }
}



