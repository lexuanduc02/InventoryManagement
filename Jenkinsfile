pipeline {
    agent any

    stages {
        stage('Delete Docker Container if exists') {
            steps {
                script {
                    try {
                        sh "docker container stop im_container"
                        sh "docker container rm im_container"
                        echo "Deleted im_container"
                    } catch (Exception e) {
                        if (e.toString().contains("No such container")) {
                            echo "im_container does not exist."
                        } else {
                            echo "Error occurred while deleting im_container: ${e.toString()}"
                        }
                    }
                }
            }
        }

        stage('Delete Docker image if exists') {
            steps {
                script {
                    if (sh(script: "docker images -q im_image", returnStatus: true) == 0) {
                        echo "Image im_image does not exist."
                    } else {
                        echo "Removing Image: im_image"
                        sh "docker image rm im_image"
                        echo "Removed Image: im_image"
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
                echo "Building Image: im_image"
                script {
                    docker.build("im_image:${env.BUILD_NUMBER}", "-f Dockerfile .")
                }
                echo "Built Image: im_image"
            }
        }

        stage('Run') {
            steps {
                echo "Starting Container"
                sh "docker run -d -p 5040:8080 -e TZ=Asia/Ho_Chi_Minh --restart=always --name=im_container im_image:${env.BUILD_NUMBER}"
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
