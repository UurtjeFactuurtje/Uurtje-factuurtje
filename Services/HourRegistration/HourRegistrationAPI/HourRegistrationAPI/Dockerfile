#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

ARG SONAR_PROJECT_KEY=Service-HourRegistration
ARG SONAR_OGRANIZAION_KEY=uurtjefactuurtje
ARG SONAR_HOST_URL=https://sonarcloud.io
ARG SONAR_TOKEN

WORKDIR /src
COPY HourRegistration/HourRegistrationAPI/HourRegistrationAPI/ ./HourRegistration/HourRegistrationAPI/HourRegistrationAPI/
COPY HourRegistrationTest/ ./HourRegistration/HourRegistrationAPI/HourRegistrationAPI/
#COPY ["./HourRegistrationAPI/HourRegistrationAPI.csproj", "HourRegistrationAPI/"]

RUN mkdir -p /usr/share/man/man1
RUN apt-get update && apt-get install -y openjdk-11-jdk
RUN dotnet tool install --global dotnet-sonarscanner
RUN dotnet tool install --global coverlet.console
ENV PATH="$PATH:/root/.dotnet/tools"
  
RUN dotnet restore "HourRegistration/HourRegistrationAPI/HourRegistrationAPI/HourRegistrationAPI.csproj"
#COPY . .
WORKDIR "/src/HourRegistration/HourRegistrationAPI/HourRegistrationAPI"

RUN dotnet sonarscanner begin \
  /k:"$SONAR_PROJECT_KEY" \
  /o:"$SONAR_OGRANIZAION_KEY" \
  /d:sonar.host.url="$SONAR_HOST_URL" \
  /d:sonar.login="$SONAR_TOKEN" \
  /d:sonar.cs.opencover.reportsPaths=/coverage.opencover.xml
RUN dotnet build "HourRegistrationAPI.csproj" -c Release -o /app/build

#RUN ls ../../../
RUN dotnet test "HourRegistrationTest.csproj" -c Release --logger "trx;LogFileName=testresults.trx"
RUN dotnet sonarscanner end /d:sonar.login="$SONAR_TOKEN"

FROM build AS publish
RUN dotnet publish "HourRegistrationAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HourRegistrationAPI.dll"]