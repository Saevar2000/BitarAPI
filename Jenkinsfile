pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        sh 'docker build -t bitarapi .'
      }
    }
    stage('Deploy') {
      steps {
        sh '''// Kill container in case there is a
sh "[ -z \\"\\$(docker ps -a | grep bitarapi 2>/dev/null)\\" ] || docker rm -f bitarapi"'''
      }
    }
  }
}