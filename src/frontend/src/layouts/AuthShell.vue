<template>
  <div class="min-h-screen bg-background text-foreground">
    <div class="absolute inset-x-0 top-0 -z-10 h-[34rem] bg-[radial-gradient(circle_at_top,rgba(14,116,144,0.16),transparent_52%)]" />
    <div class="absolute inset-y-0 right-0 -z-10 w-[28rem] bg-[radial-gradient(circle_at_center,rgba(34,197,94,0.08),transparent_62%)]" />
    <AuthHeader />

    <main class="pb-16 pt-8 md:pt-12">
      <div class="container">
        <RouterView />
      </div>
    </main>
  </div>
</template>

<script setup lang="ts">
import { RouterView } from "vue-router";
import AuthHeader from "@/components/shared/AuthHeader.vue";
</script>
