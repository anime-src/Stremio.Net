FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Hanime.Stremio.Net.csproj", "./"]
RUN dotnet restore "Hanime.Stremio.Net.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Hanime.Stremio.Net.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hanime.Stremio.Net.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hanime.Stremio.Net.dll"]
