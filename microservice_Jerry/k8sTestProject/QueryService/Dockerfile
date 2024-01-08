FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY QueryService/*.csproj QueryService/
COPY CommonService/*.csproj CommonService/
RUN dotnet restore "QueryService/QueryService.csproj"

COPY . ./
RUN dotnet publish "QueryService/QueryService.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet","QueryService.dll" ]

# Docker在第一堂的32分鐘提到
# 第一堂的74分鐘開始講這邊
# 一個project就是一個微服務，CommomService和QueryService都是微服務，Dockerfile一定放在project底下，但要建image是要在根目錄(k8sTestProject)