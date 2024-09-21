# SalesAPI - API de Vendas

## Descrição

O **SalesAPI** é uma API RESTful para gerenciar vendas, clientes, filiais e itens de venda. Implementa um CRUD completo, oferecendo endpoints para criar, atualizar, listar e cancelar vendas. A arquitetura segue princípios de **Domain-Driven Design (DDD)**, além de aplicar boas práticas de desenvolvimento como **SOLID**, **Clean Code**, e **Git Flow**.

### Tecnologias Utilizadas

- **.NET 8.0**
- **ASP.NET Core** para a API
- **Dapper** para consultas de dados
- **Serilog** para logging
- **XUnit**, **NSubstitute**, **Bogus** e **FluentAssertions** para testes de unidade e integração
- **Git Flow** para controle de versão

---

## Pré-requisitos

Antes de começar, você precisará das seguintes ferramentas instaladas em seu ambiente de desenvolvimento:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Git](https://git-scm.com/)
- [Postman](https://www.postman.com/) ou outra ferramenta para testar a API (opcional)
- [Git Flow](https://danielkummer.github.io/git-flow-cheatsheet/index.html) (para seguir o modelo de versionamento)

---

## Instalação

### 1. Clonar o repositório:

```bash
git clone https://github.com/seu-usuario/SalesAPI.git
```

### 2. Navegar até o diretório do projeto:

```bash
cd SalesAPI
```

### 3. Restaurar as dependências do projeto:

```bash
dotnet restore
```

### 4. Inicializar o Git Flow (opcional, se você seguir o fluxo de desenvolvimento):

```bash
git flow init
```

---

## Configuração

### Configurando o Serilog

O **Serilog** já está configurado para gravar logs tanto no **console** quanto em **arquivos** com rotação diária. A configuração do **Serilog** está no arquivo `appsettings.json`:

```json
"Serilog": {
  "MinimumLevel": {
    "Default": "Information",
    "Override": {
      "Microsoft": "Warning",
      "System": "Warning"
    }
  },
  "WriteTo": [
    { "Name": "Console" },
    {
      "Name": "File",
      "Args": {
        "path": "Logs/log-.txt",
        "rollingInterval": "Day"
      }
    }
  ],
  "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ]
}
```

---

## Executando o Projeto

### 1. Executar a API:

```bash
dotnet run --project src/SalesAPI
```

A API estará disponível em: `https://localhost:5001` ou `http://localhost:5000`.

### 2. Acessar a Documentação da API (Swagger):

Após iniciar o projeto, você pode acessar a interface Swagger para testar os endpoints:

```
https://localhost:5001/swagger
```

---

## Testando a API

### Testes de Unidade e Integração

O projeto contém testes de unidade e integração usando **XUnit**, **NSubstitute**, **FluentAssertions**, e **Bogus** para simulação de dados. Siga os passos abaixo para executar os testes.

### 1. Rodar todos os testes:

```bash
dotnet test
```

Os testes estão localizados no diretório `tests/` e cobrem tanto os **serviços de domínio** quanto a **API**.

### 2. Rodar testes de integração com Testcontainers (opcional):

Se estiver utilizando **Testcontainers** para executar testes de integração com bancos de dados em contêineres, garanta que você tenha o **Docker** rodando e siga as instruções de configuração no arquivo de teste.

---

## Estrutura do Projeto

A estrutura do projeto segue o modelo **DDD** (Domain-Driven Design) e está organizada em camadas:

```
├── src/
│   ├── SalesAPI/           # Camada de apresentação (API)
│   ├── Sales.Application/  # Camada de aplicação (Serviços)
│   ├── Sales.Domain/       # Camada de domínio (Entidades, Regras de Negócio)
│   ├── Sales.Data/         # Camada de infraestrutura (Repositórios)
├── tests/
│   ├── Sales.Tests/        # Testes de Unidade
│   ├── Sales.Tests.Integration/ # Testes de Integração
```

### Principais Componentes

- **SalesAPI**: Contém os controladores e endpoints da API.
- **Sales.Application**: Serviços de aplicação que coordenam as operações entre repositórios e a lógica de negócios.
- **Sales.Domain**: Entidades de domínio como `Sale`, `SaleItem`, e interfaces de repositórios como `ISaleRepository`.
- **Sales.Data**: Implementações dos repositórios que interagem com a base de dados.

---

## Usando Git Flow

### Iniciando o Git Flow

Se você deseja contribuir para o projeto ou seguir o fluxo de desenvolvimento, inicie o Git Flow no diretório:

```bash
git flow init
```

### Criando uma nova funcionalidade:

```bash
git flow feature start nome-da-feature
```

Após terminar o desenvolvimento, finalize a feature:

```bash
git flow feature finish nome-da-feature
```

### Criando uma release:

```bash
git flow release start v1.0.0
```

Finalize a release:

```bash
git flow release finish v1.0.0
```

### Criando um hotfix:

```bash
git flow hotfix start corrigir-bug
```

Finalize o hotfix:

```bash
git flow hotfix finish corrigir-bug
```

---

## Contribuindo

Ficamos felizes com contribuições! Aqui estão algumas diretrizes:

1. **Crie um branch de feature** para cada nova funcionalidade.
2. **Garanta que os testes estão passando** antes de enviar seu código.
3. Use **commits semânticos** (`feat:`, `fix:`, `chore:`, etc.) para manter um histórico de commits claro.
4. Sempre abra um **Pull Request** para o branch `develop`.

---

## Licença

Este projeto está licenciado sob a Licença MIT. Para mais detalhes, consulte o arquivo [LICENSE](./LICENSE).

---

## Autor

- **Fernando Parmezani** - Desenvolvedor Full Stack - [GitHub](https://github.com/fparmezani)

---

### Informações Adicionais

- Para perguntas ou problemas, abra uma **issue** no repositório GitHub.
- Fique à vontade para enviar **pull requests** com melhorias ou correções.
