pipeline {
    agent any

    stages {
        stage('Clone Repository') {
            steps {
                git branch: 'main', url: 'https://github.com/DivijeshVarma/dotnetnodejs.git'
            }
        }

        // Add this stage to run git whatchanged with the required flag
        stage('Git Whatchanged Check') {
            steps {
                // Run only on Windows agent (adjust label if necessary)
                script {
                    if (env.NODE_LABELS?.contains('windows')) {
                        bat 'git whatchanged --no-abbrev -M --i-still-use-this'
                    } else {
                        echo "Skipping git whatchanged, not a Windows node."
                    }
                }
            }
        }

        stage('Install Node Backend') {
            steps {
                dir('backend') {
		   withNodeJS('NodeJS') {
                       bat 'npm install'
		   } 
                }
            }
        }

        stage('Run Node Backend') {
            steps {
                dir('backend') {
                   withNodeJS('NodeJS') {
                       bat 'start /B npm start'
		   } 
                }
            }
        }

        stage('Build .NET Frontend') {
            steps {
                dir('frontend-dotnet') {
                    bat 'dotnet restore'
                    bat 'dotnet build --configuration Release'
                }
            }
        }

        stage('Run .NET Frontend') {
            steps {
                dir('frontend-dotnet') {
                    bat 'start /B dotnet run'
                }
            }
        }
    }
}
