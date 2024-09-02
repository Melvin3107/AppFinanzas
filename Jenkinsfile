pipeline {
    agent any

    parameters {
        choice choices: ['Baseline', 'APIS', 'Full'], description: 'Type of scan that is going to perform inside the container', name: 'SCAN_TYPE'
        
        booleanParam(name: 'RUN_PROBAR_APLICACION', defaultValue: true, description: 'Probar aplicación')
        booleanParam(name: 'RUN_CORRER_PRUEBAS', defaultValue: true, description: 'Correr pruebas')
        booleanParam(name: 'GENERATE_REPORT', defaultValue: true, description: 'Parameter to know if wanna generate report.')
        booleanParam(name: 'GENERAR_INFORME_PDF', defaultValue: false, description: 'Generar informe de seguridad en PDF')
    }

    stages {
        stage('Pipeline Info') {
            steps {
                script {
                    echo '<--Parameter Initialization-->'
                    echo """
                        The current parameters are:
                            Scan Type: ${params.SCAN_TYPE}
                            Generate Report: ${params.GENERATE_REPORT}
                            Generate PDF Report: ${params.GENERAR_INFORME_PDF}
                            Run Probar Aplicación: ${params.RUN_PROBAR_APLICACION}
                            Run Correr Pruebas: ${params.RUN_CORRER_PRUEBAS}
                        """
                }
            }
        }

        stage('Build and Start Containers') {
            steps {
                script {
                    echo 'Building and starting Docker Compose services...'
                    sh 'docker-compose up -d'  // Levanta todos los contenedores en segundo plano
                }
            }
        }

        stage('Generate Report') {
            when {
                expression { params.GENERATE_REPORT }
            }
            steps {
                script {
                    echo 'Generating report...'
                    sh 'echo "This is a sample report" > report.txt'
                }
            }
        }

        stage('Archive Artifacts') {
            steps {
                script {
                    echo 'Archiving generated files...'
                    archiveArtifacts artifacts: 'report.txt', allowEmptyArchive: true
                }
            }
        }
    }

    post {
        always {
            echo 'Stopping and removing Docker Compose services...'
            sh 'docker-compose down'  // Detiene y elimina todos los contenedores, redes y volúmenes
            cleanWs()
        }
    }
}
