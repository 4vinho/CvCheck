---
tipo: task
id: TASK-20260422142612
status: done
feature: [[FEAT-20260422142544 configuracao inicial da plataforma]]
us: [[US-20260422142550 definir arquitetura e baseline do projeto]]
---

# Task

## Descricao

Definir a estrategia de separacao entre frontend e backend, estabelecendo fronteiras claras entre responsabilidades, deploys e integracao via API.

## Checklist

- [x] Definir limites de responsabilidade entre as frentes
- [x] Estabelecer que a integracao inicial ocorrera por API
- [x] Validar impacto da separacao no setup local
- [x] Registrar a decisao na US vinculada

## Decisao

- frontend e backend nascem desacoplados desde a raiz do repositorio
- a integracao acontece somente por HTTP API
- o backend deve publicar contrato claro via OpenAPI
- cada frente pode evoluir, testar, versionar e fazer deploy de forma independente
- nao deve existir dependencia direta de build entre frontend e backend

## Impacto no setup local

- cada frente podera ter comando, dependencias e ciclo de desenvolvimento proprio
- o contrato HTTP sera a fronteira oficial para integracao local
- a separacao reduz retrabalho quando uma frente evoluir antes da outra
