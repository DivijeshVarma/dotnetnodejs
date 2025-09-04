pipeline {
    // Defines the agent where the pipeline will run. 'any' means any available agent.
    agent {
        // Specify the label of the Windows slave node.
        label 'windows-node1' 
    }
    tools {
        nodejs 'Node-20' // Use the name you configured in Jenkins
	dotnetsdk 'dotnet-sdk'
    }
    
    // Defines environment variables for convenience.
    environment {
        // Path to the Node.js backend directory.
        BACKEND_DIR = "backend" 
        // Path to the .NET frontend directory.
        FRONTEND_DIR = "frontend-dotnet"
	FRONTEND_URL = 'http://172.28.8.53:5050'
    }

    // Defines the stages of the pipeline.
    stages {
        // New stage to clean up processes from previous runs
        stage('Pre-Build Cleanup') {
            steps {
                echo 'Cleaning up any running processes from a previous build...'
                bat 'taskkill /F /IM dotnet.exe || exit 0'
                bat 'taskkill /F /IM node.exe || exit 0'
            }
        }
        // Stage for building the Node.js backend.
        stage('Build Node.js Backend') {
            steps {
                // Change directory to the backend.
                dir(env.BACKEND_DIR) {
                    // Use 'bat' for Windows batch commands.
                    bat 'npm install' 
                    // This is a simple build step; for more complex apps, you might have a 'build' script.
                    echo 'Node.js backend dependencies installed.'
                }
            }
        }

        // Stage for building the .NET frontend.
        stage('Build .NET Frontend') {
            steps {
                // Change directory to the frontend.
                dir(env.FRONTEND_DIR) {
                    // Use 'bat' for Windows batch commands.
                    bat 'dotnet restore' 
                    // Builds the .NET application. The '--no-restore' flag prevents a redundant restore.
                    bat 'dotnet build --no-restore' 
                    echo '.NET frontend built successfully.'
                }
            }
        }
        
        // This stage is for demonstration; a real scenario would likely use Docker or a more robust deployment strategy.
        stage('Run Applications') {
            steps {
                echo 'Starting applications...'

		// Change to the backend directory and run the command.
                dir(env.BACKEND_DIR) {
                    bat 'START /B node index.js'
                }

                // Change to the frontend directory and run the command with the URL.
                timeout(time: 60, unit: 'SECONDS')
		    dir(env.FRONTEND_DIR) {
                        bat 'dotnet run --urls=http://0.0.0.0:5050'
                }

		echo 'Applications started. Access them via the following URLs:'
                echo 'Node.js Backend: http://172.28.8.53:3000'
                echo '.NET Frontend: http://172.28.8.53:5050'
            }
        }

	stage('Verify Application is Accessible') {
            steps {
                echo "Testing frontend at ${env.FRONTEND_URL}"
                // Use PowerShell's Invoke-WebRequest to check the URL
                bat "powershell.exe -Command \"Invoke-WebRequest -Uri ${env.FRONTEND_URL}\""
                echo 'Frontend application is accessible.'
            }
        }
    }     
   
    // Defines actions to perform after the pipeline finishes, regardless of success or failure.
    post {
        // Always runs this block.
        always {
            echo 'Pipeline finished.'
            // You can add more cleanup or reporting here.
        }
    }
}
