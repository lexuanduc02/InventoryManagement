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
                        sh "docker container stop ${env.containerName}"
                        sh "docker container rm ${env.containerName}"
                        echo "Delete ${env.containerName} Done"
                    } catch (Exception e) {
                        echo " ${env.containerName} not exists or not running"
                    }
                }
            }
        }

        stage('Delete Docker image if exists') {
            steps {
                script {
                    try {
                        echo "Remove Image"
                        sh "docker image rm ${env.image}"
                        echo "Remove Image Done"
                    } catch (Exception e) {
                        echo " ${env.image} not exists or not running"
                    }
                }
            }
        }

        stage('Build') {
            steps {
                echo "Check SCM"
                checkout scm
                echo "Check SCM Done"
                echo "Build Image start"
                script {
                    docker.build(${env.image} , "-f Dockerfile .")
                }
                echo "Build Image Done"
            }
        }

        stage('Run') {
            steps {
                echo "Start Build Container"
                sh "docker run -d -p 5040:8080  -e TZ=Asia/Ho_Chi_Minh --restart=always --name=${containerName} ${image}"
                echo "Build done !"
            }
        }
    }
}