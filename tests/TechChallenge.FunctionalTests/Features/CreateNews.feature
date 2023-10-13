Funcionalidade: Criação de notícias.

    @createUser
    Cenario: Criação de uma notícia.
        Dado que uma notícia precise ser cadastrada
        E os seguintes parâmetros válidos da notícia sejam informados:
          | Title     | Content     | Date       | Author         |
          | NewsTitle | NewsContent | 2023-09-10 | AuthorFullname |
        Quando solicitado o cadastro da notícia
        Então a notícia deve ser cadastrada com sucesso