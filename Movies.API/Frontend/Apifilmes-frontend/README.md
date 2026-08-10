# 🎬 Movies API - Plataforma de Gerenciamento de Filmes

Uma plataforma web completa para gerenciamento de catálogo de filmes com autenticação JWT, sistema de favoritos e painel administrativo.

![Vue.js](https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D)
![TypeScript](https://img.shields.io/badge/TypeScript-007ACC?style=for-the-badge&logo=typescript&logoColor=white)
![Vite](https://img.shields.io/badge/Vite-B73BFE?style=for-the-badge&logo=vite&logoColor=FFD62E)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)

## 📋 Índice

- [Visão Geral](#-visão-geral)
- [Funcionalidades](#-funcionalidades)
- [Tecnologias](#-tecnologias)
- [Arquitetura](#-arquitetura)
- [Instalação](#-instalação)
- [Configuração](#-configuração)
- [Uso](#-uso)
- [API Endpoints](#-api-endpoints)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Autenticação](#-autenticação)
- [Contribuição](#-contribuição)
- [Licença](#-licença)

## 🎯 Visão Geral

O **Movies API** é uma aplicação full-stack que permite aos usuários explorar um catálogo de filmes, adicionar favoritos e gerenciar o conteúdo através de um painel administrativo. A plataforma conta com autenticação segura via JWT, sistema de permissões baseado em roles e uma interface moderna e responsiva.

### Casos de Uso
- **Usuários comuns**: Explorar catálogo, favoritar filmes, visualizar detalhes
- **Administradores**: Gerenciar CRUD completo de filmes
- **Desenvolvedores**: API RESTful documentada com Swagger

## ✨ Funcionalidades

### 🔐 Autenticação & Autorização
- Login/Logout com JWT
- Controle de acesso baseado em roles (Admin/User)
- Tokens armazenados com segurança
- Rotas protegidas

### 🎬 Gerenciamento de Filmes
- ✅ **CRUD Completo** (Create, Read, Update, Delete)
- ✅ Busca por título
- ✅ Filtro por gênero
- ✅ Sistema de avaliação (1-5 estrelas)
- ✅ Upload de trailers (YouTube/Vimeo)
- ✅ Carrossel de filmes em destaque

### ❤️ Sistema de Favoritos
- Adicionar/remover filmes dos favoritos
- Lista persistente no localStorage
- Acesso rápido aos filmes preferidos

### 👨‍💼 Painel Administrativo
- Interface dedicada para administradores
- Gerenciamento completo de conteúdo
- Visualização em grid com pré-visualização
- Modais de criação/edição

### 📱 Interface
- Design responsivo (mobile/desktop)
- Temas escuros otimizados
- Animações suaves
- Carregamento otimizado

## 🛠️ Tecnologias

### **Frontend**
- **Vue.js 3** - Framework progressivo
- **TypeScript** - Tipagem estática
- **Vite** - Build tool e dev server
- **Vuex** - Gerenciamento de estado
- **Vuetify** - Componentes UI
- **Axios** - Cliente HTTP
- **Vue Router** - Roteamento SPA

### **Backend**
- **ASP.NET Core 8** - Framework API
- **Entity Framework Core** - ORM
- **PostgreSQL** - Banco de dados
- **JWT** - Autenticação
- **Swagger** - Documentação API
- **Npgsql** - Driver PostgreSQL

### **Dev Tools**
- **VSCode** - IDE principal
- **Git** - Controle de versão
- **Postman/Insomnia** - Teste de APIs
- **Docker** (opcional) - Containerização

## 🏗️ Arquitetura

```
┌─────────────────────────────────────────────────┐
│                   Frontend (Vue)                 │
│  ┌─────────┐  ┌─────────┐  ┌─────────────────┐ │
│  │ Router  │  │  Vuex   │  │   Components    │ │
│  │         │  │  Store  │  │                 │ │
│  └─────────┘  └─────────┘  └─────────────────┘ │
│          │            │               │         │
└──────────┼────────────┼───────────────┼─────────┘
           │            │               │
           ▼            ▼               ▼
┌─────────────────────────────────────────────────┐
│               Backend (ASP.NET)                 │
│  ┌─────────┐  ┌─────────┐  ┌─────────────────┐ │
│  │Controllers││ Services │  │   Repository   │ │
│  │           ││  Layer   │  │     Pattern    │ │
│  └─────────┘  └─────────┘  └─────────────────┘ │
│          │            │               │         │
└──────────┼────────────┼───────────────┼─────────┘
           │            │               │
           ▼            ▼               ▼
┌─────────────────────────────────────────────────┐
│               Database Layer                     │
│  ┌─────────────────────────────────────────────┐│
│  │               PostgreSQL                    ││
│  │  • Users table                              ││
│  │  • Movies table                             ││
│  │  • Favorites table                          ││
│  └─────────────────────────────────────────────┘│
└─────────────────────────────────────────────────┘
```

## 🚀 Instalação

### Pré-requisitos
- Node.js 18+ e npm
- .NET 8 SDK
- PostgreSQL 15+
- Git

### Backend Setup
```bash
# Clone o repositório
git clone <seu-repositorio>
cd Movies.API/Backend/Movies.API

# Restaure as dependências
dotnet restore

# Configure a conexão com o banco
# Edite appsettings.json com suas credenciais PostgreSQL

# Execute as migrations
dotnet ef database update

# Inicie o servidor
dotnet run
# Backend disponível em: http://localhost:5244
```

### Frontend Setup
```bash
cd Movies.API/Frontend/Apifilmes-frontend

# Instale as dependências
npm install

# Configure a URL do backend
# Edite src/api/axios.ts se necessário

# Inicie o servidor de desenvolvimento
npm run dev
# Frontend disponível em: http://localhost:5173
```

## ⚙️ Configuração

### Variáveis de Ambiente (Backend)

Crie/edite `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=movies;Username=seu_usuario;Password=sua_senha"
  },
  "JwtSettings": {
    "Key": "SuaChaveSecretaSuperForteComPeloMenos32Caracteres",
    "Issuer": "MoviesAPI",
    "Audience": "MoviesAPIClient",
    "DurationMinutes": 60
  }
}
```

### Configuração do Banco

Execute no PostgreSQL:
```sql
CREATE DATABASE movies;
```

### Usuário Admin

Para criar um usuário administrador, execute no banco:

```sql
INSERT INTO users ("Username", "Password", "Role")
VALUES ('admin', 'JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=', 'Admin');
```

**Credenciais padrão:**
- Usuário: `admin`
- Senha: `admin123`
- Role: `Admin`

> A senha é armazenada como hash SHA256 em Base64.

## 🖥️ Uso

### Modo Desenvolvimento
```bash
# Backend
cd Movies.API/Backend/Movies.API
dotnet watch run

# Frontend
cd Movies.API/Frontend/Apifilmes-frontend
npm run dev
```

### Produção
```bash
# Build do frontend
npm run build

# O conteúdo estará em /dist
# Sirva com Nginx, Apache ou similar
```

### Acessando a Aplicação
1. **Usuários Comuns:**
   - Registre uma nova conta ou use credenciais existentes
   - Explore o catálogo de filmes
   - Adicione filmes aos favoritos
   - Visualize detalhes e trailers

2. **Administradores:**
   - Faça login com credenciais admin
   - Acesse o painel administrativo
   - Gerencie o catálogo de filmes (CRUD)
   - Todos os privilégios de usuário comum

## 🔌 API Endpoints

### Autenticação
- `POST /api/login` - Login de usuário
- `POST /api/register` - Registro de novo usuário

### Filmes
- `GET /api/movie/get-all` - Lista todos os filmes
- `GET /api/movie/{id}` - Obtém filme por ID
- `POST /api/movie` - Cria novo filme (Admin)
- `PUT /api/movie/{id}` - Atualiza filme (Admin)
- `DELETE /api/movie/{id}` - Remove filme (Admin)

### Usuários
- `GET /api/user` - Lista usuários (Admin)
- `GET /api/user/{id}` - Obtém usuário por ID
- `POST /api/user` - Cria usuário
- `PUT /api/user/{id}` - Atualiza usuário

### Favoritos
- `GET /api/favorite` - Lista favoritos do usuário
- `POST /api/favorite` - Adiciona favorito
- `DELETE /api/favorite/{id}` - Remove favorito

### Documentação API
Acesse `http://localhost:5244/swagger` para documentação interativa da API.

## 📁 Estrutura do Projeto

### Frontend
```
src/
├── api/                 # Configuração Axios e interceptors
├── assets/             # Imagens, fonts, CSS globais
├── components/         # Componentes Vue reutilizáveis
│   ├── MovieCard.vue   # Card de filme individual
│   ├── MovieCarousel.vue # Carrossel de destaques
│   └── Navbar.vue      # Barra de navegação
├── router/             # Configuração de rotas Vue Router
├── store/              # Vuex Store (autenticação, favoritos)
├── types/              # Definições TypeScript
├── views/              # Páginas/Vistas principais
│   ├── HomeView.vue    # Página inicial
│   ├── MoviesView.vue  # Catálogo de filmes (CRUD)
│   ├── AdminView.vue   # Painel administrativo
│   ├── FavoritesView.vue # Lista de favoritos
│   ├── LoginView.vue   # Página de login
│   └── RegisterView.vue # Página de registro
└── App.vue             # Componente raiz
```

### Backend
```
Movies.API/
├── Controllers/        # Controladores API
├── Models/             # Modelos de dados
├── DatabaseContext/    # Contexto do Entity Framework
├── Mappings/          # Configurações do Entity Framework
├── Migrations/        # Migrations do banco de dados
├── Authentication/    # Lógica de autenticação JWT
├── Encrypt/           # Criptografia de senhas
├── Interface/         # Interfaces e repositórios
└── Properties/        # Configurações de build e execução
```

## 🔐 Autenticação

### Fluxo JWT
1. Usuário envia credenciais via `POST /api/login`
2. Servidor valida e gera token JWT com claims (role, userId)
3. Token é retornado ao cliente
4. Cliente armazena token no localStorage
5. Token é enviado automaticamente em todas as requisições via interceptor
6. Backend valida token em cada request protegida

### Controle de Acesso
- **Rotas públicas**: Login, Registro, Home
- **Rotas protegidas**: Todas as outras
- **Admin-only**: CRUD de filmes, painel administrativo

## 🤝 Contribuição

Contribuições são bem-vindas! Siga estes passos:

1. Fork o projeto
2. Crie uma branch (`git checkout -b feature/nova-funcionalidade`)
3. Commit suas mudanças (`git commit -m 'Adiciona nova funcionalidade'`)
4. Push para a branch (`git push origin feature/nova-funcionalidade`)
5. Abra um Pull Request

### Padrões de Código
- Use TypeScript com tipagem estrita
- Siga o estilo de código Vue 3 Composition API
- Mantenha componentes pequenos e focados
- Documente funções complexas

## 📄 Licença

Este projeto está licenciado sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para detalhes.
