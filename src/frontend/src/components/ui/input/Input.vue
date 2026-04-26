<template>
  <input
    v-bind="attrs"
    :class="cn(
      'flex h-11 w-full rounded-2xl border border-input bg-background/90 px-4 py-2 text-sm text-foreground shadow-sm transition-colors file:border-0 file:bg-transparent file:text-sm file:font-medium placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:cursor-not-allowed disabled:opacity-50 ring-offset-background',
      props.class,
    )"
  >
</template>

<script setup lang="ts">
import type { HTMLAttributes } from "vue";
import { useAttrs } from "vue";
import { cn } from "@/lib/utils";

interface Props {
  class?: HTMLAttributes["class"];
}

const props = defineProps<Props>();
const attrs = useAttrs();
</script>
