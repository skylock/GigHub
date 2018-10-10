FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 55132
EXPOSE 44303

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["GigHub/GigHub.csproj", "GigHub/"]
RUN dotnet restore "GigHub/GigHub.csproj"
COPY . .
WORKDIR "/src/GigHub"
RUN dotnet build "GigHub.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "GigHub.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GigHub.dll"]