FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

COPY /web/Client/FMFT.Web.Client.csproj .
RUN dotnet restore FMFT.Web.Client.csproj
COPY . .
RUN dotnet build /web/client/FMFT.Web.Client.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish /web/client/FMFT.Web.Client.csproj -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY /docker/web-client/nginx.conf /etc/nginx/nginx.conf