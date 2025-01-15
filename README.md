User Management API

Esta é uma API simples para gerenciamento de usuários, criada com ASP.NET Core. A API permite realizar operações CRUD (Criar, Ler, Atualizar e Excluir) em um banco de dados SQL Server. O objetivo é oferecer um ponto de partida para desenvolvedores que desejam construir sistemas similares.

Tecnologias Utilizadas

.NET 6 (ASP.NET Core)

Entity Framework Core

SQL Server

Swagger para documentação da API

MediatR para padrão CQRS

Configuração do Projeto

Dependências

Certifique-se de que as seguintes dependências estão instaladas:

SDK .NET 6 ou superior

SQL Server

Um cliente HTTP para testes (Postman, Insomnia, etc.)

Clonando o Repositório

git clone https://github.com/seu-usuario/nome-do-repositorio.git
cd nome-do-repositorio

Configuração do Banco de Dados

Crie um banco de dados SQL Server.

Atualize a string de conexão no arquivo appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=UserManagementDb;Trusted_Connection=True;"
}

Execute as migrações para criar o esquema do banco de dados:

dotnet ef database update

Executando a API

Inicie a aplicação:

dotnet run

Acesse o Swagger para testar os endpoints:

http://localhost:5000/swagger

Endpoints da API

Base URL

https://localhost:5001/api/user

Endpoints Disponíveis

1. Obter Todos os Usuários

GET /api/user

Resposta de Exemplo:

[
  {
    "id": 1,
    "username": "johndoe",
    "firstName": "John",
    "lastName": "Doe"
  }
]

2. Adicionar Novo Usuário

POST /api/user

Corpo da Requisição:

{
  "username": "johndoe",
  "firstName": "John",
  "lastName": "Doe"
}

Resposta de Sucesso:

{
  "id": 1,
  "username": "johndoe",
  "firstName": "John",
  "lastName": "Doe"
}

3. Remover Usuário

DELETE /api/user/{id}

Parâmetros:

id: ID do usuário a ser removido.

Resposta de Sucesso:

{
  "message": "Usuário removido com sucesso."
}

Estrutura do Projeto

src/
├── Controllers/
│   └── UserController.cs
├── Application/
│   └── Interfaces/
│        └── IUserRepository.cs
├── Infra/
│   ├── Data/
│   │   └── UserManagementDbContext.cs
│   └── Repositories/
│        └── UserRepository.cs
├── Domain/
│   └── Entities/
│        └── User.cs
└── Program.cs

Contribuição

Sinta-se à vontade para abrir problemas e enviar pull requests para melhorias ou correções.

Contato

Nome: Patrick

Email: Mendespatrick720@gmail.com

Licença: Este projeto está licenciado sob a Licença MIT. Consulte o arquivo LICENSE para mais informações.

