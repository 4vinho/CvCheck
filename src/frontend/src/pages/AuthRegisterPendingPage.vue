<template>
  <section class="mx-auto grid max-w-4xl gap-6">
    <AuthPanel
      eyebrow="Confirmation pending"
      :title="panelTitle"
      :description="panelDescription"
      class="overflow-hidden"
    >
      <template #header>
        <div class="grid gap-4 rounded-[1.5rem] border border-emerald-200 bg-emerald-50/90 p-5">
          <div class="space-y-2">
            <p class="text-sm font-semibold uppercase tracking-[0.24em] text-emerald-700">
              Account status
            </p>
            <p class="text-2xl font-semibold tracking-tight text-emerald-950">
              Pending confirmation
            </p>
          </div>
          <p class="text-sm leading-6 text-emerald-900">
            This email will be used as the primary identifier for your account:
          </p>
          <div class="rounded-2xl border border-emerald-200 bg-white/90 px-4 py-3 text-lg font-semibold text-emerald-950">
            {{ resolvedEmail }}
          </div>
        </div>
      </template>

      <div class="grid gap-4 md:grid-cols-2">
        <div class="rounded-2xl border border-border/70 bg-background/70 p-4">
          <p class="text-sm font-semibold uppercase tracking-[0.24em] text-muted-foreground">
            What this means
          </p>
          <p class="mt-3 text-sm leading-6 text-muted-foreground">
            Until email confirmation is complete, full account access remains unavailable.
          </p>
        </div>

        <div class="rounded-2xl border border-dashed border-border bg-background/70 p-4">
          <p class="text-sm font-semibold uppercase tracking-[0.24em] text-muted-foreground">
            Next step
          </p>
          <p class="mt-3 text-sm leading-6 text-muted-foreground">
            Check your inbox, use the most recent code you received, and complete confirmation to unlock access.
          </p>
        </div>
      </div>

      <template #footer>
        <div class="flex flex-wrap gap-3">
          <Button variant="outline" size="lg" :disabled="isResendDisabled" @click="handleResend">
            {{ resendButtonLabel }}
          </Button>
          <RouterLink
            :to="{ name: 'auth-email-confirmation', query: { email: resolvedEmail } }"
            :class="buttonVariants({ size: 'lg' })"
          >
            Enter code
          </RouterLink>
          <RouterLink :to="{ name: 'auth-register' }" :class="buttonVariants({ size: 'lg' })">
            Change email
          </RouterLink>
          <RouterLink :to="{ name: 'auth-login', query: { email: resolvedEmail } }" :class="buttonVariants({ variant: 'ghost', size: 'lg' })">
            Back to sign in
          </RouterLink>
        </div>
      </template>
    </AuthPanel>
  </section>
</template>

<script setup lang="ts">
import { computed, onBeforeUnmount, ref, watch } from "vue";
import { RouterLink, useRoute } from "vue-router";
import AuthPanel from "@/components/shared/AuthPanel.vue";
import { useEmailConfirmationResendAvailabilityQuery } from "@/composables/auth/useEmailConfirmationResendAvailabilityQuery";
import { useResendEmailConfirmationMutation } from "@/composables/auth/useResendEmailConfirmationMutation";
import { Button, buttonVariants } from "@/components/ui/button";

const route = useRoute();
const resendMutation = useResendEmailConfirmationMutation();
const resendCooldown = ref(0);
let cooldownTimer: ReturnType<typeof setInterval> | undefined;

const resolvedEmail = computed(() => {
  const email = route.query.email;

  if (typeof email === "string" && email.trim()) {
    return email.trim();
  }

  return "email not provided";
});

const isFromLogin = computed(() => route.query.source === "login");
const availabilityQuery = useEmailConfirmationResendAvailabilityQuery(resolvedEmail.value === "email not provided" ? "" : resolvedEmail.value);

const panelTitle = computed(() =>
  isFromLogin.value
    ? "Access is waiting for email confirmation."
    : "Registration received. Email confirmation is still required.",
);

const panelDescription = computed(() =>
  isFromLogin.value
    ? "This account already exists, but it still needs email confirmation before full access is unlocked."
    : "We sent the next step to the provided address. The account will be unlocked after email confirmation.",
);

const isResendDisabled = computed(() =>
  resendMutation.isSubmitting.value
  || availabilityQuery.isLoading.value
  || resendCooldown.value > 0
  || resolvedEmail.value === "email not provided",
);

const resendButtonLabel = computed(() => {
  if (resendMutation.isSubmitting.value) {
    return "Resending...";
  }

  if (availabilityQuery.isLoading.value || availabilityQuery.isFetching.value) {
    return "Checking availability...";
  }

  if (resendCooldown.value > 0) {
    return `Resend in ${resendCooldown.value}s`;
  }

  return "Resend email";
});

function startCooldown() {
  if (cooldownTimer) {
    clearInterval(cooldownTimer);
  }

  if (resendCooldown.value <= 0) {
    cooldownTimer = undefined;
    return;
  }

  cooldownTimer = setInterval(() => {
    if (resendCooldown.value <= 1) {
      resendCooldown.value = 0;

      if (cooldownTimer) {
        clearInterval(cooldownTimer);
        cooldownTimer = undefined;
      }

      return;
    }

    resendCooldown.value -= 1;
  }, 1000);
}

watch(
  () => availabilityQuery.availability.value,
  (availability) => {
    if (!availability) {
      return;
    }

    resendCooldown.value = availability.canResend ? 0 : Math.max(availability.remainingSeconds, 0);
    startCooldown();
  },
  { immediate: true },
);

async function handleResend() {
  resendMutation.reset();

  try {
    await resendMutation.submit(resolvedEmail.value);
    await availabilityQuery.refetch();
  } catch {}
}

onBeforeUnmount(() => {
  if (cooldownTimer) {
    clearInterval(cooldownTimer);
  }
});
</script>
