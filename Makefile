# Docker commands
start:
	docker-compose up -d

stop:
	docker-compose down

logs:
	docker-compose logs -f api

rebuild:
	docker-compose build api
	docker-compose up -d api

reset:
	docker-compose down -v && docker-compose up -d

# Build and Test commands
restore-server:
	@echo "🔄 Restoring packages..."
	@dotnet restore --no-cache

clean:
	cd  server
	@echo "🧹 Cleaning solution..."
	@dotnet clean
	@find . -name "bin" -type d -exec rm -rf {} + 2>/dev/null || true
	@find . -name "obj" -type d -exec rm -rf {} + 2>/dev/null || true



build-server:
	cd server
	@echo "🔨 Building solution..."
	@dotnet build --no-restore --configuration Release

test-server:
	cd server
	@echo "🧪 Running tests..."
	@dotnet test --configuration Release --verbosity minimal
	@echo "✅ All tests passed!"

# Full CI pipeline
ci: clean restore build-solution test
	@echo "✅ All builds and tests completed successfully!"

pre-push: ci
	@echo "🚀 Pre-push validation completed - ready to push!"


.PHONY: start stop logs rebuild reset-server restore-server clean-server build-server test-server  ci pre-push 
