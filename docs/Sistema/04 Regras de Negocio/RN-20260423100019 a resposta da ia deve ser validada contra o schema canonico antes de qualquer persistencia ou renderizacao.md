---
tipo: regra
id: RN-20260423100019
---

# Regra de Negocio

## Descricao

Toda resposta da IA no fluxo de adaptacao deve ser tratada como insumo intermediario e precisa ser validada contra o schema canonico oficial antes de qualquer persistencia definitiva, renderizacao HTML ou conversao para PDF.

## Impacta

- [[FEAT-20260423100000 adaptacao de curriculo por vaga com exportacao pdf]]
- [[FEAT-20260422172338 templates de curriculo]]
- [[US-20260423100002 montar versao adaptada canonica via ia]]
- [[US-20260423100004 renderizar pdf final com o template escolhido]]
