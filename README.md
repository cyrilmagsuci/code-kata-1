# Coding Exam for VISA

http://codekata.com/kata/kata09-back-to-the-checkout/

## Setup Instructions

### 1. Start the application using Docker Compose


Run the following command from the project root directory:

```bash
docker compose up -d
```

This will build and start the containers in the background.

### 2. Run Database Migrations

After the containers have started, especially the database container, you need to apply the database migrations.

Open a terminal, navigate to the infrastructure project folder, and run the following command:

```bash
cd src/Infrastructure
dotnet ef database update
```
