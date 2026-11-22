# Decisões arquiteturais

## Interface e API separadas

O Angular organiza as telas de cadastro e listagem por rota. Os controllers ASP.NET Core expõem as operações de consulta, criação, atualização e exclusão em `/api/clients` e `/api/vehicles`. A comunicação por HTTP permite executar e depurar a interface e a API em processos independentes.

## Backend organizado por caso de uso

O projeto [EdenredTest](../src/api/EdenredTest) concentra os controllers e a configuração da aplicação. Os controllers recebem as requisições HTTP e usam o MediatR para acionar os handlers em [EdenredTest.Application/Features](../src/api/EdenredTest.Application/Features). Cada funcionalidade reúne seu handler e seus modelos de entrada ou saída, quando usados. A lógica de cada operação fica próxima dos modelos que utiliza.

O Autofac registra as dependências usadas pelos handlers, incluindo os repositórios. As interfaces de repositório permitem testar regras de negócio com dependências simuladas, como nos [testes de cadastro](../src/api/EdenredTestTests/Handlers).

Para maiores informações [consule essa documentação](https://milanjovanovic.tech/blog/vertical-slice-architecture-dotnet).

## Persistência com SQL explícito

Os repositórios chamam consultas SQL incorporadas ao assembly a partir da pasta [Shared/Queries](../src/api/EdenredTest.Application/Shared/Queries). O Dapper passa os parâmetros às consultas e mapeia os resultados. O [script de inicialização](../src/api/Scripts/Database-creation.sql) cria as tabelas e mantém o vínculo entre veículos e clientes com uma chave estrangeira em `vehicles.client_id`. A configuração da conexão com PostgreSQL usa a chave `SqlServer:ConnectionString`.

## Comunicação entre serviços

No [Docker Compose](../compose.yaml), frontend, backend e PostgreSQL rodam em contêineres separados. O navegador acessa a interface e `/api/` pelo mesmo endereço. O Nginx encaminha as chamadas da API ao backend pela rede interna. Um volume mantém os dados entre execuções, e o backend aguarda a verificação de disponibilidade do PostgreSQL antes de iniciar.

Na depuração, o Angular usa a URL HTTPS do perfil local da API. O backend permite a origem do servidor de desenvolvimento Angular por meio da configuração de CORS.

[Voltar ao README](../README.md)
