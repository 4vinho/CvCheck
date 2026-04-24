<template>
  <div class="min-h-screen bg-background text-foreground">
    <div class="absolute inset-x-0 top-0 -z-10 h-[28rem] bg-[radial-gradient(circle_at_top,rgba(14,116,144,0.18),transparent_58%)]" />
    <AppHeader />

    <main class="pb-16 pt-8">
      <div class="container">
        <div class="rounded-[2rem] border border-border/70 bg-card/80 shadow-shell backdrop-blur">
          <RouterView />
        </div>
      </div>
    </main>
  </div>
</template>

<script setup lang="ts">
import { RouterView } from "vue-router";
import AppHeader from "@/components/shared/AppHeader.vue";
</script>

