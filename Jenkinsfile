pipeline {
    agent none  // No global agent; each stage defines its own agent.
     
    stages {
        stage('Initialize') {
            agent {
                label 'PAMA' // Server Ubuntu ITM Core
            }
            steps {
                retry(3) {
                    dir('/var/www/pama-source') {
                        sh '''
                            git config --global http.postBuffer 524288000
                            git config --global http.maxPackSize 524288000

                            echo Resetting repository to origin/staging
                            git reset --hard HEAD
                            git checkout staging
                            git pull
                        ''' 
                    }
                }
            }
        }

        stage('Turn off IIS Service') {
            agent {
                label 'Black Server' // Server Item Kantor
            }
            steps {
                dir('C:\\deploy\\pama\\scripts') {
                    bat 'turn-off-service.cmd'
                }
                dir('C:\\deploy\\pama-api\\scripts') {
                    bat 'turn-off-service.cmd'
                }
            }
        }

        stage('Build Backend') {
            agent {
                label 'PAMA' // Server Ubuntu PAMA
            }
            steps {
                dir('/var/www/pama-source/1.PAMA.Razor.Views') {
                    sh 'rm -rf /var/www/pama-source/1.PAMA.Razor.Views/appsettings.json'
                    sh 'rm -rf /var/www/pama-source/1.PAMA.Razor.Views/appsettings.Development.json'
                    sh 'rm -rf /var/www/pama-source/1.PAMA.Razor.Views/appsettings.Production.json'
                    sh 'mv appsettings.self-deployment.json appsettings.json'
                    sh 'dotnet restore'
                    sh 'dotnet clean'  
                    sh 'dotnet publish -c Release -o /var/www/pama'
                }
            }
        }

        stage('Build API') {
            agent {
                label 'PAMA' // Server Ubuntu PAMA
            }
            steps {
                dir('2.Web.API.Controllers') {
                    sh 'rm -rf 2.Web.API.Controllers/appsettings.json'
                    sh 'rm -rf 2.Web.API.Controllers/appsettings.Development.json'
                    sh 'rm -rf 2.Web.API.Controllers/appsettings.Production.json'
                    sh 'mv appsettings.self-deployment.json appsettings.json'
                    sh 'dotnet clean'  
                    sh 'dotnet restore' 
                    sh 'dotnet publish -c Release -o /var/www/pama-api'
                }
            }
        }

        stage('Restart PAMA SMR Service') {
            agent { label 'PAMA' } 
            steps {
                sh '''
                    echo "* Restarting pama-smr service..."
                    script -q -c "sudo systemctl restart pama" /dev/null
                    script -q -c "sudo systemctl status pama --no-pager" /dev/null
                '''
            }
        }

        stage('Turn on IIS Service & Zip Build Folder') {
            agent {
                label 'Black Server' // Server Item Kantor
            }
            steps {
                dir('C:\\deploy\\pama\\scripts') {
                    // Run the batch script in the background without blocking the pipeline
                    bat 'turn-on-service.cmd'
                    echo 'This stage is completed, and you can access the service.'
                }
                dir('C:\\deploy\\pama-api\\scripts') {
                    // Run the batch script in the background without blocking the pipeline
                    bat 'turn-on-service.cmd'
                    echo 'This stage is completed, and you can access the service.'
                }
            }
        }

    } 

    post {
        success {
            echo 'Build completed successfully!'
        }
        failure {
            echo 'Build failed.'
        }
    }
} 