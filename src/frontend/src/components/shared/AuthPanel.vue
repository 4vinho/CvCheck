<template>
  <Card
    :class="cn(
      'border-border/80 bg-card/90 shadow-shell backdrop-blur',
      props.class,
    )"
  >
    <CardHeader class="space-y-4">
      <div class="space-y-3">
        <p
          v-if="eyebrow"
          class="text-sm font-medium uppercase tracking-[0.3em] text-muted-foreground"
        >
          {{ eyebrow }}
        </p>
        <div class="space-y-2">
          <CardTitle class="text-3xl tracking-tight text-balance">
            {{ title }}
          </CardTitle>
          <CardDescription class="text-base leading-7">
            {{ description }}
          </CardDescription>
        </div>
      </div>

      <slot name="header" />
    </CardHeader>

    <CardContent :class="cn('space-y-6', contentClass)">
      <slot />
    </CardContent>

    <CardFooter v-if="$slots.footer" :class="cn('flex-col items-stretch gap-3', footerClass)">
      <slot name="footer" />
    </CardFooter>
  </Card>
</template>

<script setup lang="ts">
import type { HTMLAttributes } from "vue";
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "@/components/ui/card";
import { cn } from "@/lib/utils";

interface Props {
  eyebrow?: string;
  title: string;
  description: string;
  class?: HTMLAttributes["class"];
  contentClass?: HTMLAttributes["class"];
  footerClass?: HTMLAttributes["class"];
}

const props = defineProps<Props>();
</script>
