FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY AxCluster/AxCluster.csproj AxCluster/
RUN dotnet restore AxCluster/AxCluster.csproj
COPY . .
WORKDIR /src/AxCluster
RUN dotnet build AxCluster.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish AxCluster.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AxCluster.dll"]
