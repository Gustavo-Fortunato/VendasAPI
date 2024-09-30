# VendasAPI

## Descrição do Projeto

Este projeto implementa uma API de vendas para a empresa **123Vendas**, desenvolvida com **.NET Core 8** e **SQL Server**. A API segue os princípios de **Domain-Driven Design (DDD)** e é dividida em camadas (**API, Domain, Data**). Inclui funcionalidades para gerenciar vendas, como a criação, leitura, atualização e cancelamento de vendas, além de registrar eventos relacionados.

## Instruções

### Como Configurar

1. Clone o repositório:
   git clone https://github.com/seu_usuario/VendasAPI.git
   cd VendasAPI
2. Restaure as dependências:
    dotnet restore
3. Crie as migrações e atualize o banco de dados:
    dotnet ef migrations add InitialCreate
    dotnet ef database update
4. Execute a aplicação:
    dotnet run --project VendasAPI.API
5. Acesse a API em https://localhost:5001/api/vendas.

### Como Testar
Os testes de unidade e integração podem ser executados usando o seguinte comando:
    dotnet test

### Caso de Uso
A API oferece um CRUD completo para gerenciar vendas, incluindo:
- Número da venda
- Data da venda
- Cliente
- Valor total da venda
- Produtos, quantidades, valores unitários e descontos
- Status de cancelamento

Além disso, eventos como CompraCriada, CompraAlterada, CompraCancelada, e ItemCancelado são registrados, mas não são publicados em um message broker, porém são adicionados no Serilog e gravado no txt de vendas.

### Estrutura do Projeto
├── tests

│   ├── VendasAPI.IntegrationTests

│   │   ├── Controllers

│   │   ├── Fixtures

│   ├── VendasAPI.UnitTests

│   │   ├── Services

│

├── VendasAPI.API

│   ├── Connected Services

│   ├── Dependências

│   ├── Controllers

│   ├── DTOs

│   ├── Logs               (logs gerados pelo Serilog)

│

├── VendasAPI.Data

│   ├── Configurations

│   ├── Migrations         (para o Entity Framework)

│   └── Repositories

│

├── VendasAPI.Domain

│   ├── DTOs

│   ├── Entities

│   ├── Events             (CompraCriadaEvent, CompraAlteradaEvent, etc.)

│   ├── Repositories

│   └── Services           (lógica de negócios e regras do domínio)


├── VendasAPI.Logs         (responsável pelo gerenciamento de logs)

### Descrição dos Diretórios
- src: Contém todo o código-fonte da aplicação.
    - API: Onde ficam os controladores e DTOs.
    - Domain: Contém entidades, serviços e outras lógicas de domínio.
    - Data: Contém repositórios e migrações do banco de dados.
- tests: Contém os projetos de testes.
    - UnitTests: Testes unitários para serviços, repositórios, etc.
    - IntegrationTests: Testes de integração, normalmente focados em controllers e integração com o banco de dados.
- logs: Para armazenar logs, caso você implemente o Serilog ou outra biblioteca de logging.

README.md: Documentação do projeto.
VendasAPI.sln: Arquivo de solução 
### Script de criação das tabelas
    CREATE TABLE Venda (
    VendaId INT PRIMARY KEY IDENTITY(1,1),
    DataVenda DATETIME NOT NULL,
    ClienteId NVARCHAR(50) NOT NULL,
    ClienteNome NVARCHAR(100) NOT NULL,
    ValorTotal DECIMAL(18, 2) NOT NULL,
    Cancelado BIT NOT NULL);
CREATE TABLE ItemVenda (
    ItemVendaId INT PRIMARY KEY IDENTITY(1,1),
    VendaId INT NOT NULL,
    ProdutoId NVARCHAR(50) NOT NULL,
    ProdutoNome NVARCHAR(100) NOT NULL,
    Quantidade INT NOT NULL,
    ValorUnitario DECIMAL(18, 2) NOT NULL,
    Desconto DECIMAL(18, 2) NOT NULL,
    ValorTotal DECIMAL(18, 2) NOT NULL,
    CONSTRAINT FK_ItemVenda_Venda FOREIGN KEY (VendaId) REFERENCES Venda(VendaId)
);
