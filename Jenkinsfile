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
                        docker run --rm \
                        -e DTRACK_URL=http://localhost:8061 \
                        -e DTRACK_API_KEY=odt_dUKx4LoQ2LKbJEyvqdQuuTSHqvDCWwRy \
                        -v ${WORKSPACE}/bom.xml:/app/bom.xml \
                        dependencytrack/bundled:latest \
                        -url $DTRACK_URL \
                        -apiKey $DTRACK_API_KEY \
                        -project "AppFinanzas" \
                        -version "1.0.0" \
                        -bom /app/bom.xml
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
