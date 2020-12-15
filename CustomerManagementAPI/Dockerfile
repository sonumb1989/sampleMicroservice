FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base  
WORKDIR /app  
EXPOSE 80  
EXPOSE 443  

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build  
WORKDIR /src  
COPY ["CustomerManagementAPI/CustomerManagementAPI.csproj", "CustomerManagementAPI/"]  
COPY ["Infrastructure.Messaging/Infrastructure.Messaging.csproj", "Infrastructure.Messaging/"]  
RUN dotnet restore "CustomerManagementAPI/CustomerManagementAPI.csproj"  
COPY . .  
WORKDIR "/src/CustomerManagementAPI"  
RUN dotnet build "CustomerManagementAPI.csproj" -c Release -o /app/build  

FROM build AS publish  
RUN dotnet publish "CustomerManagementAPI.csproj" -c Release -o /app/publish 

FROM base AS final  
WORKDIR /app  
COPY --from=publish /app/publish .  
ENTRYPOINT ["dotnet", "CustomerManagementAPI.dll", "--environment=Development"]  
