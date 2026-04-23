---
tipo: task
id: TASK-20260423100006
status: backlog
feature: [[FEAT-20260423100000 adaptacao de curriculo por vaga com exportacao pdf]]
us: [[US-20260423100001 receber vaga e disparar geracao adaptada]]
---

# Task

## Descricao

Definir a regra oficial de resolucao do curriculo fonte da geracao, respeitando o `resumeId` informado quando valido e recorrendo ao curriculo principal ou selecionado quando o identificador explicito nao vier na requisicao.

## Checklist

- [ ] Reutilizar a regra existente de curriculo principal ou selecionado
- [ ] Impedir acesso a curriculos de outros usuarios
- [ ] Registrar o comportamento para usuarios com multiplos curriculos
- [ ] Definir erro previsivel quando o curriculo resolvido nao existir ou nao pertencer ao usuario
