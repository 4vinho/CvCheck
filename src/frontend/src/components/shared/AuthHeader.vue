<template>
  <header class="border-b border-border/60 bg-background/75 backdrop-blur">
    <div class="container flex min-h-16 items-center justify-between gap-4">
      <RouterLink to="/auth/cadastro" class="space-y-1">
        <p class="text-xs font-semibold uppercase tracking-[0.28em] text-muted-foreground">
          CvCheck
        </p>
        <h1 class="text-lg font-semibold text-foreground">
          Conta local
        </h1>
      </RouterLink>

      <nav class="flex items-center gap-2 text-sm text-muted-foreground">
        <span class="rounded-full border border-border/70 bg-background/70 px-3 py-1">
          Cadastro local
        </span>
        <span class="rounded-full border border-border/70 bg-background/70 px-3 py-1">
          Confirmacao por email
        </span>
      </nav>
    </div>
  </header>
</template>

<script setup lang="ts">
import { RouterLink } from "vue-router";
</script>
