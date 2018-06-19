# Project Core Angular

## 1. Passos para começar

### Clonando o Repositório

`git clone https://github.com/rfopaulino/Projects.git`

### Configurando o banco de dados

Dentro de Interface > Startup.cs procurar por 'connectionString' e substituir para a conexão local (já está pré configurada)

Script para criação da base encontra-se em: https://drive.google.com/open?id=1rl91XVQd-14rlcyDm9wND5d2X9-71GeN

### Preparando o Back-end

Buildar a aplicação

### Instalando as Dependências

Dentro de WebApp rodar o comando `npm install`

### Configurando o Front-end

Dentro de WebApp > src > app > app.api.ts preecnher com a url do servidor de aplicação (api)

Fazer a mesma coisa em WebApp > src > app > agendamento.component.ts | linhas 96 e 110 (url's de chamadas ajax jquery)

### Inicializando o Servidor

Dentro de WebApp rodar o comando `ng serve`
