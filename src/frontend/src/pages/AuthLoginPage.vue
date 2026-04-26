<template>
  <section class="grid gap-6 lg:grid-cols-[1.15fr_0.85fr]">
    <AuthPanel
      eyebrow="Email access"
      title="Sign in with the account that already confirmed its email."
      description="Use your primary account email to access the authenticated area securely."
    >
      <template #header>
        <div class="rounded-2xl border border-border/70 bg-background/70 p-4 text-sm leading-6 text-muted-foreground">
          Local accounts that are still waiting for confirmation must complete that step before full access is granted.
        </div>
      </template>

      <form class="space-y-5" novalidate @submit.prevent="handleSubmit">
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
            Your email is the primary identifier for your account.
          </p>
          <p v-if="errors.email" class="text-sm font-medium text-red-600">
            {{ errors.email }}
          </p>
        </div>

        <div class="space-y-2">
          <Label for="password">Password</Label>
          <div class="relative">
            <Input
              id="password"
              v-model="form.password"
              :type="showPassword ? 'text' : 'password'"
              autocomplete="current-password"
              placeholder="Enter your password"
              :disabled="isSubmitting"
              :aria-invalid="Boolean(errors.password)"
              class="pr-24"
            />
            <button
              type="button"
              class="absolute inset-y-0 right-3 my-auto inline-flex h-8 w-8 items-center justify-center rounded-full text-muted-foreground transition-colors hover:bg-accent hover:text-accent-foreground"
              :disabled="isSubmitting"
              :aria-label="showPassword ? 'Hide password' : 'Show password'"
              :title="showPassword ? 'Hide password' : 'Show password'"
              @click="showPassword = !showPassword"
            >
              <EyeOff v-if="showPassword" class="h-4 w-4" aria-hidden="true" />
              <Eye v-else class="h-4 w-4" aria-hidden="true" />
            </button>
          </div>
          <p class="text-sm text-muted-foreground">
            Access is only available for accounts with a confirmed email.
          </p>
          <p v-if="errors.password" class="text-sm font-medium text-red-600">
            {{ errors.password }}
          </p>
        </div>

        <div class="flex flex-wrap gap-3 pt-2">
          <Button type="submit" size="lg" :disabled="isSubmitting">
            {{ isSubmitting ? "Signing in..." : "Sign in" }}
          </Button>
          <RouterLink
            :to="{ name: 'auth-register' }"
            :class="buttonVariants({ variant: 'outline', size: 'lg' })"
          >
            Create account
          </RouterLink>
        </div>
      </form>
    </AuthPanel>

    <div class="grid gap-6">
      <AuthInfoCard
        badge="Confirmation"
        title="If the account is still pending, the flow guides you."
        description="When sign in detects a missing email confirmation, you move directly to the resend and code verification step."
      >
        <div class="grid gap-3 text-sm leading-6 text-muted-foreground">
          <div class="rounded-2xl border border-border/70 px-4 py-3">
            Use the same email address provided during local sign up.
          </div>
          <div class="rounded-2xl border border-border/70 px-4 py-3">
            If confirmation is still pending, you can resend the code.
          </div>
          <div class="rounded-2xl border border-dashed border-border bg-background/70 px-4 py-3">
            After confirmation, sign in unlocks full account access.
          </div>
        </div>
      </AuthInfoCard>
    </div>
  </section>
</template>

<script setup lang="ts">
import { Eye, EyeOff } from "lucide-vue-next";
import { reactive, ref } from "vue";
import { RouterLink, useRouter } from "vue-router";
import AuthInfoCard from "@/components/shared/AuthInfoCard.vue";
import AuthPanel from "@/components/shared/AuthPanel.vue";
import { useLoginMutation } from "@/composables/auth/useLoginMutation";
import { Button, buttonVariants } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { clearLoginFormErrors, createLoginFormValues, validateLoginForm, type LoginFormErrors } from "@/lib/auth/login";

const router = useRouter();

const form = reactive(createLoginFormValues());
const errors = reactive<LoginFormErrors>({});
const showPassword = ref(false);
const loginMutation = useLoginMutation();
const isSubmitting = loginMutation.isSubmitting;

async function handleSubmit() {
  loginMutation.reset();
  const nextErrors = validateLoginForm(form);

  errors.email = nextErrors.email;
  errors.password = nextErrors.password;

  if (nextErrors.email || nextErrors.password) {
    return;
  }

  clearLoginFormErrors(errors);

  try {
    await loginMutation.submit(form);
    router.push({ name: "app-home" });
  } catch {
    const pendingEmail = loginMutation.pendingEmail.value;

    if (pendingEmail) {
      router.push({
        name: "auth-register-pending",
        query: {
          email: pendingEmail,
          source: "login",
        },
      });
      return;
    }

    errors.email = loginMutation.fieldErrors.value.email;
    errors.password = loginMutation.fieldErrors.value.password;
  }
}
</script>
