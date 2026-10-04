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





Para acelerar e consolidar esse aprendizado, existem 3 pilares práticos:

Pilar 1: Agrupar por "Caixas de Responsabilidade" (Apenas 4 caixas!)
Em vez de tentar memorizar centenas de métodos isolados (AnyAsync, AsNoTracking, CreatedAtAction, AddDbContext), agrupe-os pelo papel funcional que desempenham:
Caixa HTTP / Web API (ASP.NET Core):
O que faz: Trata a entrada e a saída de requisições web.
Membros: ControllerBase, [HttpPost], [HttpGet], IActionResult, Ok(), Conflict(), CreatedAtAction().
Caixa ORM / Acesso a Dados (Entity Framework Core):
O que faz: Conversa com o banco de dados.
Membros: DbContext, DbSet<T>, SaveChangesAsync(), AsNoTracking().
Caixa de Consultas (LINQ - Language Integrated Query):
O que faz: Filtra, mapeia e transforma coleções e tabelas.
Membros: AnyAsync(), FirstOrDefaultAsync(), Where(), Select(), ToListAsync().
Caixa de Arquitetura (Injeção de Dependência & Construtores):
O que faz: Conecta as ferramentas na inicialização (Program.cs) para que fiquem disponíveis nos construtores das classes.

Pilar 2: Entender o Padrão de Nomenclatura do C#
O C# é extremamente consistente. Quando você entende as convenções da Microsoft, consegue adivinhar o que um método faz mesmo sem nunca tê-lo visto antes:
Sufixo Async: O método retorna uma Task e deve ser chamado usando await.
Prefixo Get / Find / First: Métodos para buscar dados.
Prefixo Any / All: Métodos booleanos que retornam true ou false.
Substantivos HTTP (Ok, NotFound, BadRequest, Conflict): Métodos que geram respostas com seus respectivos status códigos HTTP (200, 404, 400, 409).

Pilar 3: O Padrão de Repetição por Projetos (Projetos de Fim de Semana)
Quando você terminar este projeto de liquidação de ativos, perceberá que a estrutura do próximo projeto (ex: um sistema de risco de crédito, ou um gateway de pagamentos) usará exatamente as mesmas peças:
Entidade no Core com validações no construtor.
DbContext mapeando tabelas.
DTOs definindo os contratos de entrada e saída.
Controllers injetando o DbContext, usando LINQ (AnyAsync, FirstOrDefaultAsync) para consultar/salvar e retornando IActionResult.
Ninguém memoriza a documentação inteira. Desenvolvedores sêniores consultam referências o tempo todo; a diferença é que eles sabem o que procurar porque dominam o papel arquitetural de cada peça.


Com certeza estudar o Entity Framework Core.