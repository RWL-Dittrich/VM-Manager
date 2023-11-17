FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["vm-manager/vm-manager.csproj", "vm-manager/"]
RUN dotnet restore "vm-manager/vm-manager.csproj"
COPY . .
WORKDIR "/src/vm-manager"
RUN dotnet build "vm-manager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "vm-manager.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vm-manager.dll"]
