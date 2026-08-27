# CineVerse

Aplicacao full-stack para catalogo e gerenciamento de filmes. O projeto combina uma API REST em ASP.NET Core 8 com uma SPA em Vue 3, permitindo consultar filmes, pesquisar por titulo, filtrar por genero, visualizar posters e trailers, criar uma conta, autenticar com JWT e manter uma lista de favoritos.

## Sumario

- [Visao geral](#visao-geral)
- [Funcionalidades](#funcionalidades)
- [Arquitetura](#arquitetura)
- [Tecnologias](#tecnologias)
- [Pre-requisitos](#pre-requisitos)
- [Configuracao](#configuracao)
- [Executando localmente](#executando-localmente)
- [Documentacao da API](#documentacao-da-api)
- [Autenticacao e autorizacao](#autenticacao-e-autorizacao)
- [Modelo de dados](#modelo-de-dados)
- [Frontend](#frontend)
- [Estrutura do repositorio](#estrutura-do-repositorio)
- [Migrations](#migrations)
- [Build de producao](#build-de-producao)
- [Seguranca e pontos de atencao](#seguranca-e-pontos-de-atencao)
- [Desenvolvimento](#desenvolvimento)
- [Licenca](#licenca)

## Visao geral

O CineVerse possui dois aplicativos independentes dentro do mesmo repositorio:

- **Backend**: API HTTP responsavel por autenticacao, usuarios, filmes e favoritos.
- **Frontend**: interface web responsiva que consome a API e oferece as telas de navegacao, login, cadastro, catalogo, favoritos e administracao.

O banco de dados utilizado e o PostgreSQL. O Entity Framework Core controla o mapeamento das entidades e as alteracoes de schema por meio de migrations.

## Funcionalidades

### Catalogo

- Listagem de todos os filmes.
- Consulta de filme por ID.
- Busca local por titulo.
- Filtro local por genero.
- Exibicao de poster, sinopse, genero, avaliacao e trailer.
- Carrossel de filmes em destaque.

### Contas e acesso

- Cadastro de usuario.
- Login com usuario e senha.
- Emissao de token JWT.
- Perfis `User` e `Admin`.
- Protecao de endpoints com Bearer Token.
- Rota de administracao visivel apenas para usuarios com role `Admin`.

### Favoritos

- Listagem dos favoritos do usuario autenticado.
- Adicao de um filme aos favoritos.
- Remocao de um favorito.
- IDs de favoritos mantidos no `localStorage` pelo frontend para atualizar a interface rapidamente.

### Administracao

Usuarios com role `Admin` podem criar, editar e excluir filmes. A tela de filmes oferece um formulario completo com titulo, poster, genero, avaliacao, trailer e sinopse.

## Arquitetura

```text
+-----------------------------+
| Frontend Vue 3              |
| Vue Router | Vuex | Axios   |
+--------------+--------------+
               |
               | HTTP + Bearer JWT
               v
+-----------------------------+
| Backend ASP.NET Core 8      |
| Controllers | Services      |
| JWT | Swagger | CORS       |
+--------------+--------------+
               |
               | Entity Framework Core / Npgsql
               v
+-----------------------------+
| PostgreSQL                  |
| users | Movies | Favorites  |
+-----------------------------+
```

Os controllers recebem as requisicoes HTTP, os services concentram as operacoes de usuarios e filmes, e o `DataContext` abre a conexao com o PostgreSQL e aplica os mapeamentos das entidades.

## Tecnologias

### Backend

- C# e .NET 8.
- ASP.NET Core Web API.
- Entity Framework Core 8.
- PostgreSQL.
- Npgsql e Npgsql.EntityFrameworkCore.PostgreSQL.
- JWT Bearer para autenticacao.
- Swashbuckle para Swagger/OpenAPI.

### Frontend

- Vue 3.
- TypeScript.
- Vite.
- Vue Router.
- Vuex.
- Vuetify.
- Axios.
- Material Design Icons.

## Pre-requisitos

Instale antes de iniciar:

- Git.
- .NET 8 SDK.
- Node.js 18 ou superior e npm.
- PostgreSQL em execucao.
- Opcional: ferramenta `dotnet ef` para executar migrations.

Confira as instalacoes:

```bash
dotnet --version
node --version
npm --version
psql --version
```

## Configuracao

### Banco de dados

Crie um banco PostgreSQL vazio, por exemplo:

```sql
CREATE DATABASE movies;
```

Depois ajuste a connection string em `Backend/Movies.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=movies;Username=postgres;Password=sua_senha"
  },
  "JwtSettings": {
    "Key": "uma-chave-secreta-longa-e-aleatoria-com-pelo-menos-32-caracteres",
    "Issuer": "CineVerse",
    "Audience": "CineVerseClient",
    "DurationMinutes": 60
  }
}
```

A aplicacao le atualmente a conexao e as configuracoes JWT diretamente de `appsettings.json`. Nao utilize credenciais reais nesse arquivo em um ambiente compartilhado ou de producao.

### URL do frontend

A API do frontend esta definida em `Frontend/Apifilmes-frontend/src/api/axios.ts`:

```ts
baseURL: 'http://localhost:5244/api'
```

Se a API for executada em outra porta ou dominio, atualize esse valor. A configuracao de CORS do backend tambem deve permitir a origem usada pelo frontend.

## Executando localmente

Abra dois terminais na raiz do repositorio.

### 1. Preparar e executar o backend

```bash
cd Backend/Movies.API
dotnet restore
dotnet ef database update
dotnet run
```

Com o perfil HTTP padrao, a API fica disponivel em `http://localhost:5244`. O Swagger e aberto em:

```text
http://localhost:5244/swagger
```

O perfil HTTPS tambem esta configurado para `https://localhost:7204`.

Se a ferramenta Entity Framework nao estiver instalada:

```bash
dotnet tool install --global dotnet-ef
```

Para desenvolvimento com recompilacao automatica:

```bash
dotnet watch run
```

### 2. Preparar e executar o frontend

```bash
cd Frontend/Apifilmes-frontend
npm install
npm run dev
```

Por padrao, o Vite disponibiliza a aplicacao em `http://localhost:5173`.

Com os dois processos em execucao:

1. Acesse `http://localhost:5173`.
2. Crie uma conta em `/cadastro`.
3. Entre em `/login`.
4. Abra `/filmes` para consultar o catalogo.
5. Use `/favoritos` para consultar os filmes marcados.

## Documentacao da API

A URL base e:

```text
http://localhost:5244/api
```

Os endpoints protegidos exigem o header:

```http
Authorization: Bearer SEU_TOKEN_JWT
```

### Health check

| Metodo | Rota | Auth | Resultado |
| --- | --- | --- | --- |
| `GET` | `/api/HeatlCheck` | Nao | Retorna `The API is working` |

> A grafia `HeatlCheck` reflete o nome atual da classe `HeatlCheckController`.

### Autenticacao e usuarios

| Metodo | Rota | Auth | Descricao |
| --- | --- | --- | --- |
| `POST` | `/api/Login` | Nao | Valida credenciais e retorna um JWT |
| `POST` | `/api/User` | Nao | Cria usuario com role padrao `User` |
| `GET` | `/api/User/{id}` | Sim | Busca um usuario por ID |
| `PUT` | `/api/User/{id}` | Sim | Atualiza usuario e senha |
| `DELETE` | `/api/User/{id}` | Sim | Exclui usuario |
| `GET` | `/api/User/get-all` | Sim | Lista usuarios |

Cadastro e login usam o seguinte corpo:

```json
{
  "username": "usuario",
  "password": "senha"
}
```

Um login bem-sucedido retorna:

```json
{
  "token": "eyJ..."
}
```

### Filmes

Todos os endpoints de filmes exigem autenticacao. Criacao, alteracao e exclusao exigem role `Admin`.

| Metodo | Rota | Auth | Descricao |
| --- | --- | --- | --- |
| `GET` | `/api/Movie/get-all` | User/Admin | Lista todos os filmes |
| `GET` | `/api/Movie/{id}` | User/Admin | Busca um filme por ID |
| `POST` | `/api/Movie` | Admin | Cria um filme |
| `PUT` | `/api/Movie/{id}` | Admin | Atualiza um filme |
| `DELETE` | `/api/Movie/{id}` | Admin | Exclui um filme |

Corpo usado para criar ou atualizar um filme:

```json
{
  "title": "Inception",
  "posterUrl": "https://exemplo.com/inception.jpg",
  "overview": "Uma sinopse do filme.",
  "genre": "Ficcao Cientifica",
  "rating": 5,
  "trailerUrl": "https://www.youtube.com/embed/exemplo"
}
```

### Favoritos

Todos os endpoints de favoritos exigem autenticacao e operam sobre o usuario identificado no JWT.

| Metodo | Rota | Descricao |
| --- | --- | --- |
| `GET` | `/api/Favorite` | Lista os filmes favoritos do usuario |
| `POST` | `/api/Favorite/{movieId}` | Adiciona um filme aos favoritos |
| `DELETE` | `/api/Favorite/{movieId}` | Remove um filme dos favoritos |

Exemplo com `curl`:

```bash
curl -H "Authorization: Bearer SEU_TOKEN_JWT" \
  http://localhost:5244/api/Movie/get-all
```

Respostas comuns incluem `200 OK`, `201`/`200` conforme a operacao, `400 Bad Request`, `401 Unauthorized`, `403 Forbidden`, `404 Not Found` e `409 Conflict` ao tentar favoritar o mesmo filme duas vezes.

## Autenticacao e autorizacao

1. O usuario envia `username` e `password` para `POST /api/Login`.
2. O backend compara a senha recebida com o hash armazenado.
3. O backend gera um JWT com `NameIdentifier`, `Name` e `Role`.
4. O Vuex salva o token no `localStorage`.
5. O interceptor do Axios adiciona automaticamente `Authorization: Bearer ...` nas requisicoes seguintes.
6. O Vuex le a role do token para controlar a exibicao da area administrativa.

A validade do token e definida por `JwtSettings:DurationMinutes`, que atualmente e 60 minutos no arquivo de configuracao de exemplo.

## Modelo de dados

### `users`

- `Id`: identificador inteiro.
- `Username`: obrigatorio, ate 50 caracteres.
- `Password`: obrigatorio, ate 100 caracteres; armazenado como hash SHA-256 em Base64 pelo `PasswordEncryptor`.
- `Role`: obrigatorio, ate 20 caracteres, padrao `User`.

### `Movies`

- `Id`: identificador inteiro.
- `Title`: obrigatorio, ate 100 caracteres.
- `PosterUrl`: obrigatorio, ate 255 caracteres.
- `Overview`: obrigatorio, texto livre.
- `Genre`: obrigatorio, ate 50 caracteres.
- `Rating`: numero decimal com valor inicial `0`.
- `TrailerUrl`: opcional, ate 255 caracteres.

### `Favorites`

- `Id`: identificador inteiro.
- `UserId`: identificador do usuario.
- `MovieId`: identificador do filme.
- Relacionamento com `Movies` configurado com exclusao em cascata.

## Frontend

### Rotas da aplicacao

| Rota | Tela | Acesso |
| --- | --- | --- |
| `/` | Inicio | Publico |
| `/login` | Login | Publico |
| `/cadastro` | Cadastro | Publico |
| `/filmes` | Catalogo, busca, filtros e CRUD para admin | Requer login para carregar a API |
| `/favoritos` | Favoritos do usuario | Requer login |
| `/admin` | Painel administrativo | Role `Admin` |

O catalogo usa os generos predefinidos: `Acao`, `Aventura`, `Comedia`, `Drama`, `Terror`, `Ficcao Cientifica`, `Romance`, `Animacao`, `Documentario` e `Thriller`.

### Estado local

O Vuex armazena:

- `token`: JWT atual.
- `favorites`: lista de IDs de filmes favoritos.
- `isAdmin`: resultado da leitura da role no JWT.

O logout remove token e favoritos do `localStorage`. A fonte de dados dos filmes continua sendo a API.

### Scripts disponiveis

Execute na pasta `Frontend/Apifilmes-frontend`:

```bash
npm run dev          # servidor de desenvolvimento
npm run type-check   # verificacao TypeScript/Vue
npm run build-only   # build Vite
npm run build        # type-check + build
npm run preview      # serve o build localmente
```

## Estrutura do repositorio

```text
Movies.API/
├── Backend/
│   ├── Movies.API.slnx
│   └── Movies.API/
│       ├── Authentication/   # Configuracao e geracao de JWT
│       ├── Controllers/      # Endpoints HTTP
│       ├── DatabaseContext/  # DbContext PostgreSQL
│       ├── Encrypt/          # Hash de senhas
│       ├── Interface/        # Contratos dos services
│       ├── Mappings/         # Configuracao das entidades EF Core
│       ├── Migrations/       # Historico do schema do banco
│       ├── Models/           # Entidades de dominio
│       ├── Requests/         # DTOs de entrada
│       ├── Services/         # Regras de acesso a usuarios e filmes
│       ├── Program.cs        # Pipeline, JWT, CORS e Swagger
│       └── appsettings.json  # Banco e JWT
└── Frontend/
    └── Apifilmes-frontend/
        ├── public/
        ├── src/
        │   ├── api/          # Cliente Axios
        │   ├── components/   # Navbar, cards e carrossel
        │   ├── router/       # Rotas Vue
        │   ├── store/        # Estado Vuex
        │   ├── types/        # Tipos TypeScript
        │   └── views/        # Telas da aplicacao
        ├── package.json
        ├── tsconfig*.json
        └── vite.config.ts
```

## Migrations

As migrations ficam em `Backend/Movies.API/Migrations`. Para aplicar todas as alteracoes no banco:

```bash
cd Backend/Movies.API
dotnet ef database update
```

Para criar uma nova migration depois de alterar os modelos:

```bash
dotnet ef migrations add NomeDaMigration
dotnet ef database update
```

Para remover a ultima migration ainda nao aplicada:

```bash
dotnet ef migrations remove
```

## Build de producao

### Backend

```bash
cd Backend/Movies.API
dotnet publish -c Release -o ./publish
```

Execute o artefato publicado com:

```bash
dotnet ./publish/Movies.API.dll
```

### Frontend

```bash
cd Frontend/Apifilmes-frontend
npm ci
npm run build
```

O resultado fica em `Frontend/Apifilmes-frontend/dist`. Antes de publicar, configure a URL da API e o CORS para o dominio definitivo.

## Seguranca e pontos de atencao

- Substitua a chave JWT e a senha do PostgreSQL por valores fortes e fora do controle de versao.
- O `appsettings.json` atual contem valores de desenvolvimento; nao os reutilize em producao.
- O hash de senha implementado e SHA-256 simples. Para producao, prefira um algoritmo especifico para senhas, como Argon2id, bcrypt ou PBKDF2, com salt individual.
- O `DataContext` e o gerador de token carregam `appsettings.json` diretamente. Uma evolucao recomendada e usar configuracao injetada e variaveis de ambiente ou um secret manager.
- A role padrao de novos usuarios e `User`; a promocao para `Admin` deve ser feita por um fluxo administrativo seguro.
- A rota de usuarios atualmente permite operacoes autenticadas sem uma verificacao explicita de proprietario ou role. Revise essa politica antes de expor a API publicamente.
- Favoritos possuem `UserId` no modelo, mas a migration atual nao cria uma foreign key para `users` nem um indice unico composto para impedir duplicidades no banco. A API impede duplicacao no fluxo normal.
- A lista de favoritos do frontend usa `localStorage`; ela pode ficar desatualizada se os dados forem alterados por outro dispositivo ou cliente.
- Configure CORS com origens especificas e HTTPS em ambientes reais.

## Desenvolvimento

Uma rotina recomendada para contribuir:

1. Crie uma branch para a alteracao.
2. Suba o PostgreSQL e aplique as migrations.
3. Execute o backend e o frontend em terminais separados.
4. Valide os endpoints pelo Swagger ou por um cliente HTTP.
5. Execute `npm run type-check` e `npm run build` antes de abrir o pull request.
6. Ao alterar entidades, gere uma migration correspondente e descreva a mudanca.

Atualmente nao ha uma suite de testes automatizados configurada no repositorio. Testes de controller, services e componentes sao um proximo passo importante para evoluir o projeto com seguranca.

## Licenca

Nenhum arquivo de licenca foi identificado no repositorio. Defina uma licenca antes de distribuir o projeto publicamente.
