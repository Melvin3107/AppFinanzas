pipeline {
    agent any

    parameters {
        choice choices: ['Baseline', 'APIS', 'Full'], description: 'Type of scan that is going to perform inside the container', name: 'SCAN_TYPE'
        
        booleanParam(name: 'RUN_PROBAR_APLICACION', defaultValue: true, description: 'Probar aplicación')
        booleanParam(name: 'RUN_CORRER_PRUEBAS', defaultValue: true, description: 'Correr pruebas')
        booleanParam(name: 'RUN_ANALIZAR_CON_DTRACK', defaultValue: true, description: 'Analizar con Dependency-Track')
        
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
                            Run Analizar con Dependency-Track: ${params.RUN_ANALIZAR_CON_DTRACK}
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

                
        stage('Dependency-Track Scan') {
            when {
                expression { params.RUN_ANALIZAR_CON_DTRACK }
            }
            steps {
                script {
                    echo 'Running Dependency-Track analysis...'
                    sh """

                    """
                }
            }
        }

        stage('Generate PDF with Pandoc') {
            when {
                expression { params.GENERAR_INFORME_PDF }
            }
            steps {
                script {
                    echo 'Generating PDF with Pandoc...'
                    sh '''
                        docker-compose run --rm pandoc pandoc /app/docs/README.md -o /app/docs/README.pdf
                    '''
                    sh '''
                        cp /app/docs/README.pdf ${WORKSPACE}/README.pdf
                    '''
                }
            }
        }
        
        stage('Email Report') {
            when {
                expression { params.GENERAR_INFORME_PDF }
            }
            steps {
                emailext (
                    attachLog: true,
                    attachmentsPattern: '**/*.pdf',
                    body: "Please find the attached PDF report for the latest Dependency-Track Scan.",
                    recipientProviders: [buildUser()],
                    subject: "Dependency-Track PDF Report",
                    to: 'mtorresg@miumg.edu.gt'
                )
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