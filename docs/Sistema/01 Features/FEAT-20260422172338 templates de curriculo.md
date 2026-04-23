---
tipo: feature
id: FEAT-20260422172338
status: backlog
prioridade: alta
---

# Feature

## Objetivo

Permitir que cada versao de curriculo utilize um template de apresentacao controlado pelo sistema, baseado em um schema canonico unico, para organizar preview e exportacao final sem alterar o conteudo factual do curriculo.

## Valor de negocio

Padronizar a apresentacao final do curriculo com consistencia tecnica e visual, permitindo variedade controlada de layouts sem abrir espaco para contratos soltos, schemas arbitrarios ou customizacao que complique a manutencao do produto.

## Regras de negocio

- [[RN-20260422172431 templates de curriculo devem usar um schema canonico unico]]
- [[RN-20260422172433 a escolha do template pertence a versao do curriculo]]
- [[RN-20260422172434 o catalogo inicial de templates deve ser fechado e controlado pelo sistema]]
- [[RN-20260422172435 template muda apresentacao e nao altera o conteudo factual do curriculo]]
- [[RN-20260422172436 secoes vazias devem seguir regras explicitas de exibicao do template]]
- [[RN-20260422172438 preview e exportacao devem respeitar o mesmo template]]
- [[RN-20260422172439 template invalido deve ser rejeitado de forma previsivel]]
- [[RN-20260422172440 versoes antigas com template descontinuado devem continuar renderizaveis por fallback controlado]]
- [[RN-20260423100019 a resposta da ia deve ser validada contra o schema canonico antes de qualquer persistencia ou renderizacao]]
- [[RN-20260423100021 o renderer de template deve consumir apenas o payload canonico oficial validado]]
