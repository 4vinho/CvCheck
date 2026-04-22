# Backlog

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
