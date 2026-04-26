<template>
  <div
    role="alert"
    :class="cn(alertVariants({ variant }), props.class)"
  >
    <slot />
  </div>
</template>

<script setup lang="ts">
import type { HTMLAttributes } from "vue";
import { cn } from "@/lib/utils";
import { alertVariants, type AlertVariants } from "./alert";

interface Props {
  class?: HTMLAttributes["class"];
  variant?: AlertVariants["variant"];
}

const props = defineProps<Props>();
</script>
