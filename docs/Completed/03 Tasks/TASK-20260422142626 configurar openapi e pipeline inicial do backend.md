---
tipo: task
id: TASK-20260422142626
status: done
feature: [[FEAT-20260422142544 configuracao inicial da plataforma]]
us: [[US-20260422142554 preparar backend base em net 10]]
---

# Task

## Descricao

Definir a configuracao inicial da pipeline do backend, incluindo middleware essencial, exposicao de OpenAPI e base de observabilidade minima para desenvolvimento local.

## Checklist

- [x] Delimitar os middlewares iniciais da API
- [x] Incluir OpenAPI como baseline de contrato
- [x] Validar a experiencia de uso em ambiente local
- [x] Atualizar a documentacao tecnica associada

## Pipeline adotada

- `AddControllers()`
- `AddProblemDetails()`
- `AddExceptionHandler<GlobalExceptionHandler>()`
- `AddHealthChecks()`
- `AddOpenApi("v1")`
- `AddSwaggerGen()`
- `UseExceptionHandler()`
- `UseHttpsRedirection()`
- `MapControllers()`
- `MapHealthChecks("/health")`

## Resultado validado

- contrato OpenAPI exposto pela API
- Swagger UI habilitado apenas em desenvolvimento na rota `docs`
- endpoint `GET /health` validado por teste de integracao
- endpoint `GET /api/platform` validado por teste de integracao
- pipeline centralizada dentro do projeto `CvCheck.Backend`
