pipeline {
    // Defines the agent where the pipeline will run. 'any' means any available agent.
    agent {
        // Specify the label of the Windows slave node.
        label 'windows-node1' 
    }
    tools {
        nodejs 'Node-20' // Use the name you configured in Jenkins
    }
    
    // Defines environment variables for convenience.
    environment {
        // Path to the Node.js backend directory.
        BACKEND_DIR = 'backend-node'
        // Path to the .NET frontend directory.
        FRONTEND_DIR = 'frontend-dotnet'
    }

    // Defines the stages of the pipeline.
    stages {
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
                // Use 'start /b' for Windows to run processes in the background.
                bat 'start /b node backend-node/index.js'
                bat 'start /b dotnet run --project frontend-dotnet\\frontend-dotnet.csproj'
            }
        }
        
        // Stage for cleanup or post-build actions.
        stage('Cleanup') {
            steps {
                echo 'Killing all background processes...'
                // Killing processes on Windows is more complex and depends on the specific process name.
                // This is a simple example for a known process name.
                bat 'taskkill /F /IM node.exe'
                bat 'taskkill /F /IM dotnet.exe'
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
