pipeline {
    agent any

    environment {
        IMAGE_NAME = "im_image"
        CONTAINER_NAME = "im_container"
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build and Run Container') {
            steps {
                script {
                    try {
                        // Kiểm tra xem container đã tồn tại hay chưa
                        sh "docker inspect --format '{{.Id}}' ${env.CONTAINER_NAME}"
                        if (currentBuild.result != 'SUCCESS') {
                            echo "Deleting existing container: ${env.CONTAINER_NAME}"
                            sh "docker container stop ${env.CONTAINER_NAME}"
                            sh "docker container rm ${env.CONTAINER_NAME}"
                        }
                    } catch (Exception e) {
                        echo "Container ${env.CONTAINER_NAME} does not exist."
                    }

                    try {
                        // Kiểm tra xem image đã tồn tại hay chưa
                        sh "docker images -q ${env.IMAGE_NAME}"
                        if (currentBuild.result != 'SUCCESS') {
                            echo "Image ${env.IMAGE_NAME} does not exist."
                        } else {
                            echo "Removing existing image: ${env.IMAGE_NAME}"
                            sh "docker image rm ${env.IMAGE_NAME}"
                        }
                    } catch (Exception e) {
                        echo "Error while checking image existence: $e"
                    }

                    echo "Building Docker image"
                    docker.build("${env.IMAGE_NAME}:${BUILD_NUMBER}", "-f Dockerfile .")

                    echo "Starting Docker container"
                    sh "docker run -d -p 7029:8080 -e TZ=Asia/Ho_Chi_Minh --restart=always --name=${env.CONTAINER_NAME} ${env.IMAGE_NAME}:${BUILD_NUMBER}"
                }
            }
        }
    }

    post {
        always {
            // Xóa container nếu build hoặc bất kỳ stage nào bị lỗi
            script {
                try {
                    echo "Deleting container: ${env.CONTAINER_NAME}"
                    sh "docker container stop ${env.CONTAINER_NAME}"
                    sh "docker container rm ${env.CONTAINER_NAME}"
                    echo "Deleted container: ${env.CONTAINER_NAME}"
                } catch (Exception e) {
                    echo "Error while deleting container: $e"
                }

                try {
                    echo "Removing image: ${env.IMAGE_NAME}:${BUILD_NUMBER}"
                    sh "docker image rm ${env.IMAGE_NAME}:${BUILD_NUMBER}"
                    echo "Removed image: ${env.IMAGE_NAME}:${BUILD_NUMBER}"
                } catch (Exception e) {
                    echo "Error while deleting image: $e"
                }
            }
        }
    }
}
