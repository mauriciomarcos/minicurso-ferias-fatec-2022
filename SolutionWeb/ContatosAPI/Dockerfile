FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR	/app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["./ContatosAPI/ContatosAPI.csproj", "ContatosAPI/"]

RUN dotnet restore "ContatosAPI/ContatosAPI.csproj"
COPY . .
WORKDIR "ContatosAPI"
RUN dotnet build "ContatosAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContatosAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContatosAPI.dll"]