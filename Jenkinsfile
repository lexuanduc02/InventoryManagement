def image = "im_image"
def containerName = "im_container"

node {
    try {
        stage('Delete Docker Container if exists') {
            // stop and remove logs container
            sh "docker container stop $containerName"
            sh "docker container rm $containerName"
            echo "Delete $containerName Done"
        }

        stage('Delete Docker image if exists') {
            echo "Remove Image"
            sh "docker image rm $image"
            echo "Remove Image Done"
        }

        stage('Build') {
            echo "Check SCM"
            checkout scm
            echo "Check SCM Done"
            echo "Build Image start"
            sh "docker build -t $image -f Dockerfile ."
            echo "Build Image Done"
        }

        stage('Run') {
            echo "Start Build Container"
            sh "docker run -d -p 5040:80 -e TZ=Asia/Ho_Chi_Minh --network Ite-Network --restart=always --name=${containerName} ${image}"
            echo "Build done !"
        }
    } catch (Exception e) {
        currentBuild.result = "FAILED"
        throw e
    } finally {
        echo "Build Done"
    }
}