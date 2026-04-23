---
tipo: us
id: US-20260423100002
status: backlog
feature: [[FEAT-20260423100000 adaptacao de curriculo por vaga com exportacao pdf]]
---

# User Story

## Historia

Como **usuario autenticado**, quero **que a IA transforme meu perfil base e a vaga em um JSON canonico renderizavel** para **obter uma adaptacao consistente que possa ser validada e renderizada sem depender de HTML gerado pela IA**.

## Criterios de aceite

- [ ] A IA recebe apenas o perfil base selecionado e o texto da vaga como insumos oficiais da adaptacao.
- [ ] A saida esperada da IA segue um contrato canonico unico reutilizavel pelos templates.
- [ ] JSON fora do schema canonico encerra o fluxo com erro claro.
- [ ] O sistema impede que a resposta bruta da IA siga para o renderer sem validacao.

## Tasks

- [[TASK-20260423100007 definir prompt e contrato do json canonico da versao adaptada]]
- [[TASK-20260423100008 validar a resposta da ia contra o schema canonico oficial]]
