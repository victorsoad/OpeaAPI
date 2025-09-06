# OpeaAPI

Uma API RESTful para gerenciamento de clientes empresariais, desenvolvida com .NET 8 e arquitetura Clean Architecture.

## 🚀 Tecnologias Utilizadas

- **.NET 8** - Framework principal
- **ASP.NET Core Web API** - Para criação da API REST
- **Entity Framework Core 9.0** - ORM para acesso a dados
- **SQLite** - Banco de dados
- **MediatR** - Padrão CQRS para separação de comandos e consultas
- **FluentValidation** - Validação de dados
- **Swagger/OpenAPI** - Documentação da API
- **xUnit** - Framework de testes
- **Moq** - Mocking para testes
- **FluentAssertions** - Assertions mais legíveis nos testes

## 🏗️ Arquitetura

O projeto segue os princípios da **Clean Architecture** com separação clara de responsabilidades:

```
OpeaAPI/
├── Api/                    # Camada de Apresentação (Controllers, Middlewares)
├── Application/            # Camada de Aplicação (Commands, Queries, Handlers)
├── Domain/                 # Camada de Domínio (Entities, Enums, Interfaces)
├── Infrastructure/         # Camada de Infraestrutura (DbContext, Repositories)
└── UnitTests/             # Testes Unitários
```

### Camadas da Arquitetura

- **Domain**: Entidades de negócio, enums e interfaces
- **Application**: Lógica de aplicação, comandos, consultas e handlers
- **Infrastructure**: Implementações concretas (banco de dados, repositórios)
- **Api**: Controllers, middlewares e configurações da API

## 📋 Funcionalidades

### Gestão de Clientes
- ✅ **Criar cliente** - Cadastro de novos clientes
- ✅ **Listar clientes** - Consulta de todos os clientes
- ✅ **Buscar cliente** - Consulta de cliente específico por ID
- ✅ **Atualizar cliente** - Edição de dados do cliente
- ✅ **Excluir cliente** - Remoção de cliente

### Validações
- ✅ Validação de nome da empresa (obrigatório, 1-100 caracteres)
- ✅ Validação de porte da empresa (enum válido)
- ✅ Validação automática com FluentValidation

### Recursos Técnicos
- ✅ Middleware de logging de requisições
- ✅ Middleware global de tratamento de exceções
- ✅ Documentação automática com Swagger
- ✅ Testes unitários abrangentes
- ✅ Cobertura de código

## 🚀 Como Executar

### Pré-requisitos
- .NET 8 SDK
- Visual Studio 2022 ou VS Code

### Instalação e Execução

1. **Clone o repositório**
```bash
git clone <url-do-repositorio>
cd OpeaAPI
```

2. **Restaure as dependências**
```bash
dotnet restore
```

3. **Execute as migrações do banco de dados**
```bash
cd Infrastructure
dotnet ef database update
cd ..
```

4. **Execute a API**
```bash
cd Api/Opea.Api
dotnet run
```

5. **Acesse a documentação**
- Swagger UI: `https://localhost:5162/swagger`
- API Base: `https://localhost:5162/api`

## 📚 Endpoints da API

### Base URL
```
https://localhost:5162/api/clientes
```

### Endpoints Disponíveis

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| `GET` | `/api/clientes` | Lista todos os clientes | 200 OK |
| `GET` | `/api/clientes/{id}` | Busca cliente por ID | 200 OK, 404 Not Found |
| `POST` | `/api/clientes` | Cria novo cliente | 201 Created, 400 Bad Request |
| `PUT` | `/api/clientes/{id}` | Atualiza cliente | 200 OK, 400 Bad Request, 404 Not Found |
| `DELETE` | `/api/clientes/{id}` | Remove cliente | 204 No Content, 404 Not Found |

### Exemplos de Uso

#### Criar Cliente
```http
POST /api/clientes
Content-Type: application/json

{
  "nomeEmpresa": "Empresa Exemplo Ltda",
  "porteEmpresa": 1
}
```

**Resposta (201 Created):**
```json
{
  "id": "123e4567-e89b-12d3-a456-426614174000",
  "nomeDaEmpresa": "Empresa Exemplo Ltda",
  "porteDaEmpresa": "Pequena"
}
```

#### Listar Clientes
```http
GET /api/clientes
```

**Resposta (200 OK):**
```json
[
  {
    "id": "123e4567-e89b-12d3-a456-426614174000",
    "nomeDaEmpresa": "Empresa Exemplo Ltda",
    "porteDaEmpresa": "Pequena"
  }
]
```

### Portes de Empresa
- `1` - Pequena
- `2` - Média  
- `3` - Grande

## 🧪 Testes

### Executar Testes
```bash
dotnet test
```

### Executar Testes com Cobertura
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Estrutura dos Testes
- **Unit Tests**: Testes unitários para cada camada
- **Builders**: Padrão Builder para criação de objetos de teste
- **Mocks**: Uso do Moq para simulação de dependências
- **Assertions**: FluentAssertions para assertions mais legíveis

## 🔧 Configuração

### Banco de Dados
O projeto utiliza SQLite como banco de dados padrão. A string de conexão está configurada em `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=Opea.db"
  }
}
```

### Logging
Configuração de logging em `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## 📁 Estrutura do Projeto

```
OpeaAPI/
├── Api/
│   └── Opea.Api/
│       ├── Features/Cliente/          # Controllers e DTOs
│       ├── Middleware/                # Middlewares customizados
│       ├── Extensions/                # Extensões de configuração
│       └── Program.cs                 # Configuração da aplicação
├── Application/
│   ├── Commands/                      # Comandos CQRS
│   ├── Queries/                       # Consultas CQRS
│   ├── Handlers/                      # Handlers do MediatR
│   └── Services/                      # Serviços de aplicação
├── Domain/
│   ├── Entities/                      # Entidades de domínio
│   ├── Enums/                         # Enumerações
│   └── Interfaces/                    # Contratos de domínio
├── Infrastructure/
│   ├── Data/                          # DbContext e configurações
│   ├── Repositories/                  # Implementações dos repositórios
│   └── Migrations/                    # Migrações do banco de dados
└── UnitTests/
    ├── Unit/                          # Testes unitários
    └── Builders/                      # Builders para testes
```

## 🛠️ Desenvolvimento

### Adicionando Nova Funcionalidade

1. **Domain**: Defina a entidade e interfaces necessárias
2. **Application**: Crie commands/queries e handlers
3. **Infrastructure**: Implemente repositórios se necessário
4. **Api**: Crie controllers e DTOs
5. **Tests**: Adicione testes unitários

### Padrões Utilizados

- **CQRS**: Separação entre comandos e consultas
- **Repository Pattern**: Abstração do acesso a dados
- **Dependency Injection**: Inversão de controle
- **FluentValidation**: Validação declarativa
- **Builder Pattern**: Para criação de objetos de teste

## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

## 🤝 Contribuição

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📞 Suporte

Para suporte ou dúvidas, entre em contato através dos issues do repositório.

---

**Desenvolvido com ❤️ usando .NET 8 e Clean Architecture**