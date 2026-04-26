<template>
  <section class="grid gap-6 lg:grid-cols-[1.15fr_0.85fr]">
    <AuthPanel
      eyebrow="Email access"
      title="Create your local account with email and password."
      description="Use your email to create an account and manage access more securely."
    >
      <template #header>
        <div class="rounded-2xl border border-border/70 bg-background/70 p-4 text-sm leading-6 text-muted-foreground">
          Your email will be the primary account identifier. Full access stays locked until confirmation is complete.
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
          <p :class="getHelpTextClass(helpMessages.email.tone)">
            {{ helpMessages.email.text }}
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
              autocomplete="new-password"
              placeholder="Create a password"
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
          <p :class="getHelpTextClass(helpMessages.password.tone)">
            {{ helpMessages.password.text }}
          </p>
          <p v-if="errors.password" class="text-sm font-medium text-red-600">
            {{ errors.password }}
          </p>
        </div>

        <div class="space-y-2">
          <Label for="confirmPassword">Confirm password</Label>
          <div class="relative">
            <Input
              id="confirmPassword"
              v-model="form.confirmPassword"
              :type="showConfirmPassword ? 'text' : 'password'"
              autocomplete="new-password"
              placeholder="Repeat your password"
              :disabled="isSubmitting"
              :aria-invalid="Boolean(errors.confirmPassword)"
              class="pr-24"
            />
            <button
              type="button"
              class="absolute inset-y-0 right-3 my-auto inline-flex h-8 w-8 items-center justify-center rounded-full text-muted-foreground transition-colors hover:bg-accent hover:text-accent-foreground"
              :disabled="isSubmitting"
              :aria-label="showConfirmPassword ? 'Hide password' : 'Show password'"
              :title="showConfirmPassword ? 'Hide password' : 'Show password'"
              @click="showConfirmPassword = !showConfirmPassword"
            >
              <EyeOff v-if="showConfirmPassword" class="h-4 w-4" aria-hidden="true" />
              <Eye v-else class="h-4 w-4" aria-hidden="true" />
            </button>
          </div>
          <p :class="getHelpTextClass(helpMessages.confirmPassword.tone)">
            {{ helpMessages.confirmPassword.text }}
          </p>
          <p v-if="errors.confirmPassword" class="text-sm font-medium text-red-600">
            {{ errors.confirmPassword }}
          </p>
        </div>

        <div class="flex flex-wrap gap-3 pt-2">
          <Button type="submit" size="lg" :disabled="isSubmitting">
            {{ isSubmitting ? "Creating account..." : "Create account" }}
          </Button>
        </div>
      </form>
    </AuthPanel>

    <div class="grid gap-6">
      <AuthInfoCard
        badge="Security"
        title="Your account starts with email"
        description="After sign up, we will send a confirmation message to the address you provided. Some features stay unavailable until that step is complete."
      >
        <ul class="space-y-3 text-sm leading-6 text-muted-foreground">
          <li
            v-for="rule in passwordGuidance"
            :key="rule"
            class="rounded-2xl border border-border/70 px-4 py-3"
          >
            {{ rule }}
          </li>
        </ul>

        <div class="rounded-2xl border border-dashed border-border bg-background/70 px-4 py-3 text-sm leading-6 text-muted-foreground">
          After sign up, the account remains pending until email confirmation is completed.
        </div>
      </AuthInfoCard>
    </div>
  </section>
</template>

<script setup lang="ts">
import { Eye, EyeOff } from "lucide-vue-next";
import { onBeforeUnmount, reactive, ref, watch } from "vue";
import AuthInfoCard from "@/components/shared/AuthInfoCard.vue";
import AuthPanel from "@/components/shared/AuthPanel.vue";
import { useRouter } from "vue-router";
import { useRegisterMutation } from "@/composables/auth/useRegisterMutation";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  clearRegisterFormErrors,
  createRegisterFormValues,
  getRegisterFormHelp,
  getRegisterPasswordGuidance,
  type RegisterHelpMessage,
  type RegisterFormErrors,
  type RegisterFormHelp,
} from "@/lib/auth/register";

const router = useRouter();

const form = reactive(createRegisterFormValues());
const errors = reactive<RegisterFormErrors>({});
const passwordGuidance = getRegisterPasswordGuidance();
const registerMutation = useRegisterMutation();
const isSubmitting = registerMutation.isSubmitting;
const helpMessages = reactive<RegisterFormHelp>(getRegisterFormHelp(form));
const showPassword = ref(false);
const showConfirmPassword = ref(false);
let helpTimer: ReturnType<typeof setTimeout> | undefined;

watch(
  () => [form.email, form.password, form.confirmPassword],
  () => {
    if (helpTimer) {
      clearTimeout(helpTimer);
    }

    helpTimer = setTimeout(() => {
      const nextHelpMessages = getRegisterFormHelp(form);
      helpMessages.email = nextHelpMessages.email;
      helpMessages.password = nextHelpMessages.password;
      helpMessages.confirmPassword = nextHelpMessages.confirmPassword;
    }, 500);
  },
);

onBeforeUnmount(() => {
  if (helpTimer) {
    clearTimeout(helpTimer);
  }
});

function getHelpTextClass(tone: RegisterHelpMessage["tone"]) {
  if (tone === "error") {
    return "text-sm font-medium text-red-600";
  }

  if (tone === "success") {
    return "text-sm text-emerald-700";
  }

  return "text-sm text-muted-foreground";
}

async function handleSubmit() {
  registerMutation.reset();
  clearRegisterFormErrors(errors);

  try {
    const result = await registerMutation.submit(form);

    router.push({
      name: "auth-register-pending",
      query: {
        email: result.email,
      },
    });
  } catch {
    errors.email = registerMutation.fieldErrors.value.email;
    errors.password = registerMutation.fieldErrors.value.password;
    errors.confirmPassword = registerMutation.fieldErrors.value.confirmPassword;
  }
}
</script>
