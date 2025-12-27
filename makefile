DOCKER_COMPOSE = docker compose
COMPOSE_FILE = docker-compose.yml
SERVICE_API = api
SERVICE_DB = postgres
SERVICE_REDIS = redis

init: build up logs

build:
	$(DOCKER_COMPOSE) build

up:
	$(DOCKER_COMPOSE) up -d

down:
	$(DOCKER_COMPOSE) down

down-v:
	$(DOCKER_COMPOSE) down -v

logs:
	$(DOCKER_COMPOSE) logs -f $(SERVICE_API)

logs-all:
	$(DOCKER_COMPOSE) logs -f

logs-db:
	$(DOCKER_COMPOSE) logs -f $(SERVICE_DB)

logs-redis:
	$(DOCKER_COMPOSE) logs -f $(SERVICE_REDIS)

restart:
	$(DOCKER_COMPOSE) restart $(SERVICE_API)

rebuild: down build up

rebuild-api:
	$(DOCKER_COMPOSE) up -d --build $(SERVICE_API)

status:
	$(DOCKER_COMPOSE) ps

stop:
	$(DOCKER_COMPOSE) stop

recreate-api:
	$(DOCKER_COMPOSE) up -d --force-recreate $(SERVICE_API)

exec-api:
	$(DOCKER_COMPOSE) exec $(SERVICE_API) bash

exec-db:
	$(DOCKER_COMPOSE) exec $(SERVICE_DB) psql -U root crm_db

exec-redis:
	$(DOCKER_COMPOSE) exec $(SERVICE_REDIS) redis-cli -a redis_password

clean:
	$(DOCKER_COMPOSE) down --remove-orphans --volumes

clean-redis:
	$(DOCKER_COMPOSE) exec $(SERVICE_REDIS) redis-cli -a redis_password FLUSHDB

urls:
	@echo "API: http://localhost:8080"
	@echo "API Swagger: http://localhost:8080/swagger"
	@echo "PgAdmin: http://localhost:5050"
	@echo "Redis Commander: http://localhost:8082"

help:
	@echo "Available commands:"
	@echo "  make init        - Build and start all services"
	@echo "  make up          - Start services"
	@echo "  make down        - Stop services"
	@echo "  make down-v      - Stop services and remove volumes"
	@echo "  make logs        - Show API logs"
	@echo "  make logs-all    - Show all logs"
	@echo "  make logs-db     - Show database logs"
	@echo "  make logs-redis  - Show Redis logs"
	@echo "  make restart     - Restart API service"
	@echo "  make rebuild     - Rebuild and restart all"
	@echo "  make rebuild-api - Rebuild and restart API"
	@echo "  make status      - Show services status"
	@echo "  make stop        - Stop services"
	@echo "  make recreate-api- Force recreate API service"
	@echo "  make exec-api    - Access API container shell"
	@echo "  make exec-db     - Access database shell"
	@echo "  make exec-redis  - Access Redis CLI"
	@echo "  make clean       - Stop and remove all"
	@echo "  make clean-redis - Clean Redis database"
	@echo "  make urls        - Show all service URLs"
	@echo "  make help        - Show this help"