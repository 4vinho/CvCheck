<template>
  <div
    :class="
      cn(
        'shrink-0 bg-border',
        orientation === 'horizontal' ? 'h-px w-full' : 'h-full w-px',
        props.class,
      )
    "
    :aria-orientation="orientation"
    role="separator"
  />
</template>

<script setup lang="ts">
import type { HTMLAttributes } from "vue";
import { cn } from "@/lib/utils";

interface Props {
  orientation?: "horizontal" | "vertical";
  class?: HTMLAttributes["class"];
}

const props = withDefaults(defineProps<Props>(), {
  orientation: "horizontal",
});

const { orientation } = props;
</script>

