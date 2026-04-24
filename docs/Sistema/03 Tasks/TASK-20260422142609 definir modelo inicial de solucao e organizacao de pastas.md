---
tipo: task
id: TASK-20260422142609
status: done
feature: [[FEAT-20260422142544 configuracao inicial da plataforma]]
us: [[US-20260422142550 definir arquitetura e baseline do projeto]]
---

# Task

## Descricao

Definir como o projeto sera organizado desde o inicio, incluindo separacao macro entre aplicacoes, convencoes de raiz e distribuicao inicial de pastas para cada frente.

## Checklist

- [x] Definir estrutura macro do repositorio
- [x] Registrar a distribuicao inicial entre frontend e backend
- [x] Validar se a estrutura favorece evolucao incremental
- [x] Atualizar a documentacao base da feature

## Definicao aprovada

```text
.
|-- docs/
|-- src/
|   |-- backend/
|   `-- frontend/
|-- tests/
|   `-- backend/
|-- .editorconfig
|-- .gitignore
`-- README.md
```

## Observacoes

- `src/backend` fica reservado para a futura solution e projetos da API
- `src/frontend` fica reservado para a futura aplicacao Vue 3
- `tests/backend` concentra os testes automatizados do backend
- `docs/` permanece como fonte oficial de rastreabilidade e backlog
- a estrutura favorece evolucao incremental por permitir tanto projeto unico quanto futuras extracoes sem reorganizacao de raiz
