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
``` 

#### Understanding the K8S magic
See the comments in the [k8s.yaml](src/StatusCoder/k8s.yaml) first. Then look at how we run it in [k8s-run.sh](k8s-run.sh).

And then... oh my.