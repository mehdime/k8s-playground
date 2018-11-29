# Docker and Kubernetes playground

A toy dockerized service running on K8S demonstrating the very basics of Docker of K8S. 

The service is an HTTP API that returns an HTTP status code of your choice (similar to [httpstat.us](https://httpstat.us)) built on .NET Core with [Nancy](http://nancyfx.org).

# Requirements

* Your favourite OS (the commands below have only been tested on macOS but should work on anything that runs docker)
* [Docker](https://www.docker.com). Once installed, open the Docker Preferences and tick **Kubernetes -> Enable Kubernetes**.
* Your favourite text editor or .NET IDE ([JetBrains Rider](https://www.jetbrains.com/rider/) is quite sweet)
* If you'd like to build locally (not through Docker), you'll need the [.NET Core 2.1+ SDK](https://dotnet.microsoft.com/download)

# Want to use this repo to learn about Docker and K8S?

Suggest you:
1. Start with the **Build & Run (locally)** section just to make sure your can build and run the app.
2. Then follow **Build & Run (on Docker)** and check out the Dockerfile file and the docker commands in the `.sh` files to learn about the basics of Docker.
3. Then folllow **Run on Kubernetes** for the grand finale.

# Build & Run (locally)

```
. build.sh
. run.sh
```

The app should now listen on port 5100. Try it: [http://localhost:5100/200](http://localhost:5100/200)

# Build & Run (on Docker)

```
. docker-build.sh
. docker-run.sh
```

When running on docker, we bind to local port 80. Try it: [http://localhost/200](http://localhost/200)

#### Understanding the Docker magic
See the comments in the [Dockerfile](src/StatusCoder/Dockerfile) first. Then check out how how we build and run our Docker image in [docker-build.sh](docker-build.sh) and [docker-run.sh](docker-run.sh).

From there, the [Docker command-line reference](https://docs.docker.com/engine/reference/commandline/cli/) will answer most of your questions.

# Run on Kubernetes

First, build the Docker image:

```
. docker-build.sh
```

Then start it on Kubernetes:

```
. k8s-run.sh 
```

When running on K8S, we expose the app on port 30000. Try it: [http://localhost:30000/200](http://localhost:30000/200)

Here are some useful commands to explore or troubleshoot:

```
# View all the deployments running on the cluster. You should see 'statuscoder-deployment' in there.
kubectl get deploymeents

# View the current state of our deployment, including a history of what happened to it.
kubectl describe deployment statuscoder-deployment

# View all the pods running in the K8S cluster
kubectl get pods

# View the current state of a given pod, including a history of what happened to it.
kubectl describe pod <pod name>

# If a pod looks sad, view its logs (this will display the logs that our ASP.NET app writes to the console):
kubectl logs <pod name>

# To stop the app, delete both the deployment and the service we created in the .yaml file
kubectl delete deployment statuscoder-deployment
kubectl delete service statuscoder-service
``` 

#### Seeing the liveness probe in action
Our .yaml file instructs K8S to start 3 replicas of our app (i.e. 3 pods). 

K8S can be configured to continuously monitor the health of our pods via a **liveness probe** and to automatically restart pods that are unhealthy. 

In our toy app, we've exposed a `/health` HTTP endpoint that returns the current health of the app (`200 OK` by default). You can try it yourself afer having started the app in K8S: [http://localhost:30000/health](http://localhost:30000/health)

In [our .yaml file](src/StatusCoder/k8s.yaml) we've configured our pod's liveness probe to be this `/health` endpoint and instructed K8S to check it once a second. Check out the real-time logs of one of your running pods to see this endpoint getting hit once a second:

```
# List all the running pods to find the name of a pod
kubectl get pods

# Pick one of the pods and display its logs in real-time
kubectl logs --follow <pod name>
```

In order to test the ability of K8S to detect and restart unhealthy pods, our toy app also exposes a `/die` endpoint that you can hit to cause this instance of the app to start reporting an unhealthy status on its `/health` endpoint (the health endpoint will now return `503 Service Unavailable`).

Try it now: [http://localhost:30000/die](http://localhost:30000/die)

One of your running pods is now reporting an unhealthy status on its liveness probe. Within a second, K8S should have detected this and restarted this pod. You can check if this happened by listing the running pods:

```
# One of your running pods should now have a 'RESTARTS' value of 1
kubectl get pods
```

Display the current state of the pod that was restarted and see in its Events table the sequence of events that led to the restart of that pod:

```
kubectl describe pod <name of the pod that was restarted>
``` 

#### Understanding the K8S magic
See the comments in the [k8s.yaml](src/StatusCoder/k8s.yaml) first. Then look at how we run it in [k8s-run.sh](k8s-run.sh).

The [kubectl cheatsheet](https://kubernetes.io/docs/reference/kubectl/cheatsheet/) is very useful.

Taking a few minutes to read the [Understanding Kubernetes Objects](https://kubernetes.io/docs/concepts/overview/working-with-objects/kubernetes-objects/) article is very useful to graps some key fundamental understanding of how K8S is architectured.  