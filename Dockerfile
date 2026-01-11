FROM mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /src

COPY *.sln .
COPY *.csproj .

RUN dotnet restore

COPY . .

EXPOSE 8080
EXPOSE 8081

CMD ["dotnet", "watch", "run", "--project", "CrmCore.csproj", "--urls=http://0.0.0.0:8080", "--non-interactive"]
