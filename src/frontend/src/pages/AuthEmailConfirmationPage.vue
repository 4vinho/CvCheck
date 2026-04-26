<template>
  <section class="grid gap-6 lg:grid-cols-[1.15fr_0.85fr]">
    <AuthPanel
      eyebrow="Email confirmation"
      title="Confirm the code that was sent to unlock your account."
      description="Use your primary account email and the 6-character code sent by email."
    >
      <template #header>
        <div class="grid gap-3 rounded-[1.5rem] border border-cyan-200 bg-cyan-50/90 p-5 text-sm leading-6 text-cyan-950">
          <p class="font-semibold uppercase tracking-[0.24em] text-cyan-700">
            Before you continue
          </p>
          <p>
            Confirmation unlocks full access to your local account and invalidates older codes that were replaced by a newer resend.
          </p>
        </div>
      </template>

      <form v-if="!isConfirmed" class="space-y-5" novalidate @submit.prevent="handleSubmit">
        <div class="space-y-2">
          <Label for="email">Email</Label>
          <Input
            id="email"
            v-model="form.email"
            type="email"
            inputmode="email"
            autocomplete="email"
            placeholder="you@company.com"
            :disabled="isSubmitting"
            :aria-invalid="Boolean(errors.email)"
          />
          <p class="text-sm text-muted-foreground">
            Use the same address provided during sign up.
          </p>
          <p v-if="errors.email" class="text-sm font-medium text-red-600">
            {{ errors.email }}
          </p>
        </div>

        <div class="space-y-2">
          <Label for="code">Confirmation code</Label>
          <Input
            id="code"
            v-model="form.code"
            type="text"
            inputmode="text"
            autocomplete="one-time-code"
            placeholder="ABC123"
            maxlength="6"
            :disabled="isSubmitting"
            :aria-invalid="Boolean(errors.code)"
            class="uppercase tracking-[0.35em]"
            @update:model-value="handleCodeInput"
          />
          <p class="text-sm text-muted-foreground">
            The code has 6 characters and may include letters and numbers.
          </p>
          <p v-if="errors.code" class="text-sm font-medium text-red-600">
            {{ errors.code }}
          </p>
        </div>

        <div class="flex flex-wrap gap-3 pt-2">
          <Button type="submit" size="lg" :disabled="isSubmitting">
            {{ isSubmitting ? "Confirming..." : "Confirm email" }}
          </Button>
          <RouterLink
            :to="{ name: 'auth-register-pending', query: { email: form.email || undefined } }"
            :class="buttonVariants({ variant: 'outline', size: 'lg' })"
          >
            Back to resend
          </RouterLink>
        </div>
      </form>

      <div v-else class="grid gap-4">
        <div class="flex flex-wrap gap-3">
          <RouterLink :to="{ name: 'auth-login', query: { email: confirmedEmail } }" :class="buttonVariants({ size: 'lg' })">
            Go to sign in
          </RouterLink>
          <RouterLink :to="{ name: 'auth-register' }" :class="buttonVariants({ variant: 'outline', size: 'lg' })">
            Create another account
          </RouterLink>
        </div>
      </div>
    </AuthPanel>

    <div class="grid gap-6">
      <AuthInfoCard
        badge="Guidance"
        title="If the code fails, resend it before trying again."
        description="Expired or replaced codes are no longer valid. When that happens, request a new code and use only the most recent one."
      >
        <div class="grid gap-3 text-sm leading-6 text-muted-foreground">
          <div class="rounded-2xl border border-border/70 px-4 py-3">
            Review the email you entered to make sure it matches the local account registration.
          </div>
          <div class="rounded-2xl border border-border/70 px-4 py-3">
            Copy the code exactly as received and use only the most recent delivery.
          </div>
          <div class="rounded-2xl border border-dashed border-border bg-background/70 px-4 py-3">
            If you still have not received the email, go back to the confirmation resend step.
          </div>
        </div>
      </AuthInfoCard>
    </div>
  </section>
</template>

<script setup lang="ts">
import { reactive, ref } from "vue";
import { RouterLink, useRoute } from "vue-router";
import AuthInfoCard from "@/components/shared/AuthInfoCard.vue";
import AuthPanel from "@/components/shared/AuthPanel.vue";
import { useConfirmEmailMutation } from "@/composables/auth/useConfirmEmailMutation";
import { Button, buttonVariants } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  clearEmailConfirmationFormErrors,
  createEmailConfirmationFormValues,
  normalizeConfirmationCode,
  validateEmailConfirmationForm,
  type EmailConfirmationFormErrors,
} from "@/lib/auth/emailConfirmation";

const route = useRoute();
const initialEmail = typeof route.query.email === "string" ? route.query.email.trim() : "";

const form = reactive(createEmailConfirmationFormValues(initialEmail));
const errors = reactive<EmailConfirmationFormErrors>({});
const isConfirmed = ref(false);
const confirmedEmail = ref("");
const confirmEmailMutation = useConfirmEmailMutation();
const isSubmitting = confirmEmailMutation.isSubmitting;

function handleCodeInput(value: string | number | null | undefined) {
  form.code = normalizeConfirmationCode(String(value ?? ""));
}

async function handleSubmit() {
  confirmEmailMutation.reset();
  const nextErrors = validateEmailConfirmationForm(form);

  errors.email = nextErrors.email;
  errors.code = nextErrors.code;

  if (nextErrors.email || nextErrors.code) {
    return;
  }

  clearEmailConfirmationFormErrors(errors);

  try {
    const response = await confirmEmailMutation.submit(form);
    confirmedEmail.value = response.email;
    isConfirmed.value = true;
  } catch {
    errors.email = confirmEmailMutation.fieldErrors.value.email;
    errors.code = confirmEmailMutation.fieldErrors.value.code;
  }
}
</script>
