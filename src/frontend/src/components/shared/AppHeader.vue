<template>
  <header class="border-b border-border/70 bg-background/85 backdrop-blur">
    <div class="container flex min-h-16 items-center justify-between gap-4">
      <div class="space-y-1">
        <p class="text-xs font-semibold uppercase tracking-[0.28em] text-muted-foreground">
          CvCheck
        </p>
        <h1 class="text-lg font-semibold text-foreground">
          Main area
        </h1>
      </div>

      <Button variant="outline" :disabled="isSubmitting" @click="handleLogout">
        {{ isSubmitting ? "Signing out..." : "Sign out" }}
      </Button>
    </div>
  </header>
</template>

<script setup lang="ts">
import { useRouter } from "vue-router";
import { useLogoutMutation } from "@/composables/auth/useLogoutMutation";
import { Button } from "@/components/ui/button";

const router = useRouter();
const logoutMutation = useLogoutMutation();
const isSubmitting = logoutMutation.isSubmitting;

async function handleLogout() {
  logoutMutation.reset();

  try {
    await logoutMutation.submit();
    router.push({ name: "auth-login" });
  } catch {}
}
</script>
