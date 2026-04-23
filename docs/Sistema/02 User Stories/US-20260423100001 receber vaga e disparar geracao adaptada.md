---
tipo: us
id: US-20260423100001
status: backlog
feature: [[FEAT-20260423100000 adaptacao de curriculo por vaga com exportacao pdf]]
---

# User Story

## Historia

Como **usuario autenticado**, quero **informar o texto livre da vaga e o template desejado para disparar a geracao da versao adaptada** para **receber um curriculo final alinhado ao contexto da oportunidade sem escolher manualmente cada campo da composicao**.

## Criterios de aceite

- [ ] A operacao exige autenticacao valida e resolve o usuario pelo JWT.
- [ ] A entrada minima aceita `templateId` e `jobPostingText`, com `resumeId` opcional quando houver mais de um curriculo.
- [ ] Texto de vaga vazio, curto demais ou inconsistente falha com erro previsivel antes de chamar a IA.
- [ ] O sistema seleciona apenas curriculos pertencentes ao usuario autenticado.

## Tasks

- [[TASK-20260423100005 definir contrato de entrada da geracao adaptada autenticada]]
- [[TASK-20260423100006 selecionar curriculo fonte principal ou explicitamente informado do usuario]]
