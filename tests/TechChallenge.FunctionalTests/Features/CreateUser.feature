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