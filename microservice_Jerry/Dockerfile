# 第一階段建置
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app
COPY ./console_docker/ ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

#第二階段建置
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet","console_docker.dll"]
