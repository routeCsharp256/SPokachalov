FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build 

WORKDIR /src
COPY ["OzonEdu.MerchandiseService/src/OzoneEdu.MerchandiseService/OzoneEdu.MerchandiseService.csproj","src/OzoneEdu.MerchandiseService/"]

RUN dotnet restore "src/OzoneEdu.MerchandiseService/OzoneEdu.MerchandiseService.csproj"
COPY . .

WORKDIR "/src/OzonEdu.MerchandiseService/src/OzoneEdu.MerchandiseService"
RUN dotnet build "OzoneEdu.MerchandiseService.csproj" -c Releas -o /app/build

FROM build AS publish
RUN dotnet publish "OzoneEdu.MerchandiseService.csproj" -c Releas -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM runtime AS final

WORKDIR /app
COPY --from=publish app/publish .
ENTRYPOINT ["dotnet","OzoneEdu.MerchandiseService.dll"]


