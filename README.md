# confchat application

A demo group chat application made with Blazor and SignalR, using Redis as an (optional) backplane to allow for
hoirizontal scaling.

## Registry authentication
In order to be able to push the Docker image and Helm chart to your ACR, you need to login.
The easiest (and less secure way) is to use the admin password.

```sh
docker login yourrepo.azurecr.io
```

Same goes for Helm:
```sh
helm registry login https://yourrepo.azurecr.io/helm
```

## Build and push instructions using Azure Container Registry

1. Build and push the docker image, replacing *yourrepo* with your ACR instance name.
   ```
   docker build . -t yourrepo.azurecr.io/confchat:latest
   docker push yourrepo.azurecr.io/confchat:latest
   ```

2. Package and push the Helm chart
   ```
   helm package charts/confchat
   helm push confchat-*.tgz oci://yourrepo.azurecr.io/helm
   ```
