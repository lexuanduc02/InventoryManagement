def image = "im_image"
def containerName = "im_container"

node {
    try {
        stage('Delete Docker Container if exists') {
            // Stop and remove logs container
            try {
                sh "docker container stop im_container"
                sh "docker container rm im_container"
                echo "Deleted im_container"
            } catch (Exception e) {
                echo "im_container does not exist or is not running"
            }
        }

        stage('Delete Docker image if exists') {
            def imageExists = sh(script: "docker images -q im_image", returnStatus: true)
            if (imageExists == 0) {
                echo "Image im_image does not exist."
            } else {
                stage('Remove Image') {
                    echo "Removing Image: im_image
                    sh "docker image rm im_image"
                    echo "Removed Image: im_image"
                }

                stage('Remove Image None') {
                    echo "Removing Unused Images"
                    sh "docker image prune -f"
                    echo "Removed Unused Images"
                }
            }
        }

        stage('Build') {
            echo "Checking out SCM"
            checkout scm
            echo "Checked out SCM"
            echo "Building Image: im_image"
            docker.build("im_image:${env.BUILD_NUMBER}", "-f Dockerfile .")
            echo "Built Image: im_image"
        }

        stage('Run') {
            echo "Starting Container"
            sh "docker run -d -p 5040:8080 -e TZ=Asia/Ho_Chi_Minh --restart=always --name=im_container im_image:${env.BUILD_NUMBER}"
            echo "Container Started"
        }

        stage('Clean') {
            // Clean the workspace after deployment, ignoring node_modules directory
            cleanWs(patterns: [[pattern: 'node_modules', type: 'EXCLUDE']])
            deleteDir()

            // Additional cleanup
            try {
                echo "Cleaning Workspace"
                dir("${env.WORKSPACE}@tmp") {
                    deleteDir()
                }
                dir("${env.WORKSPACE}@script") {
                    deleteDir()
                }
                dir("${env.WORKSPACE}@script@tmp") {
                    deleteDir()
                }
                echo "Workspace Cleaned"
            } catch (Exception e) {
                echo "Error Cleaning Workspace"
            }
        }
    } catch (Exception e) {
        currentBuild.result = "FAILED"
        throw e
    } finally {
        echo "Build Done"
    }
}
