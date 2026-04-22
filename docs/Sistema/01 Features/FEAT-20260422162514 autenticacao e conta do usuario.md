---
tipo: feature
id: FEAT-20260422162514
status: backlog
prioridade: alta
---

# Feature

## Objetivo

Estabelecer a primeira feature funcional de autenticacao e conta do produto, usando `ASP.NET Core Identity` como base para cadastro, login, confirmacao de email, recuperacao de senha, autenticacao com Google e manutencao basica da conta.

## Valor de negocio

Permitir acesso seguro ao sistema, criar a base oficial de identidade do usuario e viabilizar persistencia e personalizacao futura sem misturar fluxos de curriculo, perfil do candidato ou regras de negocio fora do escopo de conta.

## Regras de negocio

- [[RN-20260422162543 a autenticacao oficial deve usar asp net core identity]]
- [[RN-20260422162544 o identificador principal da conta deve ser o email]]
- [[RN-20260422162545 contas locais devem exigir confirmacao obrigatoria de email antes de uso pleno]]
- [[RN-20260422162547 a recuperacao de senha deve ocorrer por codigo enviado por email e nao por link]]
- [[RN-20260422162548 login e cadastro com google devem ser permitidos nesta feature]]
- [[RN-20260422162549 contas vindas do google podem ser consideradas confirmadas quando o email vier validado]]
- [[RN-20260422162550 a alteracao de email da conta deve exigir confirmacao do novo endereco]]
- [[RN-20260422162551 mfa fica explicitamente fora desta feature]]
- [[RN-20260422162553 outros logins sociais alem de google ficam fora desta feature]]
- [[RN-20260422162554 a feature cobre apenas autenticacao e conta sem incluir perfil curricular do usuario]]
