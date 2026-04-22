---
tipo: us
id: US-20260422162523
status: backlog
feature: [[FEAT-20260422162514 autenticacao e conta do usuario]]
---

# User Story

## Historia

Como **usuario com conta local recem-criada**, quero **confirmar meu email** para **liberar o uso pleno da conta e reduzir risco de cadastro invalido**.

## Criterios de aceite

- [ ] O sistema gera e envia um codigo ou token de confirmacao para o email cadastrado apos o registro local.
- [ ] Enquanto a conta estiver pendente de confirmacao, o sistema comunica claramente esse estado e permite reenvio controlado.
- [ ] A confirmacao bem-sucedida altera o estado da conta e habilita autenticacao plena no sistema.

## Tasks

- [[TASK-20260422162617 definir geracao e envio do codigo ou token de confirmacao]]
- [[TASK-20260422162618 definir comportamento de reenvio]]
- [[TASK-20260422162619 definir mensagens de conta pendente]]
