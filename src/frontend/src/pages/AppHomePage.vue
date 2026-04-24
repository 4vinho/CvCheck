<template>
  <section class="grid gap-10 px-6 py-8 md:px-10 md:py-10">
    <div class="grid gap-6 lg:grid-cols-[1.5fr_1fr]">
      <div class="space-y-6">
        <div class="space-y-4">
          <p class="text-sm font-medium uppercase tracking-[0.3em] text-muted-foreground">
            US-20260422142558
          </p>
          <div class="space-y-3">
            <h2 class="max-w-2xl text-4xl font-semibold tracking-tight text-balance text-foreground">
              Base pronta para evoluir a SPA sem acoplar cedo demais ao backend.
            </h2>
            <p class="max-w-2xl text-base leading-7 text-muted-foreground">
              Esta tela existe para validar o shell inicial, o roteamento e a
              baseline visual com Tailwind CSS e componentes derivados do
              shadcn-vue.
            </p>
          </div>
        </div>

        <div class="flex flex-wrap gap-3">
          <Button>Proxima US</Button>
          <Button variant="outline">Validar shell</Button>
        </div>
      </div>

      <Card class="border-border/80 bg-background/70">
        <CardHeader>
          <CardTitle>Rotas iniciais</CardTitle>
          <CardDescription>
            A navegacao minima ja existe para evitar refatoracao imediata.
          </CardDescription>
        </CardHeader>
        <CardContent class="space-y-3 text-sm text-muted-foreground">
          <div class="flex items-center justify-between rounded-xl border border-border/70 px-4 py-3">
            <span>/</span>
            <span>redireciona para /app</span>
          </div>
          <div class="flex items-center justify-between rounded-xl border border-border/70 px-4 py-3">
            <span>/app</span>
            <span>shell base da aplicacao</span>
          </div>
          <div class="flex items-center justify-between rounded-xl border border-border/70 px-4 py-3">
            <span>/*</span>
            <span>pagina 404 simples</span>
          </div>
        </CardContent>
      </Card>
    </div>

    <Separator />

    <div class="grid gap-4 md:grid-cols-3">
      <Card
        v-for="item in baselineItems"
        :key="item.title"
        class="h-full border-border/80 bg-background/70"
      >
        <CardHeader>
          <CardTitle>{{ item.title }}</CardTitle>
          <CardDescription>{{ item.description }}</CardDescription>
        </CardHeader>
        <CardContent class="text-sm leading-6 text-muted-foreground">
          {{ item.body }}
        </CardContent>
      </Card>
    </div>
  </section>
</template>

<script setup lang="ts">
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Separator } from "@/components/ui/separator";

const baselineItems = [
  {
    title: "Estrutura",
    description: "Organizacao inicial previsivel",
    body: "src/app, src/layouts, src/pages, src/components/ui, src/components/shared, src/styles e src/lib.",
  },
  {
    title: "Aliases",
    description: "Imports curtos e coerentes",
    body: "O alias @/ aponta para src e funciona tanto em arquivos .ts e .vue via Vite e TypeScript.",
  },
  {
    title: "Fora do escopo",
    description: "Sem acoplamento prematuro",
    body: "Autenticacao, cliente HTTP, estado global, i18n, testes E2E e qualquer integracao com API ficam para USs seguintes.",
  },
];
</script>
