docker stop stockmarket
docker rm stockmarket
docker build -t stockmarket.api .
docker run --rm -p 8000:5000 -e Logging__Loglevel__Default=Debug -e Logging__Loglevel__Microsoft.AspNetCore=Debug stockmarket.api
pause