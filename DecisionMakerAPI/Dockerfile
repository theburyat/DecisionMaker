FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DecisionMakerAPI/DecisionMakerAPI.csproj", "DecisionMakerAPI/"]
COPY ["Models/Models.csproj", "Models/"]
RUN dotnet restore "DecisionMakerAPI/DecisionMakerAPI.csproj"
COPY . .
WORKDIR "/src/DecisionMakerAPI"
RUN dotnet build "DecisionMakerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DecisionMakerAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DecisionMakerAPI.dll"]
