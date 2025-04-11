# Makefile for BrassAndPoem project
ROOT_DIR := $(dir $(abspath $(lastword $(MAKEFILE_LIST))))

# Project name
PROJECT_NAME = BrassAndPoem

# Project directory
PROJECT_DIR = .

# Test Suite
TEST_PROJECT_DIR = BrassAndPoem.Tests

# Build configuration
CONFIGURATION = Release

# .NET Core version
DOTNET_VERSION = $(shell dotnet --version)

# Default target
all: build

# Build target
build:
	dotnet build $(PROJECT_DIR)/$(PROJECT_NAME).csproj -c $(CONFIGURATION)

# Test target
test tests:
	cd $(ROOT_DIR) && dotnet test $(TEST_PROJECT_DIR)/$(PROJECT_NAME).Tests.csproj

# Run target
run:
	dotnet run --project $(PROJECT_DIR)/$(PROJECT_NAME).csproj

# Publish target
publish:
	dotnet publish $(PROJECT_DIR)/$(PROJECT_NAME).csproj -c $(CONFIGURATION)

# Clean target
clean:
	dotnet clean $(PROJECT_DIR)/$(PROJECT_NAME).csproj

# Restore target
restore:
	dotnet restore $(PROJECT_DIR)/$(PROJECT_NAME).csproj

# Help target
help:
	@echo "Makefile for $(PROJECT_NAME) project"
	@echo ""
	@echo "Targets:"
	@echo "  all      : Build the project"
	@echo "  build    : Build the project"
	@echo "  test     : Run tests"
	@echo "  run      : Run the project"
	@echo "  publish  : Publish the project"
	@echo "  clean    : Clean the project directory"
	@echo "  restore  : Restore NuGet packages"
	@echo "  help     : Display this help message"

# .PHONY target
.PHONY: all build test run publish clean restore help