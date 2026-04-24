---
tipo: us
id: US-20260422142554
status: done
feature: [[FEAT-20260422142544 configuracao inicial da plataforma]]
---

# User Story

## Historia

Como **time tecnico**, quero **subir o backend base em .NET 10 com API inicial e configuracoes essenciais** para **ter uma fundacao pronta para evolucao das proximas funcionalidades**.

## Dependencia explicita

Esta historia depende de [[US-20260422142550 definir arquitetura e baseline do projeto]] e deve respeitar integralmente a baseline aprovada nela.

Antes de iniciar esta US, a implementacao deve assumir como fixo:

- solution e projetos sob `src/backend`
- API no estilo `ASP.NET Core Web API` com `Controllers`
- organizacao em projeto unico `CvCheck.Backend`, com modulos internos por pastas
- exposicao do contrato da API por OpenAPI
- ausencia intencional de banco funcional, `Identity` e regras de negocio nesta primeira fundacao tecnica

## Criterios de aceite

- [x] O projeto backend base em .NET 10 esta definido no backlog com estrutura e configuracoes iniciais.
- [x] A pipeline minima da API, incluindo OpenAPI, foi considerada nas tasks da historia.
- [x] As bibliotecas e convencoes essenciais do backend foram delimitadas sem extrapolar para CI/CD completo.

## Entrega implementada

### Estrutura criada

- `src/backend/CvCheck.slnx`
- `src/backend/src/CvCheck.Backend`
- `tests/backend/CvCheck.Backend.Tests`

### Baseline tecnica materializada

- SDK fixado em `.NET 10.0.203` via `global.json`
- API criada em `ASP.NET Core Web API` com `Controllers`
- pipeline minima com `ProblemDetails`, exception handler global, `HealthChecks`, OpenAPI e Swagger UI em desenvolvimento
- endpoint institucional `GET /api/platform`
- endpoint operacional `GET /health`
- composicao centralizada no projeto `CvCheck.Backend`, com modulos internos e extensao unica de registro

### Bibliotecas adotadas

- `Microsoft.AspNetCore.OpenApi`
- `Swashbuckle.AspNetCore`
- `MediatR`
- `FluentValidation.DependencyInjectionExtensions`
- `Mapster`
- `Microsoft.AspNetCore.Mvc.Testing`
- `xUnit`
- `FluentAssertions`

### Fora deste corte

- banco de dados e migrations
- `Entity Framework Core`
- `ASP.NET Core Identity`
- autenticacao e autorizacao
- provedores externos

## Evidencias

- build da solution concluido com sucesso em `.NET 10`
- `dotnet test` concluido com `4` testes passando
- tasks desta historia atualizadas para refletir a implementacao real

## Tasks

- [[TASK-20260422142622 criar projeto base asp net core web api em net 10]]
- [[TASK-20260422142626 configurar openapi e pipeline inicial do backend]]
- [[TASK-20260422142630 definir estrutura minima de camadas do backend]]
- [[TASK-20260422142637 configurar settings e bibliotecas essenciais do backend]]
