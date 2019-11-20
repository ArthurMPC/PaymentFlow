FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["PaymentFlow.Service.API/PaymentFlow.Service.API.csproj", "PaymentFlow.Service.API/"]
COPY ["PaymentFlow.Domain.IoC/PaymentFlow.Domain.IoC.csproj", "PaymentFlow.Domain.IoC/"]
COPY ["PaymentFlow.Infra.Data/PaymentFlow.Infra.Data.csproj", "PaymentFlow.Infra.Data/"]
COPY ["PaymentFlow.Domain/PaymentFlow.Domain.csproj", "PaymentFlow.Domain/"]
COPY ["PaymentFlow.Application/PaymentFlow.Application.csproj", "PaymentFlow.Application/"]
RUN dotnet restore "PaymentFlow.Service.API/PaymentFlow.Service.API.csproj"
COPY . .
WORKDIR "/src/PaymentFlow.Service.API"
RUN dotnet build "PaymentFlow.Service.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PaymentFlow.Service.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PaymentFlow.Service.API.dll"]