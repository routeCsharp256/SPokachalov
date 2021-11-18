FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build 

WORKDIR /src
COPY ["src/OzoneEdu.MerchandiseService/OzoneEdu.MerchandiseService.csproj","src/OzoneEdu.MerchandiseService/"]

RUN dotnet restore "src/OzoneEdu.MerchandiseService/OzoneEdu.MerchandiseService.csproj"
COPY . .

WORKDIR "/src/src/OzoneEdu.MerchandiseService"
RUN dotnet build "OzoneEdu.MerchandiseService.csproj" -c Releas -o /app/build

FROM build AS publish
RUN dotnet publish "OzoneEdu.MerchandiseService.csproj" -c Releas -o /app/publish
COPY "entrypoint.sh" "/app/publish/."


FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app

EXPOSE 5000
EXPOSE 5002

FROM runtime AS final
WORKDIR /app

COPY --from=publish /app/publish .

RUN chmod +x entrypoint.sh
CMD /bin/bash entrypoint.sh

#ENTRYPOINT ["dotnet","OzoneEdu.MerchandiseService.dll"]


