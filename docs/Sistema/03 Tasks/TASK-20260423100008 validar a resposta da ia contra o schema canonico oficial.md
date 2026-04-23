---
tipo: task
id: TASK-20260423100008
status: backlog
feature: [[FEAT-20260423100000 adaptacao de curriculo por vaga com exportacao pdf]]
us: [[US-20260423100002 montar versao adaptada canonica via ia]]
---

# Task

## Descricao

Definir a validacao estrutural e semantica da resposta da IA contra o schema canonico oficial antes de qualquer persistencia definitiva ou renderizacao de template.

## Checklist

- [ ] Validar formato JSON e aderencia ao schema canonico
- [ ] Rejeitar secoes, fatos ou campos fora do contrato oficial
- [ ] Encerrar a operacao com erro claro quando a validacao falhar
- [ ] Garantir que o renderer nunca receba resposta bruta ou nao validada da IA
