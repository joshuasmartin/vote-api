FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VoteWithYourWallet.Web/VoteWithYourWallet.Web.csproj", "VoteWithYourWallet.Web/"]
COPY ["VoteWithYourWallet.Common/VoteWithYourWallet.Common.csproj", "VoteWithYourWallet.Common/"]
RUN dotnet restore "VoteWithYourWallet.Web/VoteWithYourWallet.Web.csproj"
COPY . .
WORKDIR "/src/VoteWithYourWallet.Web"
RUN dotnet build "VoteWithYourWallet.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "VoteWithYourWallet.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VoteWithYourWallet.Web.dll"]
