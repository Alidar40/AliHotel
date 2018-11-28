FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY AliHotel.sln ./
COPY AliHotel.Web/AliHotel.Web.csproj AliHotel.Web/
COPY AliHotel.BackgroundTasks/AliHotel.BackgroundTasks.csproj AliHotel.BackgroundTasks/
COPY AliHotel.Domain/AliHotel.Domain.csproj AliHotel.Domain/
COPY AliHotel.Domain.Entities/AliHotel.Domain.Entities.csproj AliHotel.Domain.Entities/
COPY AliHotel.Database/AliHotel.Database.csproj AliHotel.Database/
COPY AliHotel.Identity/AliHotel.Identity.csproj AliHotel.Identity/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/AliHotel.Web
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AliHotel.Web.dll"]