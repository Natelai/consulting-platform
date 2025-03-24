@echo off
setlocal

echo Starting MailHog...
docker run --rm -d -p 1025:1025 -p 8025:8025 --name mailhog mailhog/mailhog

