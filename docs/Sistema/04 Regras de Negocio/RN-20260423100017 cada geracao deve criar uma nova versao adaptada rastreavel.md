---
tipo: regra
id: RN-20260423100017
---

# Regra de Negocio

## Descricao

Cada solicitacao de adaptacao deve gerar uma nova versao ligada ao usuario autenticado, ao curriculo base utilizado, ao template escolhido e ao texto da vaga, preservando rastreabilidade mesmo quando a execucao terminar em falha.

## Impacta

- [[FEAT-20260423100000 adaptacao de curriculo por vaga com exportacao pdf]]
- [[US-20260423100003 persistir versao adaptada gerada]]
