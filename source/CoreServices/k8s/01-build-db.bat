docker-compose -f docker-compose.init.yml build

docker tag microservices.identity.db ezstoremicroservices/microservices.identity.db
docker push ezstoremicroservices/microservices.identity.db

docker tag microservices.services.queue ezstoremicroservices/microservices.services.queue
docker push ezstoremicroservices/microservices.services.queue

docker tag microservices.services.db ezstoremicroservices/microservices.services.db
docker push ezstoremicroservices/microservices.services.db

docker tag microservices.services.redis ezstoremicroservices/microservices.services.redis
docker push ezstoremicroservices/microservices.services.redis