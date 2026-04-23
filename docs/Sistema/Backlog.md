# Backlog

Este backlog traduz o `README.md` em uma sequencia real de evolucao do produto. A ideia e servir como base para derivar features, user stories e tasks sem perder o foco do CvCheck: adaptar curriculos para vagas reais com mais clareza, mais confianca e menos esforco manual.

## Visao de produto

O CvCheck deve ajudar o candidato a:

- manter um perfil base reutilizavel
- informar uma vaga alvo
- entender o que a vaga pede
- gerar uma adaptacao do curriculo sem inventar informacao
- revisar o que mudou e por que mudou
- exportar uma versao final pronta para uso

## Linha mestra de priorizacao

A ordem recomendada de desenvolvimento e:

1. fundacao tecnica e identidade do usuario
2. perfil base do candidato
3. entrada e leitura da vaga
4. adaptacao do curriculo
5. revisao e transparencia
6. exportacao
7. melhorias de personalizacao e historico

## Prioridade 1 - Fundacao e acesso

Estas entregas existem para permitir que o produto rode com base tecnica consistente e com identidade persistente do usuario.

### Blocos deste grupo

- configuracao inicial da plataforma
- autenticacao e conta do usuario

### Resultado esperado

Ao final deste grupo, o projeto deve ter ambiente base funcional, autenticacao segura e conta persistente para sustentar as proximas features.

## Prioridade 2 - Perfil base do candidato

Este e o primeiro bloco de valor direto para o usuario. Sem isso, nao existe materia-prima confiavel para adaptar curriculos.

### O que precisa existir

- cadastro de resumo profissional base
- cadastro e edicao de experiencias profissionais
- cadastro e edicao de formacao academica
- cadastro e edicao de habilidades e tecnologias
- visualizacao consolidada do perfil base

### Objetivo do grupo

Permitir que o usuario monte e mantenha um curriculo base estruturado, reutilizavel e fiel a propria trajetoria.

### Indicacoes para derivar features

- separar resumo, experiencias, formacao e habilidades em recortes que possam evoluir sem acoplamento desnecessario
- tratar CRUD basico antes de pensar em importacao, IA ou enriquecimento automatico
- preservar o principio de nao inventar informacoes que o usuario nao forneceu

## Prioridade 3 - Entrada e entendimento da vaga

Depois do perfil base, o produto precisa receber o contexto da vaga alvo e transformar texto bruto em insumo util para adaptacao.

### O que precisa existir

- campo para colar a descricao completa da vaga
- armazenamento da vaga informada pelo usuario
- extracao de palavras-chave relevantes
- identificacao de requisitos principais
- separacao entre requisitos explicitos e inferencias do sistema

### Objetivo do grupo

Transformar a vaga em um conjunto claro de sinais que oriente a adaptacao do curriculo.

### Indicacoes para derivar features

- separar captura da vaga da analise da vaga
- deixar claro o que veio literalmente da vaga e o que foi interpretado pelo sistema
- evitar depender de integracoes externas neste momento

## Prioridade 4 - Adaptacao do curriculo para a vaga

Este e o coracao do produto. A adaptacao deve ajudar o usuario a reposicionar o proprio conteudo para a vaga desejada sem distorcer a verdade.

### O que precisa existir

- geracao de resumo profissional adaptado ao cargo
- reescrita orientada das experiencias com foco na vaga
- destaque das habilidades mais relevantes para a oportunidade
- opcao de ajustar o tom da adaptacao, como simples, corporativo ou tecnico
- geracao de uma versao adaptada do curriculo baseada no perfil base e na vaga
- geracao sincronica de uma versao adaptada renderizavel com `templateId` e retorno direto do PDF final

### Objetivo do grupo

Produzir uma versao mais aderente ao contexto da vaga, mantendo fidelidade ao historico real do candidato.

### Indicacoes para derivar features

- separar adaptacao do resumo, experiencias e habilidades para facilitar validacao e evolucao
- tratar tom como capacidade adicional, nao como dependencia do primeiro fluxo
- explicitar nas regras que o sistema nao pode inventar experiencias, certificacoes, resultados ou tecnologias
- manter o primeiro corte orientado a JSON canonico validado e renderizacao deterministica, sem depender de preview manual

