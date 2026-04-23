---
tipo: us
id: US-20260422172349
status: backlog
feature: [[FEAT-20260422172338 templates de curriculo]]
---

# User Story

## Historia

Como **produto**, quero **validar o template selecionado na versao do curriculo** para **evitar inconsistencias de renderizacao e tratar templates invalidos de forma segura**.

## Criterios de aceite

- [ ] O sistema rejeita template inexistente ou nao suportado na criacao e atualizacao da versao.
- [ ] O sistema define comportamento controlado para versoes antigas que referenciam template descontinuado.
- [ ] O usuario nao consegue persistir uma versao em estado invalido de template.

## Tasks

- [[TASK-20260422172415 rejeitar template id invalido ou inexistente]]
- [[TASK-20260422172416 definir fallback para template descontinuado em versoes antigas]]
