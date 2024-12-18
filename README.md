# Agenda Web Application

## Pré-requisitos

- .NET (versão 9.0.101)
- Node.js (versão 22.11.0)
- MySQL

## Instruções

### 1. Clone o repositório
`git clone <URL_DO_REPOSITORIO>`

`cd meu-projeto`

### 2. Configure o Backend
#### 2.1. Configure a string de conexão com o banco de dados
Configure a string de conexão no arquivo appsettings.json para se conectar ao banco de dados no Workbench local do MySQL.

Exemplo:
``` 
"DefaultConnection": "server=nome-do-server; database=nome-do-db; User=user; Password=senha";
``` 

#### 2.2 Restaure as dependências
Normalmente o Visual Studio restaura as dependências automaticamente ao abrir o projeto.
Porém, caso não funcione, é viável restaurar as dependências dessa forma:

`cd AgendaWebApplication`

`dotnet restore`

#### 2.2 Execute as migrações no banco de dados
No Visual Studio:

`update-database`

Fora do Visual Studio:

`dotnet ef database update`

#### 2.3 Execute o servidor
No Visual Studio existe o botão de run, fora dele é necessário utilizar o comando `dotnet run`

### 3. Configure o Frontend
#### 3.1. Instale as dependências
`cd agenda-frontend`

`npm install`

#### 3.2. Execute o Frontend
`npm run serve`

### 4. Observações
Caso o frontend não se comunique corretamente com o backend, é possível que a URL em que o frontend está rodando esteja diferente da URL setada no arquivo Program.cs, no método de habilitar o CORS no servidor.
Caso não funcione, basta apenas verificar a porta em que o frontend está rodando e adicioná-la a configuração do CORS no arquivo Program.cs.
Exemplo de código:
``` 
builder.Services.AddCors(options =>
{
    options.AddPolicy("SiteCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:8080", "http://localhost:8081", "http://localhost:8082") // URL do frontend
              .AllowAnyHeader()
              .WithHeaders("Content-Type", "Authorization")
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
``` 