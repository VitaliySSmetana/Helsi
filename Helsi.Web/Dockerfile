#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see http://aka.ms/containercompat 

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1809 AS build
WORKDIR /src
COPY ["Helsi.Web/Helsi.Web.csproj", "Helsi.Web/"]
COPY ["Helsi.DataAccess/Helsi.DataAccess.csproj", "Helsi.DataAccess/"]
COPY ["Helsi.Core/Helsi.Core.csproj", "Helsi.Core/"]
COPY ["Helsi.DomainLogic/Helsi.DomainLogic.csproj", "Helsi.DomainLogic/"]
COPY ["Helsi.Dto/Helsi.Dto.csproj", "Helsi.Dto/"]
RUN dotnet restore "Helsi.Web/Helsi.Web.csproj"
COPY . .
WORKDIR "/src/Helsi.Web"
RUN dotnet build "Helsi.Web.csproj" -c Release -o /app

FROM mcr.microsoft.com/windows/servercore:ltsc2019 AS installer
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ARG ES_VERSION="5.6.11"
ENV ES_HOME="C:\elasticsearch" `
    ES_ROOT_URL="https://artifacts.elastic.co/downloads/elasticsearch/elasticsearch"

RUN Write-Host "Downloading: $env:ES_ROOT_URL-$env:ES_VERSION.zip"; `
    Invoke-WebRequest "$env:ES_ROOT_URL-$env:ES_VERSION.zip.sha512" -OutFile 'elasticsearch.zip.sha' -UseBasicParsing; `
    $env:ES_SHA = (Get-Content -Raw elasticsearch.zip.sha).Split(' ')[0]; `
    Invoke-WebRequest "$env:ES_ROOT_URL-$env:ES_VERSION.zip" -OutFile 'elasticsearch.zip' -UseBasicParsing; `
    if ((Get-FileHash elasticsearch.zip -Algorithm sha512).Hash.ToLower() -ne $env:ES_SHA) {exit 1}; `
    Expand-Archive elasticsearch.zip -DestinationPath C:\ ; `
    Move-Item c:/elasticsearch-$($env:ES_VERSION) $env:ES_HOME;

# Elasticsearch
FROM openjdk:8-windowsservercore-1809
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop';"]

ARG ES_VERSION="5.6.11"
ENV ES_VERSION=${ES_VERSION} `
    ES_HOME="C:\elasticsearch" `
    ES_JAVA_OPTS="-Xms1024m -Xmx1024m"

VOLUME C:\data
EXPOSE 9200 9300

SHELL ["cmd", "/S", "/C"]
CMD ".\bin\elasticsearch.bat"

WORKDIR $ES_HOME
COPY --from=installer C:\elasticsearch\ .
COPY config ./config

HEALTHCHECK --interval=5s `
 CMD powershell -command `
    try { `
     $content = (iwr -useb http://localhost:9200/_cat/health).Content; `
     $health = $content.Split(' ')[3]; `
     if ($health -eq 'green' -or $health -eq 'yellow') { return 0 } `
     else { return 1 }; `
    } catch { return 1 }

VOLUME c:/data
RUN set-itemproperty -path 'HKLM:\SYSTEM\CurrentControlSet\Control\Session Manager\DOS Devices' -Name 'G:' -Value '\??\C:\data' -Type String

FROM build AS publish
RUN dotnet publish "Helsi.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Helsi.Web.dll"]
