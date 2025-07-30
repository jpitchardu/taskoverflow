# Docker commands
start:
	docker-compose up --build

stop:
	docker-compose down

reset:
	docker-compose down -v && docker-compose up --build

test-server:
	cd server && ASPNETCORE_ENVIRONMENT=Testing dotnet test

test-client:
	cd client && pnpm test

test: test-server test-client

.PHONY: start stop  reset test-server test-client test
