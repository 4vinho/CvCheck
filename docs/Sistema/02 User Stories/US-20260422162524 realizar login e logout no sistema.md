---
tipo: us
id: US-20260422162524
status: backlog
feature: [[FEAT-20260422162514 autenticacao e conta do usuario]]
---

# User Story

## Historia

Como **usuario com conta existente**, quero **entrar e sair do sistema com seguranca** para **acessar meus recursos autenticados apenas quando a conta estiver apta**.

## Criterios de aceite

- [ ] O login com email e senha funciona para contas validas e confirmadas.
- [ ] Contas locais ainda nao confirmadas nao conseguem acessar o sistema plenamente pelo fluxo de login.
- [ ] O logout encerra a sessao atual e impede continuidade de uso autenticado ate novo login.

## Tasks

- [[TASK-20260422162621 definir fluxo de login com email e senha]]
- [[TASK-20260422162622 definir bloqueio para conta nao confirmada]]
- [[TASK-20260422162623 definir encerramento de sessao]]
