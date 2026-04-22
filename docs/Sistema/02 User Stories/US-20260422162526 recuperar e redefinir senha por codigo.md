---
tipo: us
id: US-20260422162526
status: backlog
feature: [[FEAT-20260422162514 autenticacao e conta do usuario]]
---

# User Story

## Historia

Como **usuario que perdeu a senha**, quero **solicitar recuperacao e redefinir a senha por codigo recebido por email** para **retomar o acesso sem depender de atendimento manual**.

## Criterios de aceite

- [ ] O usuario consegue solicitar recuperacao informando o email da conta.
- [ ] O sistema envia um codigo de recuperacao por email e rejeita redefinicao com codigo invalido ou expirado.
- [ ] A redefinicao concluida com sucesso permite login posterior com a nova senha.

## Tasks

- [[TASK-20260422162624 solicitar recuperacao por email]]
- [[TASK-20260422162625 gerar e enviar codigo]]
- [[TASK-20260422162626 validar codigo e redefinir senha]]
