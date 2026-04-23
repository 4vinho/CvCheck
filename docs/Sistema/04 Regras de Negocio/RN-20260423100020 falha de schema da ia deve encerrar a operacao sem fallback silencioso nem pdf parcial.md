---
tipo: regra
id: RN-20260423100020
---

# Regra de Negocio

## Descricao

Quando a resposta da IA nao satisfizer o schema canonico oficial, a operacao deve falhar de forma clara e previsivel, sem fallback automatico para o curriculo base, sem marcacao de sucesso e sem emissao de PDF parcial.

## Impacta

- [[FEAT-20260423100000 adaptacao de curriculo por vaga com exportacao pdf]]
- [[US-20260423100002 montar versao adaptada canonica via ia]]
- [[US-20260423100003 persistir versao adaptada gerada]]
