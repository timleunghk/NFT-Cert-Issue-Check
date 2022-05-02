#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN apt-get update -y && apt-get install -y libgdiplus && apt-get clean && ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 9001
EXPOSE 9443


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NFT-Cert-Issue-Check.csproj", "."]
RUN dotnet restore "./NFT-Cert-Issue-Check.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "NFT-Cert-Issue-Check.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NFT-Cert-Issue-Check.csproj" -c Release -o /app/publish




FROM base AS final
WORKDIR /app







COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NFT-Cert-Issue-Check.dll"]