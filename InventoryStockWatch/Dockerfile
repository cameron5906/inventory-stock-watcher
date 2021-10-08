FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["InventoryStockWatch/InventoryStockWatch.csproj", "InventoryStockWatch/"]
RUN dotnet restore "InventoryStockWatch/InventoryStockWatch.csproj"
COPY . .
WORKDIR "/src/InventoryStockWatch"
RUN dotnet build "InventoryStockWatch.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InventoryStockWatch.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryStockWatch.dll"]
