<template>
  <header class="border-b border-border/60 bg-background/75 backdrop-blur">
    <div class="container flex min-h-16 items-center justify-between gap-4">
      <RouterLink to="/auth/register" class="space-y-1">
        <p class="text-xs font-semibold uppercase tracking-[0.28em] text-muted-foreground">
          CvCheck
        </p>
        <h1 class="text-lg font-semibold text-foreground">
          Local account
        </h1>
      </RouterLink>

      <nav class="flex items-center gap-2 text-sm text-secondary-foreground">
        <RouterLink
          :to="{ name: 'auth-login' }"
          class="rounded-full border border-border/70 bg-secondary/70 px-3 py-1 transition-colors hover:bg-accent hover:text-accent-foreground"
        >
          Login
        </RouterLink>
        <RouterLink
          :to="{ name: 'auth-register' }"
          class="rounded-full border border-border/70 bg-primary/70 px-3 py-1 transition-colors hover:bg-accent hover:text-accent-foreground"
        >
          Register
        </RouterLink>
      </nav>
    </div>
  </header>
</template>

<script setup lang="ts">
import { RouterLink } from "vue-router";
</script>
