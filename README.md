No backend é importante adicionar as variáveis de conexão com o banco do MySQL, no arquivo appsettings.json:
"DefaultConnection": "server=nome-do-server; database=nome-do-db; User=user; Password=senha";

Rodar os comandos:

add-migration MyMigration

update-database

No frontend:

npm install
