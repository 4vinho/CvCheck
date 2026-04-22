---
tipo: us
id: US-20260422162529
status: backlog
feature: [[FEAT-20260422162514 autenticacao e conta do usuario]]
---

# User Story

## Historia

Como **usuario autenticado**, quero **alterar minha senha informando a senha atual** para **manter a seguranca da conta sob meu controle**.

## Criterios de aceite

- [ ] A troca de senha exige a senha atual antes de aceitar a nova senha.
- [ ] A nova senha precisa obedecer as validacoes adotadas pelo Identity.
- [ ] A atualizacao bem-sucedida confirma a troca e preserva a consistencia das regras de autenticacao.

## Tasks

- [[TASK-20260422162635 exigir senha atual]]
- [[TASK-20260422162637 validar nova senha]]
- [[TASK-20260422162638 confirmar atualizacao com regras do identity]]
