pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                git 'https://github.com/Melvin3107/AppFinanzas.git'
            }
        }
        stage('Build') {
            steps {
                script {
                    dir('AppFinanzas') {
                        // Compila los proyectos .NET
                        sh 'dotnet build -c Release Api/Usuarios/Usuarios.csproj'
                        sh 'dotnet build -c Release Api/Gastos/Gastos.csproj'
                        sh 'dotnet build -c Release frontend/frontend.csproj'
                    }
                }
            }
        }
        stage('Generate BOM') {
            steps {
                script {
                    dir('AppFinanzas') {
                        // Asegúrate de que CycloneDX esté instalado y configurado
                        // Genera el BOM usando CycloneDX para todos los proyectos .NET
                        sh '''
                        # Genera el BOM en formato XML para todos los proyectos .NET
                        dotnet cyclonedx --output bom.xml
                        '''
                    }
                }
            }
        }
    }
    
    post {
        always {
            // Archiva los artefactos de compilación
            archiveArtifacts artifacts: '**/bin/Release/**', allowEmptyArchive: true
            // Archiva el BOM generado
            archiveArtifacts artifacts: 'AppFinanzas/bom.xml', allowEmptyArchive: true
        }
    }
}
