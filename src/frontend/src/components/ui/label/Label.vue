<template>
  <label
    :for="props.for"
    :class="cn('text-sm font-medium leading-none text-foreground', props.class)"
  >
    <slot />
  </label>
</template>

<script setup lang="ts">
import type { HTMLAttributes, LabelHTMLAttributes } from "vue";
import { cn } from "@/lib/utils";

interface Props extends /* @vue-ignore */ LabelHTMLAttributes {
  class?: HTMLAttributes["class"];
}

const props = defineProps<Props>();
</script>
