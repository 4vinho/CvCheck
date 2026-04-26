<template>
  <section class="grid gap-10 px-6 py-8 md:px-10 md:py-10">
    <div class="grid gap-6 lg:grid-cols-[1.5fr_1fr]">
      <div class="space-y-6">
        <div class="space-y-4">
          <div class="space-y-3">
            <h2 class="max-w-2xl text-4xl font-semibold tracking-tight text-balance text-foreground">
              Um espaco pronto para receber a experiencia autenticada.
            </h2>
            <p class="max-w-2xl text-base leading-7 text-muted-foreground">
              Esta area concentra a navegacao principal da aplicacao e prepara
              o caminho para os proximos fluxos de conta, acesso e uso do
              produto.
            </p>
          </div>
        </div>

        <div class="flex flex-wrap gap-3">
          <Button>Acessar minha conta</Button>
          <Button variant="outline">Explorar a plataforma</Button>
        </div>
      </div>

      <Card class="border-border/80 bg-background/70">
        <CardHeader>
          <CardTitle>Navegacao principal</CardTitle>
          <CardDescription>
            Alguns caminhos principais ja estao organizados para a evolucao do produto.
          </CardDescription>
        </CardHeader>
        <CardContent class="space-y-3 text-sm text-muted-foreground">
          <div class="flex items-center justify-between rounded-xl border border-border/70 px-4 py-3">
            <span>/</span>
            <span>redireciona para o cadastro</span>
          </div>
          <div class="flex items-center justify-between rounded-xl border border-border/70 px-4 py-3">
            <span>/app</span>
            <span>area principal da aplicacao</span>
          </div>
          <div class="flex items-center justify-between rounded-xl border border-border/70 px-4 py-3">
            <span>/*</span>
            <span>pagina nao encontrada</span>
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
    title: "Experiencia",
    description: "Fluxos organizados com clareza",
    body: "A estrutura separa area publica, area autenticada e componentes reutilizaveis para manter a navegacao consistente.",
  },
  {
    title: "Componentes",
    description: "Base visual reaproveitavel",
    body: "Cards, botoes e elementos de formulario seguem a mesma linguagem visual para acelerar a evolucao das proximas telas.",
  },
  {
    title: "Evolucao",
    description: "Preparado para os proximos passos",
    body: "A navegacao atual abre espaco para login, confirmacao de email e demais recursos da conta sem reestruturar a interface.",
  },
];
</script>
