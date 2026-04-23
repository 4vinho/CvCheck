---
tipo: us
id: US-20260423100003
status: backlog
feature: [[FEAT-20260423100000 adaptacao de curriculo por vaga com exportacao pdf]]
---

# User Story

## Historia

Como **usuario autenticado**, quero **que cada geracao adaptada fique registrada como uma nova versao ligada ao meu curriculo e ao template usado** para **ter historico rastreavel de execucoes, falhas e documentos emitidos**.

## Criterios de aceite

- [ ] Cada solicitacao cria uma nova versao ligada ao usuario, ao curriculo base, ao template e ao texto da vaga.
- [ ] A versao persistida registra status, timestamps, payload canonico validado e detalhes de falha quando houver erro.
- [ ] Falhas de IA, validacao ou renderizacao permanecem rastreaveis por status e mensagem de erro.
- [ ] Nenhuma versao invalida e considerada pronta para renderizacao.

## Tasks

- [[TASK-20260423100009 persistir a entidade de versao adaptada com snapshot canonico e metadados]]
- [[TASK-20260423100013 tratar erros e rastreabilidade da geracao adaptada ponta a ponta]]
