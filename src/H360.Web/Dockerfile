#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/H360.Web/H360.Web.csproj", "src/H360.Web/"]
RUN dotnet restore "src/H360.Web/H360.Web.csproj"
COPY . .
WORKDIR "/src/src/H360.Web"
RUN dotnet build "H360.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "H360.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "H360.Web.dll"]