Teste para Desenvolvedor Pleno

📌 Objetivo do Projeto

Este projeto consiste na criação de uma **API RESTful** em **C# com ASP.NET Core e SQLite**, além de um **aplicativo WinForms** que consome essa API. O objetivo é aplicar boas práticas de desenvolvimento para garantir eficiência, segurança e escalabilidade.

🔧 Tecnologias Utilizadas

- **ASP.NET Core** (Desenvolvimento da API)
- **Entity Framework Core** (ORM para persistência de dados)
- **SQLite** (Banco de dados leve para armazenamento)
- **JWT (JSON Web Token)** (Autenticação e segurança)
- **WinForms** (Interface gráfica para consumo da API)
- **HttpClient** (Consumo de API no cliente WinForms)
- **ILogger** (Monitoramento e logs)
- **xUnit** (Testes unitários)

📂 Estrutura do Projeto

```
📁 MinhaApiComSQLite
│── 📁 Controllers
│── 📁 Services
│── 📁 Repositories
│── 📁 DTOs
│── 📁 Models
│── 📁 Data (Contexto do banco de dados)
│── 📁 Tests (Testes unitários)
│── Program.cs
│── Startup.cs
```

🚀 Como Executar o Projeto

1️⃣ Clonando o Repositório

```bash
git clone <URL_DO_REPOSITORIO>
cd TesteDevAPI
```

2️⃣ Configurando o Banco de Dados

- O banco de dados **SQLite** já está configurado no projeto.
- Para aplicar as migrações, execute:

```bash
dotnet ef database update
```
A API estará disponível em `http://localhost:5000`.

📌 Funcionalidades Implementadas

API

✅ CRUD de Produtos e Categorias\
✅ Autenticação via JWT\
✅ Paginação de produtos\
✅ Registro de logs com ILogger\
✅ Histórico de preços e relatórios

Aplicação WinForms

✅ Interface gráfica com **DataGridView**\
✅ Botões para **Criar, Atualizar e Excluir** produtos\
✅ Consumo da API com **HttpClient**\
✅ Uso de **Models** para manipulação de dados

📜 Exemplo de Requisição

Criar Produto (POST)

```json
POST /api/produtos
{
  "nome": "Produto Exemplo",
  "preco": 50.00,
  "categoriaId": 1
}
```

✅ Critérios de Avaliação

- Implementação correta dos requisitos funcionais e técnicos.
- Uso de boas práticas de código e arquitetura.
- Cobertura de testes unitários.
- Documentação clara e objetiva.

---

✉️ **Dúvidas? Entre em contato!**

