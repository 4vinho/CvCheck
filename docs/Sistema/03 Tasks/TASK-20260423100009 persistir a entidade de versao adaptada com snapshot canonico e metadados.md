---
tipo: task
id: TASK-20260423100009
status: backlog
feature: [[FEAT-20260423100000 adaptacao de curriculo por vaga com exportacao pdf]]
us: [[US-20260423100003 persistir versao adaptada gerada]]
---

# Task

## Descricao

Definir a entidade ou agregado de versao adaptada, ligada ao usuario autenticado, ao curriculo base, ao template escolhido e ao texto da vaga, persistindo o snapshot canonico validado e os metadados da geracao.

## Checklist

- [ ] Definir identidade e relacionamentos da versao adaptada
- [ ] Registrar `jobPostingText`, `templateId`, status e timestamps
- [ ] Persistir o payload canonico validado como snapshot imutavel da geracao
- [ ] Garantir rastreabilidade entre versao adaptada, curriculo base e usuario
