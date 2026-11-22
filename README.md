# Edenred Test

Aplicação web para cadastro e consulta de clientes e veículos vinculados. A interface Angular consome uma API ASP.NET Core, que armazena os dados em PostgreSQL.

## Tecnologias utilizadas

Consulte a [lista de tecnologias e ferramentas](docs/tecnologias.md).

## Como executar o projeto

### Aplicação completa com Docker Compose

Com o Docker em execução e o Docker Compose disponível, rode na raiz do repositório:

```powershell
docker compose up --build
```

Abra a [aplicação em http://localhost:4200](http://localhost:4200). O Nginx serve a interface e encaminha as chamadas a `/api/` para o backend. O PostgreSQL fica acessível dentro da rede do Compose.

Na primeira inicialização do volume `postgres_data`, o PostgreSQL cria as tabelas e insere um cliente fictício com um veículo vinculado. O projeto Compose usa o nome `api` para reaproveitar o volume existente.

Para parar os serviços, preservando os dados, execute em outro terminal:

```powershell
docker compose down
```

Para experimentar os dados de demonstração em um volume separado, pare a execução anterior e use `docker compose -p edenred-demo up --build`. Encerre essa execução com `docker compose -p edenred-demo down`.

Se o volume já existir e o banco estiver sem as tabelas, aplique o script uma vez com os serviços em execução:

```powershell
docker compose exec postgres sh -c 'psql -v ON_ERROR_STOP=1 -1 -U "$POSTGRES_USER" -d "$POSTGRES_DB" -f /docker-entrypoint-initdb.d/01-schema.sql'
```

### Depuração local com Visual Studio e Visual Studio Code

Use o Docker somente para o PostgreSQL. Execute a API no **Visual Studio** e o Angular no **Visual Studio Code**. Pare a execução completa do Compose antes de iniciar para liberar a porta 4200.

1. Na raiz do repositório, suba o banco com um volume separado e publique a porta 5433 no endereço local:

   ```powershell
   docker compose -p edenred-debug run -d --rm --name edenred-postgres-debug -p 127.0.0.1:5433:5432 postgres
   docker logs -f edenred-postgres-debug
   ```

   Aguarde a conclusão da inicialização do banco. Pressione **Ctrl+C** para sair dos logs.

2. No **Visual Studio**, use uma instalação compatível com o **SDK do .NET 11** e com a carga de trabalho **ASP.NET e desenvolvimento Web**. Abra [EdenredTest.sln](src/api/EdenredTest.sln), defina `EdenredTest` como projeto de inicialização e selecione o perfil **EdenredTest**. Se você configurou outras credenciais para o Compose, ajuste o valor.

   ```text
   Host=localhost;Port=5433;Database=edenred;Username=edenred;Password=YourStr0ngP@ssword
   ```

   Se precisar confiar no certificado HTTPS de desenvolvimento, execute `dotnet dev-certs https --trust`. Defina um ponto de interrupção em um controller ou handler e pressione **F5**. O perfil usa [https://localhost:7172/swagger](https://localhost:7172/swagger) para o Swagger. Mantenha a variável com as credenciais no ambiente local.

3. No **Visual Studio Code**, abra a pasta [src/front/edenred-test](src/front/edenred-test). Com **Node.js 24** e npm disponíveis, execute `npm ci` no terminal da pasta. Defina um ponto de interrupção em um arquivo TypeScript e pressione **F5** com a configuração **ng serve**. Ela inicia o servidor de desenvolvimento e abre o Chrome em [http://localhost:4200](http://localhost:4200). O Angular usa `https://localhost:7172/api` nesse modo. Para rodar a interface a partir do terminal, use `npm start`.

4. Ao terminar, interrompa a depuração nas duas IDEs e execute `docker stop edenred-postgres-debug`. O contêiner é removido e o volume de depuração é preservado.

## Decisões arquiteturais

Consulte as [decisões arquiteturais](docs/arquitetura.md) para conhecer a organização do código, o fluxo das requisições e a persistência.

## Melhorias futuras

[Melhorias futuras](docs/melhorias-futuras.md)
