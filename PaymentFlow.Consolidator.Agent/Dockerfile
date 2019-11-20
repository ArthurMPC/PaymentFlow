FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["PaymentFlow.Consolidator.Agent/PaymentFlow.Consolidator.Agent.csproj", "PaymentFlow.Consolidator.Agent/"]
COPY ["PaymentFlow.Domain.IoC/PaymentFlow.Domain.IoC.csproj", "PaymentFlow.Domain.IoC/"]
COPY ["PaymentFlow.Infra.Data/PaymentFlow.Infra.Data.csproj", "PaymentFlow.Infra.Data/"]
COPY ["PaymentFlow.Domain/PaymentFlow.Domain.csproj", "PaymentFlow.Domain/"]
RUN dotnet restore "PaymentFlow.Consolidator.Agent/PaymentFlow.Consolidator.Agent.csproj"
COPY . .
WORKDIR "/src/PaymentFlow.Consolidator.Agent"
RUN dotnet build "PaymentFlow.Consolidator.Agent.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PaymentFlow.Consolidator.Agent.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PaymentFlow.Consolidator.Agent.dll"]