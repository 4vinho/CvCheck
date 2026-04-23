---
tipo: task
id: TASK-20260422172415
status: backlog
feature: [[FEAT-20260422172338 templates de curriculo]]
us: [[US-20260422172349 validar template selecionado na versao do curriculo]]
---

# Task

## Descricao

Definir a validacao do template selecionado para impedir que versoes do curriculo sejam persistidas com identificadores inexistentes ou nao suportados.

## Checklist

- [ ] Definir a validacao de `template_id` contra o catalogo oficial
- [ ] Definir a resposta esperada para template inexistente
- [ ] Garantir que a versao invalida nao seja persistida
- [ ] Registrar a regra de validacao no vault
