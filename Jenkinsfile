pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                // Clonar el repositorio
                git branch: 'main', url: 'https://github.com/Melvin3107/AppFinanzas.git'
            }
        }

        stage('Generate BOM File') {
            steps {
                script {
                    // Aquí puedes ejecutar comandos para generar el archivo bom.xml si es necesario.
                    // Por ejemplo, si estás utilizando un proyecto .NET y tienes un comando para generar el BOM:
                    sh 'dotnet tool install -g dotnet-bom'
                    sh 'dotnet bom generate --output /path/to/desired/location/bom.xml'
                }
            }
        }

        stage('Archive BOM File') {
            steps {
                // Archivar el archivo para que esté disponible para otros pipelines o tareas
                archiveArtifacts artifacts: '/path/to/desired/location/bom.xml', allowEmptyArchive: true
            }
        }
    }
}

