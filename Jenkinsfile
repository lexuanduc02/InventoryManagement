def image = "im_image"
def containerName = "im_container"

pipeline {
    agent any

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
                    try {
                        echo "Removing image: $image"
                        sh "docker image rm $image"
                        echo "Deleted $image"
                    } catch (Exception e) {
                        echo "$image does not exist or is not running"
                    }
                }
            }
        }

        stage('Build') {
            steps {
                echo "Checking out SCM"
                checkout scm
                echo "Checked out SCM"
                echo "Building image"
                script {
                    docker.build(image, "-f Dockerfile .")
                }
                echo "Build Complete"
            }
        }

        stage('Run') {
            steps {
                echo "Starting container"
                sh "docker run -d -p 5040:8080 -e TZ=Asia/Ho_Chi_Minh --restart=always --name=${containerName} ${image}"
                echo "Container started successfully"
            }
        }
    }
}
