FROM microsoft/aspnetcore-build
WORKDIR /dotnetapp
COPY . .
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT dotnet Fibonacci.Service.dll --urls "https://*:5050"