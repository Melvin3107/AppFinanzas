pipeline {
    agent any
    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_NOLOGO = 'true'
    }
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        stage('Build with Docker Compose') {
            steps {
                script {
                    sh 'docker-compose --version'
                    sh 'docker-compose build'
                    sh 'docker-compose up -d'
                }
            }
        }
        stage('Run Tests and Publish') {
            steps {
                script {
                    sh 'docker-compose exec -T <service_name> dotnet test --configuration Release'
                    sh 'docker-compose exec -T <service_name> dotnet publish --configuration Release --output /app/publish'
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




