pipeline {
    agent none

    environment {
        IMAGE_PAMA_SMR = "beithub/pama-smr"
        IMAGE_PAMA_API = "beithub/pama-api"
        SERVER_USER = "beit"
        SERVER_IP = "116.193.172.225"
    }

    stages {
        stage('Initialize') {
            agent { label 'PAMA' }
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

        stage('Build Docker Images') {
            agent { label 'PAMA' }
            steps {
                dir('/var/www/pama-source') {
                    sh '''
                        # Build the SMR Docker image
                        docker build -t $IMAGE_PAMA_SMR -f 1.PAMA.Razor.Views/Dockerfile .

                        # Build the API Docker image
                        docker build -t $IMAGE_PAMA_API -f 2.Web.API.Controllers/Dockerfile .
                    '''
                }
            }
        }

        stage('Deploy to Server') {
            agent { label 'PAMA' }
            steps {
                dir('/var/www/') {
                    sh '''
                        docker compose up -d pama-smr --force-recreate
                        docker compose up -d pama-api --force-recreate
                    ''' 
                }
            }
        }

        stage('Push Docker Images to Docker Hub') {
            agent { label 'PAMA' }
            steps {
                catchError(buildResult: 'SUCCESS', stageResult: 'UNSTABLE') {
                    withCredentials([usernamePassword(credentialsId: 'docker-hub-credentials', usernameVariable: 'DOCKER_HUB_USERNAME', passwordVariable: 'DOCKER_HUB_PASSWORD')]) {
                        sh """
                            echo \$DOCKER_HUB_PASSWORD | docker login -u \$DOCKER_HUB_USERNAME --password-stdin
                            docker push $IMAGE_PAMA_SMR:latest
                            docker push $IMAGE_PAMA_API:latest
                            docker logout
                        """
                    }
                }
            }
        }
    }

    post {
        success { echo 'Build and deployment completed successfully!' }
        unstable { echo 'Build and deployment completed, but Docker Hub push failed.' }
        failure { echo 'Build or deployment failed.' }
    }
}
