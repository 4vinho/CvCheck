<template>
  <section class="grid gap-10 px-6 py-8 md:px-10 md:py-10">
    <div class="grid gap-6 lg:grid-cols-[1.5fr_1fr]">
      <div class="space-y-6">
        <div class="space-y-4">
          <div class="space-y-3">
            <h2 class="max-w-2xl text-4xl font-semibold tracking-tight text-balance text-foreground">
              A space ready to host the authenticated experience.
            </h2>
            <p class="max-w-2xl text-base leading-7 text-muted-foreground">
              This area brings together the application's main navigation and prepares
              the path for the next account, access, and product usage flows.
            </p>
          </div>
        </div>

        <div class="flex flex-wrap gap-3">
          <Button>Access my account</Button>
          <Button variant="outline">Explore the platform</Button>
        </div>
      </div>

      <Card class="border-border/80 bg-background/70">
        <CardHeader>
          <CardTitle>Main navigation</CardTitle>
          <CardDescription>
            Some key paths are already organized for the product's next steps.
          </CardDescription>
        </CardHeader>
        <CardContent class="space-y-3 text-sm text-muted-foreground">
          <div class="flex items-center justify-between rounded-xl border border-border/70 px-4 py-3">
            <span>/</span>
            <span>redirects to sign up</span>
          </div>
          <div class="flex items-center justify-between rounded-xl border border-border/70 px-4 py-3">
            <span>/app</span>
            <span>main application area</span>
          </div>
          <div class="flex items-center justify-between rounded-xl border border-border/70 px-4 py-3">
            <span>/*</span>
            <span>page not found</span>
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
    title: "Experience",
    description: "Flows organized with clarity",
    body: "The structure separates the public area, authenticated area, and reusable components to keep navigation consistent.",
  },
  {
    title: "Components",
    description: "Reusable visual foundation",
    body: "Cards, buttons, and form elements share the same visual language to speed up the next screens.",
  },
  {
    title: "Evolution",
    description: "Ready for the next steps",
    body: "The current navigation leaves room for sign in, email confirmation, and the rest of the account features without restructuring the interface.",
  },
];
</script>
