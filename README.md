# AssetLiquidation
AssetLiquidation is a sample backend application designed to simulate financial asset liquidation. It manages asset balances by processing payment and reversal events asynchronously via RabbitMQ messaging.


# Architecture
Following Clean Architecture principles and the separation of responsibilities, the project will be structured as follows:

* Solution (.sln): Project container.
* Core / Domain (Class Library): Contains the entities (Asset, Occurrence), interfaces, and pure business rules in C#.
* API (Web API): Controllers, REST routes, Swagger, and dependency injection.
* Worker / Infrastructure (Worker Service): Background service responsible for consuming RabbitMQ messages and processing asset liquidation.
* Tests (xUnit): Unit test project.


# Architecture - code
2. O que vamos codificar nesta Branch
Nesta branch feature/setup-arquitetura, a meta é colocar a estrutura de código C# pronta para compilar. Vamos criar os seguintes arquivos na biblioteca LiquidaAtivos.Core:

* Enums.cs: Definição dos tipos (TipoOcorrencia, StatusAtivo, StatusProcessamento).
* Ativo.cs: Entidade do Ativo com propriedades e validações de saldo.
* Ocorrencia.cs: Entidade de Pagamento/Estorno.
* LiquidaDbContext.cs: Contexto do Entity Framework Core.