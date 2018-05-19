pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        sh "docker build -t bitarapi ."
      }
    }
    stage('Deploy') {
      steps {
        // Kill container in case there is a leftover
        sh "[ -z \\"\\$(docker ps -a | grep bitarapi 2>/dev/null)\\" ] || docker rm -f bitarapi"
        
        echo "Starting container"
        sh "docker run --detach --name bitarapi --rm --publish 5000:80 bitarapi"
      }
    }
  }
}
