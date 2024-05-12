Microservices Architecture (.Net)<br/>
based on Les Jackson's Course [https://www.youtube.com/watch?v=DgVjEo3OGBI]

Technologies Used:<br/>
<pre>.Net 8, Docker, Kubernetes (K8s), RabbitMQ, Entity Franework Core (Model-First), gRPC</pre>

![Kubernetes](https://github.com/roydale/.net-microservices/assets/157586512/085fafd6-0c37-4236-83aa-0e80bbad5333)

Definitions:<br/>
<pre>
  Cluster - Contains a set of "worker machines" called Nodes. Every cluster has at least 1 node.
  Cluster IP - A Service that exposes the container "internally" within the Cluster.
  Container - An image that has been executed, containing the app and it's dependencies.
  Image - The result of building an app and it's dependencies. Images are transferable units.
  Load Balancer - A Service that exposes a container externally.
  Node - A Node is "worker machine" that runs containerized applications.
  Node Port - A Service used for development purposes to expose containers externally.
  Pod
    - Smallest K8S object. Represents a set of running containers.
    - Host and run containers. A single pod can have multiple containers.
    - The most common model is to have one container per pod, but a pod can also contain multiple containers that work together.
  Kubernetes Ingress - an API object that helps developers expose their applications and manage external access by providing 
                       http/s routing rules to the services within a Kubernetes cluster.
  WSL 2 - 
  Imperative Commands - Command Line
  Declarative Commands - Config Files
  Storage Concepts
    - Persistent Volume Claim * - States a claim to some physical storage on the machine.
    - Persistent Volume
    - Storage Class
</pre>
  
Docker Commands:<br/>
<pre>
  -- Build a Docker image
  docker build -t [docker hub id]/[app name] . (Context) Path of your project that you want build an image of
  docker build -t [docker hub id]/platform-service-api .
  docker build -t [docker hub id]/command-service-api .
  
  -- Share image to Docker Hub
  docker push [docker hub id]/[app name]:[tag]
  docker push [docker hub id]/platform-service-api
  docker push [docker hub id]/command-service-api
  
  -- Pull an image from Docker Hub
  docker pull [docker hub id]/[app name]:[tag]
  docker pull [docker hub id]/platform-service-api:latest
  docker push [docker hub id]/command-service-api:latest
  
  -- Pulls an image (if needed) and runs a new container (-d = Detach mode: Run container in background and print container id)
  docker run --name=platform_service_api -p 8000:8080 -d [docker hub id]/platform-service-api
  docker run --name=command-service-api -p 8001:8080 -d [docker hub id]/command-service-api
  docker run --name=platform_service_api -p 8000:8080 [docker hub id]/platform-service-api
  docker run --name=command-service-api -p 8001:8080 [docker hub id]/command-service-api
  
  -- List all containers
  docker ps
  docker ps -a
  
  -- Start or stop container
  docker start [container id]
  docker stop [container id]
  
  -- Remove container (-f = force remove)
  docker rm [container id]
  docker rm -f [container id]
  
  -- Remove an image locally
  docker rmi [image id]  
</pre>
  
Docker Compose - Allows you to define and manage multi-container applications in a single YAML file.
<pre>
  -- Gets the Docker Compose version if you're interested
  docker-compose -v
  
  -- Vaidate docker-compose file and helps in correcting syntax
  docker-compose cofig
  
  -- Build image, push image to Docker Hub, and pull the latest image from Docker Hub
  docker-compose build
  docker-compose push
  docker-compose pull
  docker-compose build --push --pull
  docker-compose --env-file ./config/.env.dev build --push
  docker-compose --env-file ./config/.env.prod build --push
  docker-compose --env-file ./config/.env.dev build --push --pull
  docker-compose --env-file ./config/.env.prod build --push --pull
  
  -- Spawns containers defined in docker-compose file
  docker-compose up -d
  docker-compose up -d --scale [service name]=[no. of instances]
  docker-compose up -d --scale platform-api=3
  NOTE: A custom container name doesn't work with scale. Docker requires each container to have a unique name.
  docker-compose --env-file ./config/.env.dev up -d
  docker-compose --env-file ./config/.env.prod up -d
  
  -- Test environment files
  docker-compose --env-file ./config/.env.dev config
  docker-compose --env-file ./config/.env.prod config
  
  -- Deletes containers defined in docker-compose file
  docker-compose down
</pre>

Kubernetes Commands:
<pre>
  -- Gets the Kubernetes version if you're interested
  kubectl version
  
  -- Get the list of object types
  kubectl get [object type]
  kubectl get all
  kubectl get deployments
  kubectl get pods
  kubectl get pods --namespace=[namespace]
  kubectl get pods --namespace=ingress-nginx
  kubectl get services
  kubectl get namespace
  kubectl get ingress
  kubectl get storageclass
  kubectl get pvc
  kubectl get secrets
  kubectl get persistentvolumeclaim
  
  -- Create a deployment
  kubectl apply -f [yaml file]
  kubectl apply -f platform-service-deployment.yaml
  kubectl apply -f platform-node-port-service.yaml
  kubectl apply -f command-service-deployment.yaml
  kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.10.1/deploy/static/provider/cloud/deploy.yaml
  kubectl apply -f ingress-nginx-service.yaml
  kubectl apply -f local-persistent-volume-claim.yaml
  kubectl apply -f mssql-platform-deployment.yaml
  kubectl apply -f mssql-command-deployment.yaml
  
  -- Delete objects
  kubectl delete [object type] [object name]
  kubectl delete deployment [deployment name]
  kubectl delete deployment platform-service-deployment
  kubectl delete deployment command-service-deployment
  kubectl delete ingress ingress-service
  kubectl delete secret mssql
  kubectl delete deployment mssql-command-deployment
  kubectl delete service mssql-command-load-balancer
  kubectl delete service mssql-command-cluster-ip-service
  kubectl delete deployment mssql-deployment
  kubectl delete service mssql-load-balancer
  kubectl delete service mssql-ip-service
  kubectl delete persistentvolumeclaim mssql-storage-claim
  kubectl delete deploy/mssql-deployment svc/mssql-load-balancer svc/mssql-cluster-ip-service pvc/mssql-storage-claim
  
  -- Restart a deployment - forces Kubernetes to pull down latest image (refresh) from Docker hub
  kubectl rollout restart deployment platform-service-deployment
  kubectl rollout restart deployment command-service-deployment
  
  -- Create secret
  kubectl create secret generic mssql --from-literal=[key]="[secret string]"
  kubectl create secret generic mssql --from-literal=MY_PASSWORD="password"
  kubectl create -f mssql-secret.yaml
</pre>
