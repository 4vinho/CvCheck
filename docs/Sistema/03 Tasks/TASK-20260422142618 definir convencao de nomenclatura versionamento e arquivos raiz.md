---
tipo: task
id: TASK-20260422142618
status: done
feature: [[FEAT-20260422142544 configuracao inicial da plataforma]]
us: [[US-20260422142550 definir arquitetura e baseline do projeto]]
---

# Task

## Descricao

Definir convencoes iniciais de nomenclatura, versionamento e arquivos raiz do projeto para manter consistencia tecnica desde a primeira entrega.

## Checklist

- [x] Definir padrao de nomenclatura para pastas e projetos
- [x] Delimitar estrategia inicial de versionamento
- [x] Listar arquivos raiz obrigatorios do repositorio
- [x] Atualizar o backlog tecnico com as convencoes

## Convencoes aprovadas

- nomes de pastas, projetos e namespaces em ingles tecnico
- nome produto usado como base para solution, projetos e namespaces: `CvCheck`
- nomes curtos e previsiveis para solution e assemblys futuros
- IDs dos artefatos funcionais permanecem no formato atual dentro de `docs/`

## Versionamento

- o produto inicia em `0.x`
- a troca para `1.x` so deve acontecer quando o MVP estiver estabilizado

## Arquivos raiz obrigatorios

- `README.md`
- `.gitignore`
- `.editorconfig`
- `docs/`

## Observacao

As orientacoes detalhadas de setup local e onboarding continuam pertencendo a [[US-20260422142601 definir padroes compartilhados e dev experience]].
