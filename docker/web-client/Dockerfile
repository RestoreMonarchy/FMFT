FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY /web/Client/FMFT.Web.Client.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish web/Client/FMFT.Web.Client.csproj -c Release -o .

ENTRYPOINT ["echo", "Done."]