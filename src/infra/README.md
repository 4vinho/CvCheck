# Infra local

Suba o PostgreSQL local com:

```bash
docker compose up -d --build
```

Arquivos:
- `Dockerfile`: imagem base do PostgreSQL local.
- `docker-compose.yml`: sobe o banco com volume persistente e healthcheck.
- `.env.example`: variaveis esperadas pelo compose.

Antes de subir, copie `.env.example` para `.env` no mesmo diretorio.
