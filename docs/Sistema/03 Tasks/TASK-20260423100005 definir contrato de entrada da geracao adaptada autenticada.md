---
tipo: task
id: TASK-20260423100005
status: backlog
feature: [[FEAT-20260423100000 adaptacao de curriculo por vaga com exportacao pdf]]
us: [[US-20260423100001 receber vaga e disparar geracao adaptada]]
---

# Task

## Descricao

Definir a operacao autenticada de geracao adaptada com entrada minima em `templateId` e `jobPostingText`, admitindo `resumeId` opcional para cenarios com multiplos curriculos do mesmo usuario.

## Checklist

- [ ] Registrar o contrato minimo da requisicao
- [ ] Explicitar a dependencia de autenticacao por JWT
- [ ] Definir validacoes previsiveis para texto de vaga vazio, curto ou inconsistente
- [ ] Registrar a ordem de validacao antes do acionamento da IA
