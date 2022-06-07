# Defini��o das vers�es
ARG VERSION_BASE=6.0-alpine3.14
ARG VERSION_BUILD=6.0-alpine3.14

FROM mcr.microsoft.com/dotnet/aspnet:$VERSION_BASE AS base
LABEL maintainer="Felipe Bernardi <Felipe100bernardi@hotmail.com>"
WORKDIR /app

# Defini��o Vari�veis de Ambiente
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:5000;
ENV ACCESS_SERVICE_DB=Host=localhost;Database=control_access;Username=access;Password=aP3fVui2G7EiVNHevS5L8ZysZoabyP1fJ
ENV REDIS_SERVER=172.17.0.1
ENV REDIS_PASSWORD=kinTivEtprEs
ENV REDIS_SERVER_PORT=6379

# Open port
EXPOSE 5000

# Compilar os componentes da aplica��o
FROM mcr.microsoft.com/dotnet/sdk:$VERSION_BUILD AS build
WORKDIR /src
COPY ["Touchless.Access.Services.Api/Touchless.Access.Services.Api.csproj", "Touchless.Access.Services.Api/"]
RUN true
COPY ["Touchless.Access.Exception/Touchless.Access.Exception.csproj", "Touchless.Access.Exception/"]
RUN true
COPY ["Touchless.Access.Repository/Touchless.Access.Repository.csproj", "Touchless.Access.Repository/"]
RUN true
COPY ["Touchless.Access.Data/Touchless.Access.Data.csproj", "Touchless.Access.Data/"]
RUN true
COPY ["Touchless.Access.Services.Common/Touchless.Access.Services.Common.csproj", "Touchless.Access.Services.Common/"]
RUN true
COPY ["Touchless.Access.Common/Touchless.Access.Common.csproj", "Touchless.Access.Common/"]
RUN true
COPY ["Touchless.Access.Pagination/Touchless.Access.Pagination.csproj", "Touchless.Access.Pagination/"]
RUN true
COPY ["Touchless.Access.Api.Security/Touchless.Access.Api.Security.csproj", "Touchless.Access.Api.Security/"]
RUN true
COPY ["Touchless.Access.Authentication/Touchless.Access.Authentication.csproj", "Touchless.Access.Authentication/"]
RUN true

RUN dotnet restore "Touchless.Access.Services.Api/Touchless.Access.Services.Api.csproj"
COPY . .
RUN true
WORKDIR "/src/Touchless.Access.Services.Api"

RUN dotnet build "Touchless.Access.Services.Api.csproj" \
	-c Release \
	-o /app/build

FROM build AS publish
RUN dotnet publish "Touchless.Access.Services.Api.csproj" \
	-c Release \
	-o /app/publish

# Publicar a aplica��o para uso
FROM base AS final

# Atualizar Alpine
RUN apk upgrade musl

WORKDIR /app
COPY --from=publish /app/publish .
RUN true
COPY ["Touchless.Access.Services.Api/appsettings.json", "/app/appsettings.json"]
RUN true
COPY ["Touchless.Access.Services.Api/Touchless.Access.Services.Api.xml", "/app/Touchless.Access.Services.Api.xml"]
RUN true
ENTRYPOINT ["dotnet", "Touchless.Access.Services.Api.dll"]