## Prioridade 5 - Revisao, transparencia e confianca

O diferencial do produto nao deve ser apenas gerar texto, mas ajudar o usuario a confiar no resultado.

### O que precisa existir

- comparacao entre curriculo base e curriculo adaptado
- destaque visual do que mudou
- explicacao do motivo de cada mudanca sugerida
- marcacao de trechos com baixa confianca ou inferencia
- alertas sobre lacunas relevantes em relacao a vaga
- score de compatibilidade com explicacao compreensivel

### Objetivo do grupo

Dar visibilidade sobre a adaptacao para que o usuario revise com criterio e mantenha controle sobre a versao final.

### Indicacoes para derivar features

- separar visualizacao de diff da camada de justificativa
- tratar score como apoio a decisao, nunca como verdade absoluta
- garantir que inferencias sejam sempre sinalizadas

## Prioridade 6 - Finalizacao e exportacao

Quando a adaptacao estiver pronta e revisada, o usuario precisa transformar isso em um artefato final usavel.

### O que precisa existir

- preview final do curriculo adaptado
- edicao manual antes da conclusao
- exportacao em PDF

### Objetivo do grupo

Permitir que o usuario refine os ultimos detalhes e saia da plataforma com um curriculo pronto para candidatura.

### Indicacoes para derivar features

- priorizar um fluxo simples de preview e exportacao antes de pensar em multiplos templates
- manter a edicao manual como etapa oficial do produto, nao como excecao

## Prioridade 7 - Evolucoes de valor adicional

Estas entregas podem aumentar valor e retencao, mas nao devem bloquear o MVP.

### O que pode entrar depois

- historico de curriculos adaptados por vaga
- armazenamento de vagas analisadas
- geracao de carta de apresentacao
- importacao de curriculo por arquivo
- mais de um modelo visual de curriculo
- recomendacoes por area, senioridade ou tipo de vaga

### Objetivo do grupo

Expandir conveniencia, personalizacao e reutilizacao depois que o fluxo principal estiver estavel.

## Regras de recorte para novas features

- cada feature deve resolver um bloco coeso do fluxo do produto
- auth/conta nao deve ser misturada com perfil curricular ou adaptacao de curriculo
- perfil base deve vir antes de IA de adaptacao
- leitura da vaga deve vir antes de score, justificativas e alertas avancados
- exportacao nao deve depender de features futuras de personalizacao visual
- tudo que envolver inferencia precisa deixar claro quando o sistema esta sugerindo e quando o usuario confirmou

## Escopo explicito de MVP

Com base no `README`, o MVP real do produto deve cobrir:

- conta do usuario
- perfil base com resumo, experiencias, formacao e habilidades
- entrada manual da vaga por texto
- extracao de sinais principais da vaga
- adaptacao de resumo e experiencias
- destaque de habilidades relevantes
- preview final
- edicao manual
- exportacao em PDF

## Fora do MVP por enquanto

- integracoes externas avancadas
- ingestao complexa de arquivos
- multiplos templates altamente customizaveis
- automacao completa de candidatura
- funcionalidades amplas de planejamento de carreira
- qualquer recurso que incentive exagero ou invencao de experiencia

## Foco atual no vault

- `configuracao inicial da plataforma` prepara a base tecnica
- `autenticacao e conta do usuario` prepara a base de identidade
- os proximos blocos naturais sao `perfil base do candidato`, `templates de curriculo` e `adaptacao de curriculo por vaga com exportacao pdf`

## Features

```dataview
TABLE id AS "ID", status AS "Status", prioridade AS "Prioridade"
FROM "Sistema/01 Features"
WHERE tipo = "feature"
SORT id ASC
```

## User Stories

```dataview
TABLE id AS "ID", status AS "Status", feature AS "Feature"
FROM "Sistema/02 User Stories"
WHERE tipo = "us"
SORT id ASC
```

## Tasks

```dataview
TABLE id AS "ID", status AS "Status", feature AS "Feature", us AS "User Story"
FROM "Sistema/03 Tasks"
WHERE tipo = "task"
SORT id ASC
```
