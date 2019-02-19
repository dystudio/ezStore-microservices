# A microservices system built on .NET Core (in-progress)

## Technologies and frameworks used:
- DDD
- CLEAN architecture
- ASP.NET MVC Core 2.2
- Microsoft.EntityFrameworkCore (2.2)
- IdentityServer4 (2.1.1)
- Angular6
- CoreUI
- mysql (5.7)
- mongo (4.1.1)
- rabbitmq (3.7)
- Docker-ce
- Kubernetes (1.10.3)
- Istio-1.0.2

## IDE
- Visual Studio Code

## Local Development
- From folder `CoreServices`, run `docker-compose -f docker/docker-compose.init.yml up` to start databases and queues
- Wait for databases and queues ready
- Run task `Run CoreServices` to start `CoreServices`

- From folder `ezStore`, run `docker-compose -f docker/docker-compose.init.yml up` to start databases and queues
- Wait for databases and queues ready

- Run Angular6 project from `ezStoreWebUI` by `npm start`. Note: I am using port 7001
- If you want to deploy Angular project to docker, build project `ng build --prod` then run `docker-compose -f docker-compose-webui.yml up`

- Open IdentityServer: http://localhost:55161, http://localhost:55161/swagger
- Open Logging Api: http://localhost:55160/swagger
- Open Setting Api: http://localhost:55163/swagger
- Open FileSystem Api: http://localhost:55159/swagger
- Open Order Api: http://localhost:45160/swagger
- Open Payment Api: http://localhost:45161/swagger
- Open Product Api: http://localhost:45162/swagger
- Open Warehouse Api: http://localhost:45163/swagger
- Open WebUI: http://localhost:7001
- Use IdentityServer: http://localhost:55161 to create a user, then login by WebUI: http://localhost:7001 to get `token` in `localstorage`
- Use `token` as `Bearer Authentication` for `Swagger`

## Install Kubernetes on Ubuntu (inprogress...)
- Run command `sudo snap install microk8s --classic` to install `microk8s` (https://microk8s.io/)
- Add alias `sudo snap alias microk8s.kubectl kubectl`
- Start microk8s by running command `sudo microk8s.start`
- Enable addons by running command `microk8s.enable dns dashboard`
- Enable Istio by running command `microk8s.enable istio`

## Local Kubernetes Deployment with Istio Dashboard (Application metric & health check)
### Pulish images
- (If you want to update Docker Repository) Login Docker Hub `docker login` by DockerId: ezstoremicroservices   Password: 8dh&^5D@@
- (If you want to update Docker Repository) Run `k8s/01-build-db`, this will build images for databases and queues then push to Docker repository (optional: if images are already there)
- (If you want to update Docker Repository) Run `k8s/03-build-api` to build and publish API image
- (If you want to update Docker Repository) Run `k8s/05-build-webui` to build WebUI image then run `k8s\06-publish-webui-image.bat` to publish WebUI image
### Deploy images
- Download Istio from https://github.com/istio/istio/releases/download/1.0.6/istio-1.0.6-win.zip.
- From Istio folder, run `kubectl apply -f install/kubernetes/istio-demo-auth.yaml`
- Add `istio-1.0.6\bin` absolute path into `PATH` in `Environment Variables`
- Waiting for Istio ready, run Istio Dashboard command from GIT Bash: `kubectl -n istio-system port-forward $(kubectl -n istio-system get pod -l app=grafana -o jsonpath='{.items[0].metadata.name}') 4001:3000 &` and open http://localhost:4001
- Run `k8s/02-deploy-db` to set up API.
- Run `k8s/istio-03-rules.bat` to allow Istio Service to connect database from outside 
- Open `hosts` file, add custom DNS:
  - 127.0.0.1 microservices.identityserver 
  - 127.0.0.1 microservices.logging
  - 127.0.0.1 ezstore.orderapi
  - 127.0.0.1 ezstore.paymentapi
  - 127.0.0.1 ezstore.productapi
  - 127.0.0.1 ezstore.warehouseapi
- Run `k8s/istio-04-gateway` to allow run API from domain name http://microservices.identityserver:40101/
- Run `k8s/istio-02-setup-api` to set up API with injected Istio sidecar.
- Run command from GIT Bash `kubectl -n istio-system port-forward $(kubectl get pod -l istio=ingressgateway -n istio-system -o jsonpath={.items[0].metadata.name}) 40101:40101`
- Open site http://microservices.identityserver:40101/ and check activity from http://localhost:4001 
- Get istio-ingressgateway: `kubectl get svc istio-ingressgateway -n istio-system`
- (updating...)

## Microservices
- Identity Server
- Logging Service
- Notification Service
- Setting Service
- Product Service

## Entity Framework Migration
- Implement database structure in ezStore.Product.Infrastructure: Entities and DbContext
- Implement code to run migration in `DatabaseInitialization.cs`
- From `ezStore.Product.API`, register service:

`services.AddDbContext<ApplicationDbContext>(options => 
                options.UseMySql(Configuration.GetConnectionString(MicroservicesConstants.DefaultConnection), b => b.MigrationsAssembly("ezStore.Product.API")));`
- Add migration:
    - From Visual Studio run command: `Add-Migration Initial`
    - Or, run command line: `dotnet ef migrations add Initial`
- Call DatabaseInitialize from Program.cs

## Reference
### CLEAN Architecture
- https://herbertograca.com/2017/11/16/explicit-architecture-01-ddd-hexagonal-onion-clean-cqrs-how-i-put-it-all-together/
### Kubernetes
- https://kubernetes.io/
### Istio
- https://istio.io/
- https://istio.io/docs/tasks/traffic-management/ingress/
### Microk8s
-https://microk8s.io/

## Contributing
- Fork the repo on GitHub
- Clone the project to your own machine
- Add `upstream` branch: `git remote add upstream https://github.com/ws4vn/ezStore-microservices`
- Commit changes to your own branch
- Be sure to merge the latest from "upstream" before making a pull request!
- Push your work back up to your fork
- Submit a Pull request so that we can review your changes