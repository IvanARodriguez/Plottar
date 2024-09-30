# Makefile for Docker operations

# Variables
PROJECT_NAME = api
DOCKER_COMPOSE_FILE = compose.yml

.PHONY: build
build:
	docker-compose -f $(DOCKER_COMPOSE_FILE) build api

.PHONY: up
up:
	docker-compose -f $(DOCKER_COMPOSE_FILE) up api

.PHONY: build-prod
build-prod:
	docker-compose -f $(DOCKER_COMPOSE_FILE) build api-prod

.PHONY: up-prod
up-prod:
	docker-compose -f $(DOCKER_COMPOSE_FILE) up -d api-prod

.PHONY: down
down:
	docker-compose -f $(DOCKER_COMPOSE_FILE) down

.PHONY: logs
logs:
	docker-compose -f $(DOCKER_COMPOSE_FILE) logs -f

.PHONY: clean
clean:
	docker-compose -f $(DOCKER_COMPOSE_FILE) down --rmi all --volumes --remove-orphans
