# Inicialização
dotnet new sln -n FreightHub
mkdir src tests docs infra perf
cd src

# Host e Worker
dotnet new webapi -n FreightHub.Api
dotnet new worker -n FreightHub.Worker

# Building Blocks (Núcleo compartilhado)
mkdir BuildingBlocks && cd BuildingBlocks
dotnet new classlib -n FreightHub.BuildingBlocks.Domain
dotnet new classlib -n FreightHub.BuildingBlocks.Application
dotnet new classlib -n FreightHub.BuildingBlocks.Infrastructure
cd ..

# Módulo Shipments
mkdir Modules && cd Modules
mkdir Shipments && cd Shipments
dotnet new classlib -n FreightHub.Shipments.Domain
dotnet new classlib -n FreightHub.Shipments.Application
dotnet new classlib -n FreightHub.Shipments.Infrastructure
dotnet new classlib -n FreightHub.Shipments.Contracts
cd ../../../

# Adicionar à Solution (Executar na raiz do projeto)
dotnet sln add src/FreightHub.Api
dotnet sln add src/FreightHub.Worker
dotnet sln add src/BuildingBlocks/FreightHub.BuildingBlocks.Domain
dotnet sln add src/BuildingBlocks/FreightHub.BuildingBlocks.Application
dotnet sln add src/BuildingBlocks/FreightHub.BuildingBlocks.Infrastructure
dotnet sln add src/Modules/Shipments/FreightHub.Shipments.Domain
dotnet sln add src/Modules/Shipments/FreightHub.Shipments.Application
dotnet sln add src/Modules/Shipments/FreightHub.Shipments.Infrastructure
dotnet sln add src/Modules/Shipments/FreightHub.Shipments.Contracts

# Configuração rigorosa de dependências (Regra de Ouro)
dotnet add src/Modules/Shipments/FreightHub.Shipments.Domain reference src/BuildingBlocks/FreightHub.BuildingBlocks.Domain
dotnet add src/Modules/Shipments/FreightHub.Shipments.Application reference src/Modules/Shipments/FreightHub.Shipments.Domain
dotnet add src/Modules/Shipments/FreightHub.Shipments.Infrastructure reference src/Modules/Shipments/FreightHub.Shipments.Application