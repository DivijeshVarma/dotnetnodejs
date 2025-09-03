pipeline {
    agent any

    stages {
        stage('Clone Repository') {
            steps {
                git 'https://github.com/DivijeshVarma/dotnetnodejs.git'
            }
        }

        stage('Install Node Backend') {
            steps {
                dir('backend') {
                    sh 'npm install'
                }
            }
        }

        stage('Run Node Backend') {
            steps {
                dir('backend') {
                    sh 'nohup npm start &'
                }
            }
        }

        stage('Build .NET Frontend') {
            steps {
                dir('frontend-dotnet') {
                    sh 'dotnet restore'
                    sh 'dotnet build --configuration Release'
                }
            }
        }

        stage('Run .NET Frontend') {
            steps {
                dir('frontend-dotnet') {
                    sh 'nohup dotnet run &'
                }
            }
        }
    }
}

