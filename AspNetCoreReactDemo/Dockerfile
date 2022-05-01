# escape=`
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/powershell:lts-nanoserver-2004 AS downloadnodejs
ENV NODE_VERSION=16.15.0
SHELL ["pwsh", "-Command"]
RUN Invoke-WebRequest -OutFile nodejs.zip -UseBasicParsing "https://nodejs.org/dist/v$($env:NODE_VERSION)/node-v$($env:NODE_VERSION)-win-x64.zip"; `
    Expand-Archive nodejs.zip -DestinationPath C:\; `
    Rename-Item "C:\node-v$($env:NODE_VERSION)-win-x64" c:\nodejs

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=downloadnodejs C:\nodejs\ C:\Windows\system32\

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
COPY --from=downloadnodejs C:\nodejs\ C:\Windows\system32\
WORKDIR /src
COPY ["AspNetCoreReactDemo/AspNetCoreReactDemo.csproj", "AspNetCoreReactDemo/"]
RUN dotnet restore "AspNetCoreReactDemo/AspNetCoreReactDemo.csproj"
COPY . .
WORKDIR "/src/AspNetCoreReactDemo"
RUN dotnet build "AspNetCoreReactDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetCoreReactDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCoreReactDemo.dll"]