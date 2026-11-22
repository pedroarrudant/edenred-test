# Tecnologias utilizadas

| Área | Tecnologias e uso no projeto |
| --- | --- |
| API | C# e ASP.NET Core sobre .NET 11 para os endpoints HTTP. |
| Casos de uso | MediatR para encaminhar requisições aos handlers e Autofac para injeção de dependências. |
| Dados | PostgreSQL 16 para armazenamento, Npgsql como driver e Dapper para executar SQL parametrizado e mapear resultados. |
| Acesso ao banco | Polly para novas tentativas em operações de repositório que geram exceções do Npgsql. |
| Documentação da API | Swashbuckle para gerar a especificação OpenAPI e disponibilizar o Swagger UI em desenvolvimento. |
| Interface | Angular 22, TypeScript 6, Angular Router, Angular Forms e RxJS. Angular Material e Angular CDK dão suporte aos componentes da interface. |
| Build da interface | Node.js 24, npm e Angular CLI. |
| Testes | xUnit e Moq no backend, com Coverlet para coleta de cobertura. |
| Execução | Docker e Docker Compose para orquestrar os serviços. Nginx para servir a aplicação Angular e encaminhar chamadas à API. |
| Depuração | Visual Studio para C# e Visual Studio Code com Chrome para Angular. |

As referências de dependências estão nos projetos [da API](../src/api/EdenredTest/EdenredTest.csproj), [da aplicação](../src/api/EdenredTest.Application/EdenredTest.Application.csproj), [dos testes .NET](../src/api/EdenredTestTests/EdenredTestTests.csproj) e no [package.json do frontend](../src/front/edenred-test/package.json).

[Voltar ao README](../README.md)
