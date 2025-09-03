pipeline {
    agent any

    stages {
        stage('Clone Repository') {
            steps {
                git branch: 'main', url: 'https://github.com/DivijeshVarma/dotnetnodejs.git'
            }
        }

        stage('Install Node Backend') {
            steps {
                dir('backend') {
                    bat 'npm install'
                }
            }
        }

        stage('Run Node Backend') {
            steps {
                dir('backend') {
                    bat 'start /B npm start'
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

