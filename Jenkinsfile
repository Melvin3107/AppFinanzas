pipeline {
    agent any

    parameters {
        choice choices: ['Baseline', 'APIS', 'Full'], description: 'Tipo de escaneo', name: 'SCAN_TYPE'
        booleanParam(name: 'RUN_PROBAR_APLICACION', defaultValue: true, description: 'Probar aplicación')
        booleanParam(name: 'RUN_CORRER_PRUEBAS', defaultValue: true, description: 'Correr pruebas')
        booleanParam(name: 'RUN_ANALIZAR_CON_DTRACK', defaultValue: true, description: 'Analizar con Dependency-Track')
        booleanParam(name: 'GENERAR_INFORME_PDF', defaultValue: false, description: 'Generar informe de seguridad en PDF')
    }

    stages {
        stage('Mostrar Información de Parámetros') {
            steps {
                script {
                    echo 'Parámetros inicializados:'
                    echo """
                        Tipo de Escaneo: ${params.SCAN_TYPE}
                        Probar Aplicación: ${params.RUN_PROBAR_APLICACION}
                        Correr Pruebas: ${params.RUN_CORRER_PRUEBAS}
                        Analizar con Dependency-Track: ${params.RUN_ANALIZAR_CON_DTRACK}
                        Generar Informe PDF: ${params.GENERAR_INFORME_PDF}
                    """
                }
            }
        }

        stage('Construir y Levantar Contenedores') {
            steps {
                script {
                    echo 'Levantando servicios Docker Compose...'
                    sh 'docker-compose up -d'
                }
            }
        }

        stage('Escaneo con Dependency-Track') {
            when {
                expression { params.RUN_ANALIZAR_CON_DTRACK }
            }
            steps {
                script {
                    echo 'Iniciando escaneo con Dependency-Track...'
                    sh '''
                        curl -X POST \
                          -H "Content-Type: application/xml" \
                          -H "Authorization: Bearer odt_ynam8X200cm1LaVUoSqdCCaS3BBhaHn9" \
                          --data-binary @/app/bom.xml \
                          http://localhost:8080/api/v1/bom
                    '''
                }
            }
        }

        stage('Generar PDF con Pandoc') {
            when {
                expression { params.GENERAR_INFORME_PDF }
            }
            steps {
                script {
                    echo 'Generando PDF con Pandoc...'
                    sh '''
                        docker-compose run --rm pandoc pandoc /app/docs/README.md -o /app/docs/README.pdf
                    '''
                    sh 'cp /app/docs/README.pdf ${WORKSPACE}/README.pdf'
                }
            }
        }
    }

    post {
        always {
            echo 'Deteniendo y eliminando servicios Docker Compose...'
            sh 'docker-compose down'
            cleanWs()
        }
    }
}
