---
tipo: regra
id: RN-20260422162544
---

# Regra de Negocio

## Descricao

O identificador principal da conta deve ser o `email`, tanto para autenticacao local quanto para conciliacao de contas externas, evitando duplicidade de identidade por outros campos.

## Impacta

- [[FEAT-20260422162514 autenticacao e conta do usuario]]
- [[US-20260422162522 cadastrar conta local com email e senha]]
- [[US-20260422162524 realizar login e logout no sistema]]
- [[US-20260422162526 recuperar e redefinir senha por codigo]]
- [[US-20260422162528 gerenciar dados basicos da conta]]
