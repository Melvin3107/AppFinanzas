pipeline {
    agent any

    parameters {
        choice choices: ['Baseline', 'APIS', 'Full'], description: 'Type of scan that is going to perform inside the container', name: 'SCAN_TYPE'
        booleanParam(name: 'RUN_PROBAR_APLICACION', defaultValue: true, description: 'Probar aplicación')
        booleanParam(name: 'RUN_CORRER_PRUEBAS', defaultValue: true, description: 'Correr pruebas')
        booleanParam(name: 'RUN_ANALIZAR_CON_DTRACK', defaultValue: true, description: 'Analizar con Dependency-Track')
        booleanParam(name: 'GENERATE_REPORT', defaultValue: true, description: 'Parameter to know if wanna generate report.')
        booleanParam(name: 'GENERAR_INFORME_PDF', defaultValue: false, description: 'Generar informe de seguridad en PDF')
        string(name: 'DTRACK_URL', defaultValue: 'http://localhost:8061', description: 'URL del servidor Dependency-Track')
        string(name: 'DTRACK_API_KEY', defaultValue: '', description: 'API Key para Dependency-Track')
        string(name: 'PROJECT_NAME', defaultValue: 'my-project', description: 'Nombre del proyecto')
        string(name: 'VERSION', defaultValue: '1.0.0', description: 'Versión del proyecto')
        string(name: 'BOM_FILE', defaultValue: 'bom.xml', description: 'Nombre del archivo BOM (Bill of Materials)')
    }

    stages {
        stage('Pipeline Info') {
            steps {
                script {
                    echo '<--Parameter Initialization-->'
                    echo """
                        Scan Type: ${params.SCAN_TYPE}
                        Generate Report: ${params.GENERATE_REPORT}
                        Generate PDF Report: ${params.GENERAR_INFORME_PDF}
                        Run Probar Aplicación: ${params.RUN_PROBAR_APLICACION}
                        Run Correr Pruebas: ${params.RUN_CORRER_PRUEBAS}
                        Run Analizar con Dependency-Track: ${params.RUN_ANALIZAR_CON_DTRACK}
                        Dependency-Track URL: ${params.DTRACK_URL}
                        Dependency-Track API Key: ${params.DTRACK_API_KEY}
                        Project Name: ${params.PROJECT_NAME}
                        Version: ${params.VERSION}
                        BOM File: ${params.BOM_FILE}
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
                    echo 'Starting Dependency-Track scan...'

                    // Espera un tiempo para que Dependency-Track esté completamente operativo
                    sleep(time: 30, unit: 'SECONDS')

                    // Verifica que Dependency-Track esté accesible
                    def statusCode = sh(script: "curl --write-out '%{http_code}' --silent --output /dev/null ${params.DTRACK_URL}/api/v1/ping", returnStdout: true).trim()
            
                    if (statusCode != '200') {
                        error "Dependency-Track is not accessible. HTTP Status Code: ${statusCode}"
                    }

                    // Ejecuta el comando de análisis usando Dependency-Track
                    sh '''
                        docker run --rm \
                        -e DTRACK_URL=${params.DTRACK_URL} \
                        -e DTRACK_API_KEY=${params.DTRACK_API_KEY} \
                        -e PROJECT_NAME=${params.PROJECT_NAME} \
                        -e VERSION=${params.VERSION} \
                        -v ${WORKSPACE}/${params.BOM_FILE}:/app/bom.xml \
                        dependencytrack/cli:latest \
                        -url $DTRACK_URL \
                        -apiKey $DTRACK_API_KEY \
                        -project $PROJECT_NAME \
                        -version $VERSION \
                        -bom /app/bom.xml
                    '''
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
