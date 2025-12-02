.PHONY: init build up down logs dev prod

init: build up logs

build:
	docker compose build

dev-build:
	docker compose -f docker-compose.dev.yml build

up:
	docker compose up -d

dev:
	docker-compose -f docker-compose.dev.yml up -d

down-dev:
	docker-compose -f docker-compose.dev.yml down

logs:
	docker compose logs -f api

dev-logs:
	docker compose -f docker-compose.dev.yml logs -f api

restart-api:
	docker-compose -f docker-compose.dev.yml restart api

rebuild-api:
	docker-compose -f docker-compose.dev.yml build api
	docker-compose -f docker-compose.dev.yml up -d api

rebuild: down-dev dev-build dev

status-dev:
	docker-compose -f docker-compose.dev.yml ps

stop-dev:
	docker-compose -f docker-compose.dev.yml stop
