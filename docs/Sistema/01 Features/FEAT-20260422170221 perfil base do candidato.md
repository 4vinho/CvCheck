---
tipo: feature
id: FEAT-20260422170221
status: backlog
prioridade: alta
---

# Feature

## Objetivo

Permitir que o usuario crie e mantenha um ou mais curriculos base estruturados dentro da plataforma, organizando dados pessoais, resumo profissional, experiencias, formacao academica, habilidades e idiomas em um fluxo guiado por etapas.

## Valor de negocio

Estabelecer a materia-prima oficial do produto para futuras adaptacoes por vaga, com dados reutilizaveis, editaveis e fieis a trajetoria do candidato sem misturar autenticacao com perfil curricular.

## Regras de negocio

- [[RN-20260422170350 perfil curricular deve ser uma feature separada da conta do usuario]]
- [[RN-20260422170351 um usuario pode manter multiplos curriculos]]
- [[RN-20260422170352 cada curriculo pertence a um unico usuario]]
- [[RN-20260422170353 foto e anexos ficam fora do mvp de perfil curricular]]
- [[RN-20260422170355 o fluxo oficial de preenchimento do perfil deve ser em wizard por etapas]]
- [[RN-20260422170356 experiencias formacoes habilidades e idiomas aceitam multiplos itens]]
- [[RN-20260422170357 o sistema nao deve inventar informacoes nao fornecidas pelo usuario]]
- [[RN-20260422170358 o perfil base deve ser reutilizavel para futuras adaptacoes por vaga]]
- [[RN-20260423100014 a adaptacao por vaga deve usar o curriculo principal ou explicitamente selecionado do usuario autenticado]]
- [[RN-20260423100016 a ia nao pode inventar fatos fora do perfil base do usuario]]
- [[RN-20260423100018 a adaptacao pode reorganizar resumir e destacar apenas conteudo real do perfil base]]
