<template>
  <RouterView />
  <AppToastViewport />
</template>

<script setup lang="ts">
import { RouterView } from "vue-router";
import AppToastViewport from "@/components/shared/AppToastViewport.vue";
</script>
