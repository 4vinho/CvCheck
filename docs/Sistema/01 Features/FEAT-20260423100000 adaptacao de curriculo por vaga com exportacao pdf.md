---
tipo: feature
id: FEAT-20260423100000
status: backlog
prioridade: alta
---

# Feature

## Objetivo

Permitir que o usuario autenticado gere uma nova versao adaptada do curriculo a partir do curriculo base, do texto livre da vaga e de um `templateId`, com persistencia rastreavel do payload canonico validado e retorno direto do PDF final.

## Valor de negocio

Entregar o fluxo central do CvCheck em um recorte pragmatico de MVP, conectando perfil base, adaptacao orientada por IA e exportacao deterministica sem permitir invencao de fatos nem dependencia de contratos soltos de template.

## Regras de negocio

- [[RN-20260423100014 a adaptacao por vaga deve usar o curriculo principal ou explicitamente selecionado do usuario autenticado]]
- [[RN-20260423100015 a vaga entra como texto livre no mvp da adaptacao]]
- [[RN-20260423100016 a ia nao pode inventar fatos fora do perfil base do usuario]]
- [[RN-20260423100017 cada geracao deve criar uma nova versao adaptada rastreavel]]
- [[RN-20260423100018 a adaptacao pode reorganizar resumir e destacar apenas conteudo real do perfil base]]
- [[RN-20260423100019 a resposta da ia deve ser validada contra o schema canonico antes de qualquer persistencia ou renderizacao]]
- [[RN-20260423100020 falha de schema da ia deve encerrar a operacao sem fallback silencioso nem pdf parcial]]
- [[RN-20260423100021 o renderer de template deve consumir apenas o payload canonico oficial validado]]
- [[RN-20260423100022 o primeiro corte da feature retorna pdf direto sem fluxo de preview ou revisao manual]]
