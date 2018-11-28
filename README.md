# Docker and Kubernetes playground

A toy dockerized service running on K8S demonstrating the very basics of Docker of K8S. 

The service is an HTTP API that returns an HTTP status code of your choice (similar to [httpstat.us](https://httpstat.us)) built on .NET Core with [Nancy](http://nancyfx.org).

# Requirements

* [Docker](https://www.docker.com)
* Your favourite text editor or .NET IDE ([JetBrains Rider](https://www.jetbrains.com/rider/) is quite sweet)
* If you'd like to build locally (not through Docker), you'll need the [.NET Core 2.1+ SDK](https://dotnet.microsoft.com/download)

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

See the comments in the `.sh` files and in the `src/StatusCoder/Dockerfile` file to understand the docker magic. 