---
tipo: us
id: US-20260422162527
status: backlog
feature: [[FEAT-20260422162514 autenticacao e conta do usuario]]
---

# User Story

## Historia

Como **visitante**, quero **entrar ou criar minha conta usando Google** para **reduzir friccao no acesso inicial e recorrente**.

## Criterios de aceite

- [ ] O sistema permite iniciar autenticacao com Google como unico login social desta feature.
- [ ] No primeiro acesso, a conta pode ser criada automaticamente a partir dos dados retornados pelo provedor.
- [ ] Quando o Google informar email validado, a conta resultante passa a ser considerada confirmada.

## Tasks

- [[TASK-20260422162627 configurar provedor google no identity]]
- [[TASK-20260422162629 permitir criacao da conta no primeiro acesso]]
- [[TASK-20260422162630 permitir login recorrente com google]]
- [[TASK-20260422162631 considerar conta confirmada quando o google ja trouxer email validado]]
