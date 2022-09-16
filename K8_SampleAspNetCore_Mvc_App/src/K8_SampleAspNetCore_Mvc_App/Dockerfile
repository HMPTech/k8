#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001
ENv ASPNETCORE_URLS=http://*:5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "K8_SampleAspNetCore_Mvc_App.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "K8_SampleAspNetCore_Mvc_App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "K8_SampleAspNetCore_Mvc_App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "K8_SampleAspNetCore_Mvc_App.dll"]