---
tipo: task
id: TASK-20260422142622
status: done
feature: [[FEAT-20260422142544 configuracao inicial da plataforma]]
us: [[US-20260422142554 preparar backend base em net 10]]
---

# Task

## Descricao

Planejar a criacao do projeto base `ASP.NET Core Web API` em `.NET 10`, definindo a estrutura minima necessaria para iniciar o backend.

## Checklist

- [x] Confirmar o template base do backend
- [x] Definir a estrutura inicial do projeto da API
- [x] Validar aderencia ao uso de .NET 10
- [x] Registrar o escopo da entrega no backlog

## Implementacao

- template utilizado: `dotnet new webapi --use-controllers --framework net10.0`
- SDK fixado em `10.0.203`
- solution criada como `src/backend/CvCheck.slnx`
- projetos materializados:
  - `CvCheck.Api`
  - `CvCheck.Application`
  - `CvCheck.Domain`
  - `CvCheck.Infrastructure`
  - `CvCheck.Api.Tests`

## Observacao

O scaffold foi mantido sem banco, `Identity` e regras de negocio, respeitando o recorte desta historia.
