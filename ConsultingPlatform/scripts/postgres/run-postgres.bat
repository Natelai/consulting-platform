@echo off
echo Starting PostgreSQL in Docker...

:: Check if the container already exists
docker ps -a --format "{{.Names}}" | findstr /C:"consulting_db" > nul
if %errorlevel% == 0 (
    echo Container "consulting_db" already exists.
    echo Starting it...
    docker start consulting_db
) else (
    echo Container does not exist, creating a new one...
    docker run --name consulting_db ^
        -e POSTGRES_USER=postgres ^
        -e POSTGRES_PASSWORD=1111 ^
        -e POSTGRES_DB=consulting_db ^
        -p 5433:5432 -d postgres
)

echo PostgreSQL started successfully!
pause
