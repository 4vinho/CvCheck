---
tipo: task
id: TASK-20260422172403
status: backlog
feature: [[FEAT-20260422172338 templates de curriculo]]
us: [[US-20260422172344 escolher template para uma versao do curriculo]]
---

# Task

## Descricao

Definir como o identificador do template passa a fazer parte da entidade de versao do curriculo, permitindo rastrear qual apresentacao final aquela versao utiliza.

## Checklist

- [ ] Definir o campo `template_id` na versao do curriculo
- [ ] Garantir que a associacao ocorra por versao e nao por usuario
- [ ] Registrar a regra de persistencia no vault
- [ ] Confirmar os impactos desse campo em preview e exportacao
