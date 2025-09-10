pipeline {
    agent {
        label 'windows-node1' // Make sure this matches your Windows slave label
    }
    tools {
        nodejs 'Node-20'
        dotnetsdk 'dotnet-sdk'
    }

    environment {
        BACKEND_DIR = "backend"
        FRONTEND_DIR = "frontend-dotnet"
        FRONTEND_URL = 'http://172.28.8.53:5050' // Replace with your Windows slave IP
	DOTNET_BACKGROUND = "%USERPROFILE%\\.dotnet\\tools\\DotnetBackground.exe"
    }

    stages {
        stage('Pre-Build Cleanup') {
            steps {
                echo 'Cleaning up previous Node/.NET processes...'
                bat 'taskkill /F /IM dotnet.exe || exit 0'
                bat 'taskkill /F /IM node.exe || exit 0'
            }
        }

        stage('Build Node.js Backend') {
            steps {
                dir(env.BACKEND_DIR) {
                    bat 'npm install'
		    bat 'npm install pm2 -g'
                    echo 'Node.js dependencies installed.'
                }
            }
        }

        stage('Start Node.js Backend') {
            steps {
                dir(env.BACKEND_DIR) {
		    echo 'Starting Node.js backend via PM2...'
                    bat 'pm2 start index.js --name backend-app || pm2 restart backend-app'
                }
            }
        }

        stage('Build .NET Frontend') {
            steps {
                dir(env.FRONTEND_DIR) {
                    bat 'dotnet restore'
                    bat 'dotnet build --no-restore'
		    bat 'dotnet tool install --global DotnetBackground'
                    echo '.NET frontend built successfully.'
                }
            }
        }

        stage('Start .NET Frontend') {
            steps {
                dir(env.FRONTEND_DIR) {
                    echo 'Starting .NET frontend in background on 0.0.0.0:5050...'
		    bat "\"${env.DOTNET_BACKGROUND}\" run"
                }
            }
        }

        stage('Display URLs') {
            steps {
                echo "Frontend URL: ${env.FRONTEND_URL}"
                echo "Backend API: ${env.FRONTEND_URL.replace('5050', '3000')}/api/message"
            }
        }
    }

    post {
        always {
            echo 'Pipeline completed.'
        }
    }
}

