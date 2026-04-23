---
tipo: us
id: US-20260423100004
status: backlog
feature: [[FEAT-20260423100000 adaptacao de curriculo por vaga com exportacao pdf]]
---

# User Story

## Historia

Como **usuario autenticado**, quero **receber o PDF final da versao adaptada usando o template escolhido** para **sair do fluxo com um documento pronto para candidatura sem divergencia entre contrato de conteudo e renderer**.

## Criterios de aceite

- [ ] `templateId` inexistente ou nao suportado falha antes da renderizacao.
- [ ] O renderer oficial do template recebe apenas o JSON canonico validado.
- [ ] O HTML final e gerado de forma deterministica fora da IA.
- [ ] A conversao HTML para PDF retorna o arquivo final apenas quando a geracao tiver status valido.

## Tasks

- [[TASK-20260423100010 compor a geracao adaptada com o catalogo oficial de templates]]
- [[TASK-20260423100011 renderizar html deterministico a partir do contrato canonico validado]]
- [[TASK-20260423100012 converter html renderizado em pdf final retornavel ao usuario]]
