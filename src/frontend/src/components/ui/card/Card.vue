<template>
  <div :class="cn('rounded-3xl border bg-card text-card-foreground', props.class)">
    <slot />
  </div>
</template>

<script setup lang="ts">
import type { HTMLAttributes } from "vue";
import { cn } from "@/lib/utils";

interface Props {
  class?: HTMLAttributes["class"];
}

const props = defineProps<Props>();
</script>

