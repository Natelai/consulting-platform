@echo off
echo Starting MongoDB in Docker...

:: Check if the container already exists
docker ps -a --format "{{.Names}}" | findstr /C:"consulting_mongo" > nul
if %errorlevel% == 0 (
    echo Container "consulting_mongo" already exists.
    echo Starting it...
    docker start consulting_mongo
) else (
    echo Container does not exist, creating a new one...
    docker run --name consulting_mongo ^
        -e MONGO_INITDB_ROOT_USERNAME=mongoadmin ^
        -e MONGO_INITDB_ROOT_PASSWORD=secret ^
        -e MONGO_INITDB_DATABASE=consulting ^
        -p 27020:27017 ^
        -v %cd%\mongo_data:/data/db ^
        -d mongo
)

echo MongoDB started successfully!
pause
