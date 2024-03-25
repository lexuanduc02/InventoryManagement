pipeline {
    agent any

    environment {
        image = "im_image"
        containerName = "im_container"
    }

    stages {
        stage('Delete Docker Container if exists') {
            steps {
                script {
                    try {
                        sh "docker container stop $containerName"
                        sh "docker container rm $containerName"
                        echo "Deleted $containerName"
                    } catch (Exception e) {
                        echo "$containerName does not exist or is not running"
                    }
                }
            }
        }

        stage('Delete Docker image if exists') {
            steps {
                script {
                    def imageExists = sh(script: "docker images -q ${env.image}", returnStatus: true)
                    if (imageExists == 0) {
                        echo "Image ${env.image} does not exist."
                    } else {
                        echo "Removing Image: ${env.image}"
                        sh "docker image rm ${env.image}"
                        echo "Removed Image: ${env.image}"

                        echo "Removing Unused Images"
                        sh "docker image prune -f"
                        echo "Removed Unused Images"
                    }
                }
            }
        }

        stage('Build') {
            steps {
                echo "Checking out SCM"
                checkout scm
                echo "Checked out SCM"

                echo "Building Image: ${env.image}"
                docker.build("${env.image}:${env.BUILD_NUMBER}", "-f Dockerfile .")
                echo "Built Image: ${env.image}"
            }
        }

        stage('Run') {
            steps {
                echo "Starting Container"
                sh "docker run -d -p 5040:8080 -e TZ=Asia/Ho_Chi_Minh --restart=always --name=${env.containerName} ${env.image}:${env.BUILD_NUMBER}"
                echo "Container Started"
            }
        }

        stage('Clean') {
            steps {
                echo "Cleaning Workspace"
                cleanWs(deleteDirs: true)

                echo "Workspace Cleaned"
            }
        }
    }

    post {
        always {
            echo "Build Done"
        }
        failure {
            currentBuild.result = "FAILED"
        }
    }
}
