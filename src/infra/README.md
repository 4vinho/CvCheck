# Infra local

Suba o PostgreSQL local e o capturador de emails com:

```bash
docker compose up -d --build
```

Servicos expostos:
- PostgreSQL em `localhost:5432`
- SMTP do Mailpit em `localhost:1025`
- UI do Mailpit em `http://localhost:8025`

Arquivos:
- `Dockerfile`: imagem base do PostgreSQL local.
- `Mailpit.Dockerfile`: imagem base do Mailpit para captura de emails.
- `docker-compose.yml`: sobe o banco e o Mailpit com volume persistente e healthchecks.
- `.env.example`: variaveis esperadas pelo compose.

Antes de subir, copie `.env.example` para `.env` no mesmo diretorio.

Para o backend capturar os emails no ambiente de desenvolvimento, ele ja esta configurado para usar:
- `SmtpHost=localhost`
- `SmtpPort=1025`

Depois de subir a infra, abra `http://localhost:8025` para inspecionar as mensagens enviadas.
