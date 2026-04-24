---
tipo: us
id: US-20260422142558
status: done
feature: [[FEAT-20260422142544 configuracao inicial da plataforma]]
---

# User Story

## Historia

Como **time tecnico**, quero **subir o frontend base em Vue 3 com Vite, Tailwind CSS e shadcn-vue** para **acelerar a construcao das interfaces sobre uma base moderna e padronizada**.

## Criterios de aceite

- [x] O projeto frontend base em Vue 3 esta definido com TypeScript e ferramental inicial.
- [x] Tailwind CSS e shadcn-vue fazem parte da baseline registrada para a camada visual.
- [x] A estrutura inicial de layout e componentes foi desdobrada em tasks objetivas.

## Tasks

- [[TASK-20260422142645 criar projeto base vue 3 com vite e typescript]]
- [[TASK-20260422142650 configurar tailwind css no frontend]]
- [[TASK-20260422142654 configurar shadcn vue no frontend]]
- [[TASK-20260422142658 definir estrutura base de layout e componentes do frontend]]

## Implementacao adotada

- O frontend foi criado em `frontend/` como SPA com `Vue 3 + Vite + TypeScript`.
- A baseline inclui `vue-router` com redirecionamento de `/` para `/app`, shell base em `src/layouts/AppShell.vue` e rota catch-all para 404.
- O alias `@/` foi configurado de forma consistente em `vite.config.ts` e `tsconfig.app.json`.
- O Tailwind CSS foi centralizado em `src/styles/globals.css`, com tokens CSS iniciais e layout neutro para a shell tecnica.
- A integracao com `shadcn-vue` foi registrada em `frontend/components.json`, usando `src/components/ui` como destino oficial.
- Foram adicionados os componentes base equivalentes ao baseline esperado para `Button`, `Card` e `Separator`, sem expandir o catalogo.

## Fora desta US

- Autenticacao
- Consumo de API
- Estado global
- i18n
- Testes E2E
- Qualquer acoplamento com backend alem da preparacao estrutural
