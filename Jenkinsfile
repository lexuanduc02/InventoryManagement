def image = "im_image"
def containerName = "im_container"

node {
    try {
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
            def imageExists = sh(script: "docker images -q ${image}", returnStatus: true)
            if (imageExists == 0) {
                echo "Image $image does not exist."
            } else {
                stage('Remove Image - ${image}') {
                    echo "Remove Image"
                    sh "docker image rm $image"
                    echo "Remove Image Done"
                }

                stage('Remove Image None - ${image}') {
                    echo "Remove Image None"
                    sh "docker image prune -f"
                    echo "Remove Image None Done"
                }
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

        stage('Clean'){
        //clean the workspace after deployment ignoring node_modules directory
        cleanWs(patterns: [[pattern: 'node_modules', type: 'EXCLUDE']])
        deleteDir()
        try{
            echo "Clean success"
             dir("${env.WORKSPACE}@tmp") {
                        deleteDir()
                      }
                      dir("${env.WORKSPACE}@script") {
                        deleteDir()
                      }
                      dir("${env.WORKSPACE}@script@tmp") {
                        deleteDir()
                      }
         }
         catch(Exception e){
            echo "Clean error"
         }
      }
    } catch (Exception e) {
        currentBuild.result = "FAILED"
        throw e
    } finally {
        echo "Build Done"
    }
}