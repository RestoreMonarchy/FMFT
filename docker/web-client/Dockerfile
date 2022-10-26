FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY /web/Client/FMFT.Web.Client.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish web/Client/FMFT.Web.Client.csproj -c Release -o out

WORKDIR /app
COPY /app/out .
ENTRYPOINT ["echo", "Done."]