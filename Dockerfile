FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /build
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
#EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:80 DOTNET_RUNNING_IN_CONTAINER=true
COPY --from=build /app .

ENTRYPOINT ["dotnet", "CorrecoesMgr.Api.dll"]
