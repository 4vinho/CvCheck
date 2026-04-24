---
tipo: us
id: US-20260422142550
status: done
feature: [[FEAT-20260422142544 configuracao inicial da plataforma]]
---

# User Story

## Historia

Como **time tecnico**, quero **definir a arquitetura inicial, a organizacao do repositorio e as convencoes base do projeto** para **comecar a implementacao com uma fundacao consistente e rastreavel**.

## Criterios de aceite

- [x] Existe uma definicao clara da estrutura inicial do projeto e da separacao entre frontend e backend.
- [x] As convencoes de nomenclatura, versionamento e arquivos raiz foram registradas no backlog tecnico.
- [x] A US referencia as tasks necessarias para fechar a baseline arquitetural.

## Baseline aprovada

### Estrutura macro do repositorio

```text
.
|-- docs/
|-- src/
|   |-- backend/
|   `-- frontend/
|-- tests/
|   `-- backend/
|-- .editorconfig
|-- .gitignore
`-- README.md
```

### Separacao entre as frentes

- frontend e backend evoluem desacoplados
- a comunicacao entre as frentes ocorre somente por HTTP API
- o contrato publico do backend deve ser exposto por OpenAPI
- frontend e backend podem ser publicados e versionados de forma independente
- nenhuma frente deve depender do processo de build da outra

### Baseline arquitetural do backend

- estilo de API: `ASP.NET Core Web API` com `Controllers`
- stack oficial: `.NET 10`
- solucao inicial orientada pelas camadas `Api`, `Application`, `Domain` e `Infrastructure`
- `Api` referencia `Application`
- `Infrastructure` concentra adaptadores e dependencias externas
- `Domain` permanece isolado de framework, transporte e persistencia

### Convencoes tecnicas

- nomes de pastas, projetos e namespaces devem usar ingles tecnico e curto, alinhado ao produto `CvCheck`
- IDs de features, user stories, tasks e regras continuam no formato atual dos artefatos em `docs/`
- a numeracao de versao do produto fica em `0.x` ate a estabilizacao do MVP
- os arquivos raiz obrigatorios desta baseline sao `README.md`, `.gitignore`, `.editorconfig` e `docs/`

## Fora desta US

- scaffold funcional da solution `.NET 10`
- projetos compilaveis do backend
- banco de dados, migrations e persistencia
- `ASP.NET Core Identity`, autenticacao e autorizacao
- provedores externos e configuracoes de infraestrutura em runtime

## Dependencias e handoff

Esta US prepara a fundacao obrigatoria para [[US-20260422142554 preparar backend base em net 10]].

A proxima US deve implementar apenas a fundacao tecnica da API dentro da baseline aprovada aqui:

- criar a solution em `src/backend`
- configurar pipeline inicial da API e OpenAPI
- materializar os projetos `Api`, `Application`, `Domain` e `Infrastructure`
- manter fora do escopo banco, Identity e regras de negocio

## Evidencias

- `README.md` atualizado com a baseline tecnica do monorepo
- `.gitignore` e `.editorconfig` adicionados como arquivos raiz obrigatorios
- pastas `src/backend`, `src/frontend` e `tests/backend` criadas para reservar a organizacao inicial

## Tasks

- [[TASK-20260422142609 definir modelo inicial de solucao e organizacao de pastas]]
- [[TASK-20260422142612 definir estrategia de separacao entre front e back]]
- [[TASK-20260422142618 definir convencao de nomenclatura versionamento e arquivos raiz]]
