# Introdução

Esse projeto tem como objetivo demonstrar a utilização de todos os conhecimentos adquiridos durante a **Fase 3** na Pós Graduação da FIAP em **Desenvolvimento .NET e Azure**.

## Requisitos

O Tech Challenge desta fase será a evolução da API de notícias desenvolvida na fase 2. Os requisitos são:

1. Criar os testes necessários para sua aplicação, sendo eles testes unitários e um teste de integração para verificar se os dados estão sendo salvos e carregados corretamente em seu banco de dados.
2. Adicionar o Application Insights à sua aplicação para que ela tenha um monitoramento básico.
3. Criar uma pipeline no Azure ou Github Actions para demonstrar os testes unitários e de integração passando com o Docker.

## Resolução

### Requisito 1

Visando aplicar os conhecimentos de testes aprendidos nessa fase, foi implementado um total de **14** cenários de testes, sendo:

- **5** testes de unidade
- **3** testes funcionais utilizando o SpecFlow
- **6** testes de integração

Os cenários descritos acima possibilitaram uma cobertura de testes de **96%**, conforme imagem abaixo:

![imagem cobertura](/docs/img_coverage.png "Cobertura do projeto.")

Para mais detalhes, acesso o relatório detalhado: [Relatório](docs/code_coverage.html)

___

#### Testes de Unidade

O foco dos testes de unidade foi basicamente nas camadas de `Domain` e `Application` por ser considerada as partes onde possuiam mais lógicas de negócios. Tecnologias utilizadas:

- **AutoFixture v. 4.18** para geração de massa de dados;
- **Moq v. 4.20** para criação de mocks;
- **FluentAssertations v. 6.12** para utilização da sintaxe fluentes nas assertações.

Os seguintes cenários foram testados:

- `Should_CreateNews_When_CommandIsValid`
- `Should_CreateUser_When_UserNotExists`
- `Should_ThrowException_When_UserAlreadyExists`
- `Should_CreateNewsInstance_When_AllFieldsAreValid`
- `Should_CreateUserInstance_When_AllFieldsAreValid`

___

#### Testes de Integração

Os testes de integração visavam proporcionar uma experiência fluída para os consumidores da API disponibilizada, buscando garantir que as requisições e respostas fossem aderentes. Os testes se concentraram em chamadas nas `Controllers`.Tecnologias utilizadas:

- **AutoFixture v. 4.18** para geração de massa de dados;
- **Moq v. 4.20** para criação de mocks;
- **FluentAssertations v. 6.12** para utilização da sintaxe fluentes nas assertações.
- **Microsoft.AspNetCore.Mvc.Testing v. 6.0.22** para execução da aplicação em memória.

Os seguintes cenários foram testados:

- `Should_ReturnStatusCode200OkWithToken_When_PostAuthenticateEndpointSuccess`
- `Should_ReturnStatusCode401Unauthorized_When_PostAuthenticateEndpointFail`
- `Should_ReturnStatusCode200Ok_When_PostAddNewsEndpointSuccess`
- `Should_ReturnStatusCodeSuccessOkWithNewsList_When_GetAllNewsEndpointSuccess`
- `Should_ReturnStatusCode201Created_When_PostCreateUserEndpointSuccess`
- `Should_ReturnStatusCode500InternalServerError_When_PostCreateUserEndpointFail`

___

#### Testes de Funcionais

Os testes funcionais foram um extra em relação aos requisitos solicitados. Visando praticar a utilização do `SpecFlow`, foram desenvolvidos os seguintes cenários:

```feature
Funcionalidade: Criação de notícias

@createUser
Cenario: Criação de uma notícia.
    Dado que uma notícia precise ser cadastrada
    E os seguintes parâmetros válidos da notícia sejam informados:
        | Title     | Content     | Date       | Author         |
        | NewsTitle | NewsContent | 2023-09-10 | AuthorFullname |
    Quando solicitado o cadastro da notícia
    Então a notícia deve ser cadastrada com sucesso
```

```feature
Funcionalidade: Criação de usuário.

@createUser
Cenario: Criação de um novo usuário.
    Dado que um usuário precise ser cadastrado
    E os seguintes parâmetros válidos sejam informados:
        | Username | Password | Role  |
        | username | password | admin |
    E o mesmo não exista
    Quando solicitado o cadastro
    Então o usuário deve ser cadastrado com sucesso

@createUser
Cenario: Erro ao tentar criar um usuário existente.
    Dado que um usuário precise ser cadastrado
    E os seguintes parâmetros válidos sejam informados:
        | Username | Password | Role  |
        | username | password | admin |
    E o mesmo já exista
    Quando solicitado o cadastro
    Então deve retornar um erro com a seguinte mensagem: "This user already exists."

```

### Requisito 2

TODO.

### Requisito 3

Para atender esse requisito, foi necessário realizar pequenas alterações na pipeline. Por questões de exploração das funcionalidades e perfomance, separamos os testes de integração e funcionais em estágios separados. A pipeline ficou da seguinte forma:

![imagem pipeline projeto](/docs/img_pipeline_tests.png "Pipeline do projeto.")

Além disso os resultados foram gerados e disponibilizados via Azure DevOps na guia Tests, conforme a imagem abaixo:

![imagem pipeline testes](/docs/img_tests_runs.png "Testes executados")
# PosTech.TechChallenge.PhaseThree
