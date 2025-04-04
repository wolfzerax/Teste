Windows Forms + API em ASP.NET Core com SQLite

Este projeto consiste em uma API desenvolvida em ASP.NET Core com banco de dados SQLite, integrada a um aplicativo Windows Forms para gerenciar produtos e categorias. O sistema inclui autenticação via JWT, permissões de usuário e um módulo de estatísticas.

🛠️ Tecnologias Utilizadas

ASP.NET Core 5 (Back-end da API)

Entity Framework Core (ORM para acesso ao SQLite)

Windows Forms (Interface gráfica do usuário)

SQLite (Banco de dados leve)

JWT Authentication (Segurança e autenticação de usuários)

HttpClient (Consumo da API pelo Windows Forms)

📁 Estrutura do Projeto

/
├── MinhaApiComSQLite/  # Código da API
│   ├── Controllers/     # Endpoints da API
│   ├── Data/            # Contexto do banco de dados
│   ├── Models/          # Modelos de dados
│   ├── Repository/      # Repositórios de acesso ao banco
│   ├── Services/        # Lógica de negócios
│   ├── appsettings.json # Configurações da API
│   ├── Startup.cs       # Configuração do projeto
│
├── WindowsFormsAPI/     # Código do Windows Forms
│   ├── Forms/           # Telas do aplicativo
│   ├── Program.cs       # Inicialização do Windows Forms
│   ├── App.config       # Configurações do app
│
└── README.md            # Documentação do projeto

🚀 Como Rodar o Projeto

🔹 1. Configurar e Rodar a API

1️⃣ Instalar as Dependências

Se ainda não instalou o .NET 5, faça isso aqui.

Em seguida, instale os pacotes necessários rodando o seguinte comando no diretório da API:

cd MinhaApiComSQLite

dotnet restore

2️⃣ Configurar o Banco de Dados

Antes de rodar a API, aplique as migrações para criar o banco de dados SQLite:

dotnet ef database update

3️⃣ Rodar a API

Para iniciar a API, execute:

dotnet run

A API será iniciada em https://localhost:5001/.

🔹 2. Rodar o Windows Forms

1️⃣ Configurar as Dependências

Abra o projeto WindowsFormsAPI no Visual Studio e restaure os pacotes NuGet.

2️⃣ Rodar o Aplicativo

No Visual Studio, defina WindowsFormsAPI como projeto de inicialização e pressione F5.

🔐 Autenticação e Uso


🔹 1. Autenticar e Obter Token JWT

Faça login com o usuário criado:

POST /api/Auth/login

{
  "email": "admin",
  "senha": "123"
}

A resposta conterá um token JWT que deve ser usado para acessar as rotas protegidas da API.

🔹 3. Usar o Token no Windows Forms

Após o login, o token JWT será armazenado e utilizado nas requisições para carregar produtos e categorias.

📊 Relatórios e Estatísticas

O botão de estatísticas na interface gráfica calcula e exibe a média de preços dos produtos cadastrados na API.

Endpoint utilizado:

GET /api/Relatorio/

📌 Funcionalidades Implementadas

✅ Autenticação JWT
✅ Cadastro, edição e exclusão de produtos e categorias
✅ Interface gráfica com Windows Forms
✅ Consulta de estatísticas (média de preços)
✅ Banco de dados SQLite integrado
✅ Proteção de rotas com [Authorize]
✅ Consumo da API via HttpClient

🛠️ Próximos Passos (Melhorias Futuras)

🔹 Adicionar gráficos nos relatórios de estatísticas 📊
🔹 Melhorar a UI com estilização no Windows Forms 🎨
🔹 Implementar diferentes níveis de permissão de usuário 🔑
🔹 Criar logs para monitoramento das requisições 📝

📌 Contribuição

Pull requests são bem-vindos! Se encontrar algum bug ou tiver sugestões de melhorias, abra uma issue no GitHub. 🚀
