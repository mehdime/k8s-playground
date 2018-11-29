#!/bin/bash

# Kill the container in case it's already running
docker rm statuscoder >/dev/null 2>/dev/null

# This relies on our docker image having been built with a name of 'statuscoder'.
# --name is the name you want to give the container you're starting. It can be anything you want.
# --publish defines on which local port (of the host machine) our application be bound.
docker run --name statuscoder --publish 80:5100 statuscoder
