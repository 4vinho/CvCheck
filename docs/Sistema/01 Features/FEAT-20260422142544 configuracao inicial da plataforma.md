---
tipo: feature
id: FEAT-20260422142544
status: done
prioridade: alta
---

# Feature

## Objetivo

Estabelecer a base tecnica do projeto para iniciar o desenvolvimento com backend em .NET 10 e frontend em Vue 3, definindo stack inicial, organizacao das frentes e baseline de configuracao.

## Valor de negocio

Reduzir retrabalho de setup, padronizar a stack desde o inicio, acelerar o onboarding tecnico e diminuir o risco de decisoes inconsistentes entre frontend e backend.

## Escopo tecnico aprovado

- repositorio em formato `monorepo`
- separacao fisica inicial em `src/backend`, `src/frontend` e `tests/backend`
- backend oficial em `.NET 10`
- frontend oficial em `Vue 3`
- integracao entre as frentes exclusivamente por HTTP API com contrato OpenAPI
- baseline pragmatica sem CI/CD completo neste primeiro corte

## Fora desta feature

- automacao completa de pipeline e deploy
- regras de negocio do dominio
- implementacao funcional de banco de dados
- implementacao funcional de autenticacao e provedores externos

## Entrega consolidada

- baseline documental e estrutural do repositorio consolidada
- backend base em `.NET 10` materializado e depois simplificado para projeto unico `CvCheck.Backend`
- frontend base em `Vue 3` com `Vite`, `Tailwind CSS` e `shadcn-vue` consolidado
- padroes compartilhados de qualidade, setup local e integracao entre as frentes registrados no vault

## Regras de negocio

- [[RN-20260422142730 a stack inicial oficial do backend deve ser net 10]]
- [[RN-20260422142734 a stack inicial oficial do frontend deve ser vue 3]]
- [[RN-20260422142739 o frontend deve usar tailwind css e shadcn vue como base visual]]
- [[RN-20260422142744 o setup inicial deve priorizar baseline pragmatica sem incluir ci cd completo]]
- [[RN-20260422142748 as escolhas de libs nesta feature devem cobrir apenas o essencial]]
- [[RN-20260422142752 front e back devem nascer desacoplados com contrato de api claro]]
- [[RN-20260422142757 o vault deve manter rastreabilidade por wikilinks entre os artefatos]]
