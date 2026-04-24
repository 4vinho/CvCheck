---
tipo: task
id: TASK-20260422142714
status: done
feature: [[FEAT-20260422142544 configuracao inicial da plataforma]]
us: [[US-20260422142601 definir padroes compartilhados e dev experience]]
---

# Task

## Descricao

Definir a estrategia inicial de variaveis de ambiente e URLs por ambiente para suportar execucao local e evolucao controlada da configuracao.

## Checklist

- [x] Delimitar como frontend e backend consumirao configuracoes
- [x] Definir URLs e ambientes considerados na baseline
- [x] Validar simplicidade e clareza para setup local
- [x] Registrar a estrategia escolhida

## Implementacao

- backend usa `appsettings.json` e `appsettings.Development.json` para configuracao por ambiente
- frontend segue configuracao do Vite e scripts locais para `dev`, `build` e `preview`
- URLs locais da API permanecem registradas em `launchSettings.json`, mantendo setup previsivel para desenvolvimento
