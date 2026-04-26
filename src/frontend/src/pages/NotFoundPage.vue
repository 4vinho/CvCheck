<template>
  <main class="container flex min-h-[70vh] items-center justify-center py-12">
    <Card class="w-full max-w-xl border-border/80 bg-card/90 text-center shadow-shell">
      <CardHeader class="space-y-4">
        <p class="text-sm font-semibold uppercase tracking-[0.3em] text-muted-foreground">
          404
        </p>
        <CardTitle class="text-3xl">Page not found</CardTitle>
        <CardDescription class="text-base">
          The requested address does not match an available page.
        </CardDescription>
      </CardHeader>
      <CardContent class="flex justify-center">
        <RouterLink to="/app">
          <Button as="span">Back to /app</Button>
        </RouterLink>
      </CardContent>
    </Card>
  </main>
</template>

<script setup lang="ts">
import { RouterLink } from "vue-router";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
</script>
