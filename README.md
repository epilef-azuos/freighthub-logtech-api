# FreightHub API 🚛

<p align="left">
  <img src="https://img.shields.io/badge/.NET_10-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 10" />
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#" />
  <img src="https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white" alt="Docker" />
  <img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" alt="PostgreSQL" />
  <img src="https://img.shields.io/badge/MongoDB-4EA94B?style=for-the-badge&logo=mongodb&logoColor=white" alt="MongoDB" />
  <img src="https://img.shields.io/badge/RabbitMQ-FF6600?style=for-the-badge&logo=rabbitmq&logoColor=white" alt="RabbitMQ" />
</p>

Uma API Logtech de nível corporativo construída em **.NET 10** utilizando a arquitetura de **Monólito Modular (Modular Monolith)**. 
Este projeto demonstra padrões avançados de engenharia de software focados em escalabilidade, resiliência e consistência eventual para o ecossistema logístico.

## 🚀 Tecnologias e Padrões (Stack)

*   **Framework:** .NET 10 (C# 13)
*   **Arquitetura:** Clean Architecture, Modular Monolith
*   **Padrões de Design:** CQRS, Event-Driven Architecture, Transactional Outbox, Rich Domain Model, State Machine
*   **Persistência (Escrita):** PostgreSQL 16 + Entity Framework Core 10 (Design Configurations)
*   **Persistência (Leitura):** MongoDB 7.0 (Projeções) *[Em Breve]*
*   **Mensageria:** RabbitMQ + MassTransit *[Em Breve]*
*   **Segurança:** Keycloak (OIDC/OAuth2)
*   **Mediação & Validação:** MediatR, FluentValidation
*   **Infraestrutura & Observabilidade:** Docker Compose, Aspire Dashboard, WireMock, Scalar (OpenAPI)
*   **CI/CD:** GitHub Actions (Pipelines de Build, Test e Linting)

## 🏗️ Desenho Arquitetural (C4 Model - Nível Contêiner)

```mermaid
flowchart TD
    Client([Cliente / Scalar])
    Keycloak{{Keycloak OIDC}}
    
    subgraph API [FreightHub.Api .NET 10]
        ShipmentsM[Módulo Shipments]
    end

    PgSQL[(PostgreSQL Write DB \n + Outbox Table)]
    RabbitMQ(((RabbitMQ \n MassTransit)))
    MongoDB[(MongoDB Read DB)]
    Worker[FreightHub.Worker]

    Client -- 1. Autentica --> Keycloak
    Client -- 2. POST / Comandos --> API
    API -- 3. Grava Estado e Evento --> PgSQL
    PgSQL -- 4. Publica Eventos --> RabbitMQ
    RabbitMQ -- 5. Consome Eventos --> Worker
    Worker -- 6. Atualiza Projeções --> MongoDB
    Client -- GET / Queries --> MongoDB
```

## ⚙️ Como Executar Localmente

**1. Subir a Infraestrutura (Docker)**
Na raiz do projeto, inicialize os bancos de dados, mensageria e observabilidade:
```bash
docker compose up -d
```

**2. Executar a Aplicação (API)**
```bash
dotnet run --project src/FreightHub.Api
```

A documentação interativa (Scalar/OpenAPI) estará disponível em `http://localhost:5205/scalar/v1`.

---
*Desenvolvido por [Felipe Assis De Souza](https://www.linkedin.com/in/felipeassissouza/) | [GitHub](https://github.com/epilef-azuos)*