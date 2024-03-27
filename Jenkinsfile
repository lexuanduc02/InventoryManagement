def image = "im_image"
def containerName = "im_container"

node {
    try {
        stage('Build') {
            echo "Check SCM"
            checkout scm
            echo "Check SCM Done"
            echo "Build Image start"
            docker.build(image + ":$BUILD_NUMBER", "-f Dockerfile .")
            echo "Build Image Done"
        }

        stage('Run') {
            echo "Start Build Container"
            sh "docker run -d -p 5040:80 --ip 172.18.0.4 -e TZ=Asia/Ho_Chi_Minh --network Ite-Network --restart=always --name=${containerName} ${image}:${BUILD_NUMBER}"
            echo "Build done !"
        }
    } catch (Exception e) {
        currentBuild.result = "FAILED"
        throw e
    } finally {
        echo "Build Done"
    }
}