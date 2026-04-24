---
tipo: task
id: TASK-20260422142637
status: done
feature: [[FEAT-20260422142544 configuracao inicial da plataforma]]
us: [[US-20260422142554 preparar backend base em net 10]]
---

# Task

## Descricao

Definir os arquivos de configuracao e as bibliotecas essenciais do backend, mantendo uma baseline pragmatica para API, configuracoes e evolucao futura.

## Checklist

- [x] Definir arquivos de settings e ambientes do backend
- [x] Listar bibliotecas essenciais para a baseline
- [x] Validar que o escopo nao extrapola o necessario
- [x] Atualizar a task com as escolhas aprovadas

## Settings definidos

- `appsettings.json`
- `appsettings.Development.json`
- secao `ApiMetadata` para nome, versao e descricao institucional da API

## Bibliotecas essenciais aprovadas

- `Microsoft.AspNetCore.OpenApi`
- `Swashbuckle.AspNetCore`
- `MediatR`
- `FluentValidation.DependencyInjectionExtensions`
- `Mapster`
- `Microsoft.AspNetCore.Mvc.Testing`
- `xUnit`
- `FluentAssertions`

## Convencoes complementares

- `Directory.Build.props` com `Nullable`, `ImplicitUsings` e `TreatWarningsAsErrors`
- `Directory.Packages.props` para gestao central das versoes de pacotes
- sem inclusao de `Entity Framework`, `Identity`, `Serilog` ou telemetria completa neste corte
