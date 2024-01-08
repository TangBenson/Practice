FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY TransService/*.csproj TransService/
COPY CommonService/*.csproj CommonService/
RUN dotnet restore "TransService/TransService.csproj"

COPY . ./
RUN dotnet publish "TransService/TransService.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet","TransService.dll" ]
