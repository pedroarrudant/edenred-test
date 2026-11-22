FROM mcr.microsoft.com/dotnet/sdk:11.0 AS build
WORKDIR /src
COPY EdenredTest/EdenredTest.csproj EdenredTest/
COPY EdenredTest.Application/EdenredTest.Application.csproj EdenredTest.Application/
RUN dotnet restore EdenredTest/EdenredTest.csproj
COPY EdenredTest/ EdenredTest/
COPY EdenredTest.Application/ EdenredTest.Application/
RUN dotnet publish EdenredTest/EdenredTest.csproj -c Release --no-restore -o /app

FROM mcr.microsoft.com/dotnet/aspnet:11.0
WORKDIR /app
COPY --from=build /app ./
USER $APP_UID
ENTRYPOINT ["dotnet", "EdenredTest.dll"]
