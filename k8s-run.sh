#!/bin/bash

# We're using Kubernetes' declarative management method here to start or update our app. When we "apply" our K8S config, 
# K8S will compare the current state of the K8S cluster with what we've specified we wanted in the .yaml file.
# 
# If the cluster is already in the desired state (i.e. the app is already running and configured as per the .yaml file), 
# K8S will simply do nothing.
#
# If the cluster state and the .yaml spec differ, K8S will update the cluster state to match the .yaml spec. E.g. if your
# app is currently running with 3 replicas in the cluster and you're applying a .yaml spec where you've bumped the number
# of replicas to 5, K8S will simply spin up another two pods for your app and won't otherwise touch anything else.
#
# This is an alternative to the imperative management method (e.g. the 'kubectl create' command) where you instruct K8S
# what to do.
kubectl apply -f ./src/StatusCoder/k8s.yaml