---
tipo: us
id: US-20260422162522
status: backlog
feature: [[FEAT-20260422162514 autenticacao e conta do usuario]]
---

# User Story

## Historia

Como **visitante**, quero **criar uma conta local com email e senha** para **passar a ter um acesso proprio e persistente ao sistema**.

## Criterios de aceite

- [x] O cadastro aceita os campos minimos da conta e valida email, senha e confirmacao de senha conforme as regras do Identity.
- [x] Uma conta local valida e criada com sucesso fica associada ao email como identificador principal.
- [x] A conta criada ainda nao tem uso pleno do sistema ate concluir a confirmacao de email.

## Tasks

- [[TASK-20260422162612 definir campos minimos da conta]]
- [[TASK-20260422162613 definir validacoes de cadastro]]
- [[TASK-20260422162615 implementar criacao da conta via identity]]
- [[TASK-20260422162616 bloquear uso pleno antes da confirmacao]]
