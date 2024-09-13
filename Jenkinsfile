pipeline {
    agent any

    environment {
        // Variables de entorno si es necesario
        REPO_URL = 'https://github.com/Melvin3107/AppFinanzas.git'
        ILSPY_REPO = 'https://github.com/icsharpcode/ILSpy.git'
        OBFSCR_REPO = 'https://github.com/obfuscar/obfuscar.git'
        WORKSPACE_DIR = '/var/jenkins_home/workspace'
        DLL_FILE = 'AppFinanzas.dll'
    }

    stages {
        
        stage('Clean Workspace') {
            steps {
                script {
                    // Eliminar el directorio de trabajo de AppFinanzas si existe
                    def appFinanzasDir = "${WORKSPACE_DIR}/AppFinanzas"
                    if (fileExists(appFinanzasDir)) {
                        sh "rm -rf ${appFinanzasDir}"
                    }
                }
            }
        }

        stage('Checkout') {
            steps {
                script {
                    // Clona el repositorio de código
                    git url: "${REPO_URL}", branch: 'main'

                    // Clona los repositorios de ILSpy y Obfuscar, manejando directorios existentes
                    def ilspyDir = "${WORKSPACE_DIR}/ILSpy"
                    def obfuscarDir = "${WORKSPACE_DIR}/Obfuscar"

                    if (fileExists(ilspyDir)) {
                        sh "rm -rf ${ilspyDir}"
                    }
                    sh "git clone ${ILSPY_REPO} ${ilspyDir}"

                    if (fileExists(obfuscarDir)) {
                        sh "rm -rf ${obfuscarDir}"
                    }
                    sh "git clone ${OBFSCR_REPO} ${obfuscarDir}"
                }
            }
        }

        stage('Build') {
            steps {
                dir("${WORKSPACE_DIR}/AppFinanzas/frontend") {
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
