FROM microsoft/aspnetcore-build
WORKDIR /dotnetapp
COPY . .
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT dotnet Fibonacci.Api.dll --urls "https://*:5000"