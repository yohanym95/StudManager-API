FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80


COPY StudManager.sln ./
COPY ["StudManager/StudManager.csproj", "StudManager/"]
COPY ["StudManager.Data/StudManager.Data.csproj", "StudManager.Data/"]


RUN dotnet restore 
COPY .StudManager/. ./StudManager/
COPY .StudManager.Data/. ./StudManager.Data/

WORKDIR /app/StudManager
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS runtime
WORKDIR /app

COPY --from=build /app/StudManager/out ./
ENTRYPOINT ["dotnet", "StudManager.dll"]