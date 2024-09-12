pipeline {
    agent any

    environment {
        // Variables de entorno si es necesario
        REPO_URL = 'https://github.com/Melvin3107/AppFinanzas.git'
        ILSPY_REPO = 'https://github.com/icsharpcode/ILSpy.git'
        OBFSCR_REPO = 'https://github.com/obfuscar/obfuscar.git'
        WORKSPACE_DIR = '/workspace'
        DLL_FILE = 'AppFinanzas.dll'
    }

    stages {
        stage('Checkout') {
            steps {
                // Clona el repositorio de código
                git url: "${REPO_URL}", branch: 'main'
                
                // Clona los repositorios de ILSpy y Obfuscar
                sh "git clone ${ILSPY_REPO} ${WORKSPACE_DIR}/ILSpy"
                sh "git clone ${OBFSCR_REPO} ${WORKSPACE_DIR}/Obfuscar"
            }
        }

        stage('Build') {
            steps {
                dir("${WORKSPACE_DIR}/AppFinanzas") {
                    // Compila el código
                    sh 'dotnet build -c Release'
                }
            }
        }

        stage('Decompile with ILSpy') {
            steps {
                dir("${WORKSPACE_DIR}/ILSpy") {
                    // Descompilar el .dll usando ILSpy
                    sh 'dotnet build -c Release'
                    // Aquí puedes necesitar un comando específico de ILSpy si tiene uno para CLI.
                    // Por ejemplo:
                    sh 'dotnet run --project ILSpy/ILSpy.csproj -- /file:AppFinanzas.dll --output:decompiled'
                }
            }
        }

        stage('Obfuscate with Obfuscar') {
            steps {
                dir("${WORKSPACE_DIR}/Obfuscar") {
                    // Construye Obfuscar
                    sh 'dotnet build'

                    // Crea un archivo de configuración para Obfuscar
                    writeFile file: 'Obfuscar.xml', text: '''
                    <Obfuscator>
                        <Assembly file="${WORKSPACE_DIR}/AppFinanzas/bin/Release/net8.0/AppFinanzas.dll">
                            <Module>
                                <Rename />
                            </Module>
                        </Assembly>
                    </Obfuscator>
                    '''

                    // Ejecuta Obfuscar
                    sh 'dotnet run --project src/Obfuscar/Obfuscar.csproj -- /config:Obfuscar.xml'
                }
            }
        }

        stage('Decompile Obfuscated DLL with ILSpy') {
            steps {
                dir("${WORKSPACE_DIR}/ILSpy") {
                    // Descompilar el .dll ofuscado usando ILSpy
                    sh 'dotnet run --project ILSpy/ILSpy.csproj -- /file:AppFinanzas.obfuscated.dll --output:decompiled_obfuscated'
                }
            }
        }
    }
    
    post {
        always {
            // Limpieza y otros pasos post-ejecución si es necesario
            cleanWs()
        }
    }
}
