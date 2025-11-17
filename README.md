# Heulender Wolf API

API desenvolvida para um projeto da faculdade com foco no auxílio à adoção de pets.  
O sistema conecta **visitantes**, **organizações de adoção** e **administradores**, permitindo que pets disponíveis sejam exibidos de forma pública.

## 🐾 Objetivo

Facilitar que visitantes encontrem pets para adoção, visualizando diferentes organizações e suas listas de animais disponíveis.

## 🔑 Papéis de Usuário

| Papel | Permissões |
|------|-------------|
| Visitante | Ver organizações e ver pets disponíveis |
| Organização | Gerenciar seus próprios pets e fazer login |
| Administrador | Gerenciar organizações e configurar o sistema |

## ✨ Funcionalidades Principais

- Cadastro e autenticação JWT para **Administradores** e **Organizações**
- Cadastro de **Organizações** (restrito a Administradores)
- Cadastro de **Pets** (restrito a Organizações)
- Listagem pública de:
  - Organizações
  - Pets por organização
  - Pet por ID

## 🔒 Segurança

- Senhas armazenadas com **hash usando BCrypt**
- Endpoints protegidos por **role-based authorization**
- Dados sensíveis não são expostos em responses

## 🛠️ Tecnologias Utilizadas

- **ASP.NET Core Web API**
- **Entity Framework Core** (InMemory Database)
- **JWT Authentication**
- **Swagger** para documentação

## 🚀 Como Executar

1. Instale o SDK .NET 7+ (ou superior)
2. Rode o projeto:
   ```sh
   dotnet run
   ```
3. Acesse a documentação Swagger:
   ```
   https://localhost:{porta}/swagger
   ```

Projeto em desenvolvimento.
