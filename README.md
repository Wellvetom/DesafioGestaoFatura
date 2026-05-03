# 📄 Desafio Gestão de Faturas

## 📌 Descrição

Aplicação desenvolvida em **ASP.NET Core** para gestão de faturas e seus respectivos itens, contemplando operações de CRUD, regras de negócio, persistência de dados, API REST e testes automatizados.

---

## 🚀 Tecnologias Utilizadas

* .NET 6+
* C#
* Entity Framework Core
* SQL Server
* FluentValidation
* xUnit
* FluentAssertions
* Swagger (Swashbuckle)

---

## 🧱 Arquitetura

A aplicação foi organizada em camadas:

```id="arch"
DesafioGestaoFatura/
 ┣ API            → Camada de apresentação (controllers)
 ┣ Application    → Serviços, DTOs e validações
 ┣ Domain         → Entidades e regras de negócio
 ┣ Infrastructure → Persistência (EF Core, DbContext, Repositories)
 ┗ Tests          → Testes automatizados
```

---

## 📦 Entidades

### Fatura

* Id
* Número
* Nome do Cliente
* Data de Emissão
* Status (Aberta / Fechada)
* Valor Total

### ItemFatura

* Id
* Descrição
* Quantidade
* Valor Unitário
* Valor Total
* Justificativa

---

## ⚙️ Regras de Negócio

* Fatura inicia com status **Aberta**
* Nome do cliente é obrigatório
* Fatura pode conter um ou mais itens
* Valor total da fatura é recalculado automaticamente
* Fatura fechada não pode ser alterada
* Itens não podem ser adicionados/editados/removidos em fatura fechada
* Itens com valor acima de **R$ 1.000** exigem justificativa
* Descrição do item é obrigatória (mínimo de 3 caracteres)
* Não é permitido fechar uma fatura já fechada
* É possível fechar uma fatura

---

## 🔗 Endpoints

### Faturas

* `POST /faturas`
* `GET /faturas`
* `GET /faturas/{id}`
* `PUT /faturas/{id}/fechar`

### Itens

* `POST /faturas/{id}/itens`

---

## 🔎 Filtros disponíveis

Consulta de faturas permite:

* Nome do cliente
* Data inicial
* Data final
* Status

---

## 🧪 Testes Automatizados

A solução possui testes cobrindo regras de negócio e validações de entrada.

---

### 🔹 Testes de Domínio

Garantem que as regras de negócio sejam respeitadas.

#### ✔ Cenários cobertos

* Criação de fatura válida
* Bloqueio de criação de fatura sem cliente
* Fechamento de fatura
* Bloqueio de fechamento de fatura já fechada
* Validação de item acima de R$ 1.000 sem justificativa
* Validação de item com:

  * descrição inválida
  * quantidade inválida

#### 📌 Exemplo

```csharp id="domainex"
Action act = () => new ItemFatura("Item", 1, 2000, null);

act.Should().Throw<DomainException>();
```

---

### 🔹 Testes de Validação (FluentValidation)

Garantem que entradas inválidas sejam tratadas antes de chegar ao domínio.

#### ✔ Cenários cobertos

* Descrição obrigatória
* Nome do cliente obrigatório
* Justificativa obrigatória para itens acima de R$ 1.000

#### 📌 Exemplo

```csharp id="valex"
var result = validator.Validate(dto);

result.IsValid.Should().BeFalse();
result.Errors.Should().Contain(e => e.ErrorMessage == "Descrição inválida");
```

---

### 🧠 Estratégia de validação

* **FluentValidation** → valida entrada (DTO)
* **Domain** → valida regras críticas de negócio

---

## 🔐 Validação e Segurança

* Validação de entrada com **FluentValidation**
* Regras críticas protegidas no **Domain**
* Uso de DTOs para evitar exposição direta das entidades
* Prevenção de ciclos de serialização
* Tratamento centralizado de exceções

---

## ⚠️ Tratamento Global de Erros

Middleware responsável por capturar exceções e padronizar respostas.

### ✔ Regras

* `DomainException` → **400 (BadRequest)**
* Entidades não encontradas → **404 (NotFound)**
* Demais erros → **500 (Internal Server Error)**

---

### 📌 Exemplo de resposta

```json id="errjson"
{
  "message": "Mensagem de erro",
  "statusCode": 400
}
```

---

## 📄 Execução do Projeto

### 🔹 Pré-requisitos

* .NET SDK instalado
* SQL Server

---

### 🔹 Configuração

Editar:

```id="appsettings"
appsettings.json
```

```json id="conn"
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=DesafioFaturas;Trusted_Connection=True;"
}
```

---

## 🗄️ Banco de Dados

A aplicação utiliza Entity Framework Core com migrations.

Para criar a base de dados e aplicar as tabelas, execute:

```bash
dotnet ef database update
```
### 🔹 Executar aplicação

```
dotnet run
```
md
Caso necessário, as migrations podem ser recriadas com:

```bash
dotnet ef migrations add InitialCreat
```

### 🔹 Swagger

```id="swagger"
https://localhost:{porta}/swagger
```

---

## 🧪 Executar Testes

```bash id="tests"
dotnet test
```

---

## 📌 Decisões Técnicas

* Arquitetura em camadas
* Uso de DTOs para desacoplamento
* FluentValidation para validação de entrada
* Regras de negócio centralizadas no domínio
* Middleware global para tratamento de exceções
* Testes automatizados separados por responsabilidade
* Uso de FluentAssertions para legibilidade

---

## ⚠️ Premissas

* Não foi implementado autenticação/autorização
* Foco em regras de negócio e organização da solução

---

## 🚀 Melhorias Futuras

* Autenticação com JWT
* Paginação e ordenação
* Cache com Redis
* Logging estruturado (Serilog)
* Testes de integração
* AutoMapper
* CI/CD

---

## 👨‍💻 Autor do Projeto

Wellington Almeida
