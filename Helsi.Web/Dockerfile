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

FROM build AS publish
RUN dotnet publish "Helsi.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Helsi.Web.dll"]
