FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ASPNETWebApiDemo/ASPNETWebApiDemo.csproj", "ASPNETWebApiDemo/"]
RUN dotnet restore "ASPNETWebApiDemo/ASPNETWebApiDemo.csproj"
COPY . .
WORKDIR "/src/ASPNETWebApiDemo"
RUN dotnet build "ASPNETWebApiDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ASPNETWebApiDemo.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish
ENTRYPOINT ["dotnet", "ASPNETWebApiDemo.dll"]
