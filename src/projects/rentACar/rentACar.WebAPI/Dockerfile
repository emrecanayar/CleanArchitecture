#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal As build
WORKDIR /source
COPY . .
RUN dotnet restore rentACar.WebAPI.csproj --disable-parallel
RUN dotnet publish rentACar.WebAPI.csproj -c release -o out

#Serve Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5252
ENTRYPOINT ["dotnet", "rentACar.WebAPI.dll"]