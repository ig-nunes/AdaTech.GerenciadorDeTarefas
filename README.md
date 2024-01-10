# Gerenciador de Tarefas - Projeto Individual Ada Tech

Este projeto foi desenvolvido como parte do módulo de Programação Orientada à Objetos na instituição Ada Tech. O objetivo é criar um gerenciador de tarefas simples com suporte a usuários, tarefas e suas interações.

## Módulos Principais

O projeto é estruturado em módulos, cada um desempenhando um papel específico:

- **Tasks (Tarefas):** Gerenciamento das tarefas, incluindo criação, edição, atribuição de usuários e alteração de status.

- **Users (Usuários):** Representação e gerenciamento de usuários, incluindo desenvolvedores e líderes técnicos.

- **Repositories (Repositórios):** Lida com o armazenamento e recuperação de dados, incluindo a inicialização dos arquivos `tasks.json` e `users.json`.

- **AccessStrategy (Estratégia de Acesso):** Define estratégias de acesso para diferentes tipos de usuários, como líderes técnicos.

## Funcionalidades Principais

1. **Visualizar Todas as Tarefas:**
   - Listar todas as tarefas disponíveis com detalhes como nome, status, descrição, usuário atribuído, data de início, data de término e tarefas relacionadas.

2. **Editar Tarefas:**
   - Editar a descrição de uma tarefa existente.
   - Atribuir um usuário existente a uma tarefa.
   - Alterar o status de uma tarefa.

3. **Criar Nova Tarefa:**
   - Adicionar uma nova tarefa ao sistema.

4. **Visualizar Todos os Usuários:**
   - Exibir uma lista de todos os usuários, incluindo desenvolvedores e líderes técnicos.

5. **Adicionar Usuário:**
   - Registrar um novo usuário no sistema.

6. **Sistema de login:**
   - Fazer login no sistema com um dos tipos possíveis de usuários. 

## Inicialização do Projeto

1. **Inicialização do Gerenciador de Tarefas:**
   - Ao rodar o programa, inicialmente será criado os arquivos `tasks.json` e `users.json` com alguns dados já adicionados, para poder logar no sistema.

2. **Configuração dos arquivos tasks.json e users.json:**
   - Depois da primeira execução do programa, certifique-se de que o arquivo `tasks.json` e `users.json` contém dados válidos e segue o formato esperado.



## Como Usar

- Clone esse projeto.
- Execute o aplicativo.
- Siga as instruções no console para navegar pelas opções disponíveis.


