<template>
  <Card :class="cn('border-border/80 bg-background/75', props.class)">
    <CardHeader class="space-y-3">
      <div class="inline-flex w-fit items-center rounded-full border border-border/70 bg-accent/60 px-3 py-1 text-xs font-semibold uppercase tracking-[0.24em] text-accent-foreground">
        {{ badge }}
      </div>
      <div class="space-y-2">
        <CardTitle class="text-2xl tracking-tight">
          {{ title }}
        </CardTitle>
        <CardDescription class="text-sm leading-6">
          {{ description }}
        </CardDescription>
      </div>
    </CardHeader>

    <CardContent class="space-y-4">
      <slot />
    </CardContent>
  </Card>
</template>

<script setup lang="ts">
import type { HTMLAttributes } from "vue";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { cn } from "@/lib/utils";

interface Props {
  badge: string;
  title: string;
  description: string;
  class?: HTMLAttributes["class"];
}

const props = defineProps<Props>();
</script>
