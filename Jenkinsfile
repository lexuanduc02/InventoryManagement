def image = "im_image"
def containerName = "im_container"

node {
    try {
         stage('Initialize'){
            def dockerHome = tool 'myDocker'
            env.PATH = "${dockerHome}/bin:${env.PATH}"
        }
        
        stage('Delete Docker Container if exists') {
            // stop and remove logs container
            try {
                sh "docker container stop $containerName"
                sh "docker container rm $containerName"
                echo "Delete $containerName Done"
            } catch (Exception e) {
                echo " $containerName not exists or not running"
            }
        }

        stage('Delete Docker image if exists') {
            try {
                echo "Remove Image"
                sh "docker image rm $image"
                echo "Remove Image Done"
            } catch (Exception e) {
                echo " $containerName not exists or not running" 
                throw e
            } 
        }

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