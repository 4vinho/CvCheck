<template>
  <section class="mx-auto grid max-w-4xl gap-6">
    <AuthPanel
      eyebrow="Confirmacao pendente"
      :title="panelTitle"
      :description="panelDescription"
      class="overflow-hidden"
    >
      <template #header>
        <div class="grid gap-4 rounded-[1.5rem] border border-emerald-200 bg-emerald-50/90 p-5">
          <div class="space-y-2">
            <p class="text-sm font-semibold uppercase tracking-[0.24em] text-emerald-700">
              Estado da conta
            </p>
            <p class="text-2xl font-semibold tracking-tight text-emerald-950">
              Pendente de confirmacao
            </p>
          </div>
          <p class="text-sm leading-6 text-emerald-900">
            Este email sera usado como identificador principal da sua conta:
          </p>
          <div class="rounded-2xl border border-emerald-200 bg-white/90 px-4 py-3 text-lg font-semibold text-emerald-950">
            {{ resolvedEmail }}
          </div>
        </div>
      </template>

      <div
        v-if="successMessage"
        class="rounded-2xl border border-emerald-200 bg-emerald-50 px-4 py-3 text-sm leading-6 text-emerald-800"
      >
        {{ successMessage }}
      </div>

      <div
        v-if="submitError"
        class="rounded-2xl border border-red-200 bg-red-50 px-4 py-3 text-sm leading-6 text-red-700"
      >
        {{ submitError }}
      </div>

      <div class="grid gap-4 md:grid-cols-2">
        <div class="rounded-2xl border border-border/70 bg-background/70 p-4">
          <p class="text-sm font-semibold uppercase tracking-[0.24em] text-muted-foreground">
            O que isso significa
          </p>
          <p class="mt-3 text-sm leading-6 text-muted-foreground">
            Ate a confirmacao do email, o acesso completo a conta permanece indisponivel.
          </p>
        </div>

        <div class="rounded-2xl border border-dashed border-border bg-background/70 p-4">
          <p class="text-sm font-semibold uppercase tracking-[0.24em] text-muted-foreground">
            Proximo passo
          </p>
          <p class="mt-3 text-sm leading-6 text-muted-foreground">
            Verifique sua caixa de entrada, use o codigo mais recente recebido e conclua a confirmacao para liberar o acesso.
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
            Informar codigo
          </RouterLink>
          <RouterLink :to="{ name: 'auth-register' }" :class="buttonVariants({ size: 'lg' })">
            Alterar email
          </RouterLink>
          <RouterLink :to="{ name: 'auth-login', query: { email: resolvedEmail } }" :class="buttonVariants({ variant: 'ghost', size: 'lg' })">
            Voltar ao login
          </RouterLink>
        </div>
      </template>
    </AuthPanel>
  </section>
</template>

<script setup lang="ts">
import { computed, onBeforeUnmount, ref } from "vue";
import { RouterLink, useRoute } from "vue-router";
import AuthPanel from "@/components/shared/AuthPanel.vue";
import { useResendEmailConfirmationMutation } from "@/composables/auth/useResendEmailConfirmationMutation";
import { Button, buttonVariants } from "@/components/ui/button";

const route = useRoute();
const resendMutation = useResendEmailConfirmationMutation();
const submitError = ref("");
const successMessage = ref("");
const resendCooldown = ref(0);
let cooldownTimer: ReturnType<typeof setInterval> | undefined;

const resolvedEmail = computed(() => {
  const email = route.query.email;

  if (typeof email === "string" && email.trim()) {
    return email.trim();
  }

  return "email nao informado";
});

const isFromLogin = computed(() => route.query.source === "login");

const panelTitle = computed(() =>
  isFromLogin.value
    ? "O acesso esta aguardando a confirmacao do email."
    : "Cadastro recebido. Falta confirmar o email.",
);

const panelDescription = computed(() =>
  isFromLogin.value
    ? "Esta conta ja existe, mas ainda precisa concluir a confirmacao para liberar o uso pleno."
    : "Enviamos a proxima etapa para o endereco informado. A conta sera liberada apos a confirmacao do email.",
);

const isResendDisabled = computed(() =>
  resendMutation.isSubmitting.value || resendCooldown.value > 0 || resolvedEmail.value === "email nao informado",
);

const resendButtonLabel = computed(() => {
  if (resendMutation.isSubmitting.value) {
    return "Reenviando...";
  }

  if (resendCooldown.value > 0) {
    return `Reenviar em ${resendCooldown.value}s`;
  }

  return "Reenviar email";
});

function startCooldown() {
  resendCooldown.value = 30;

  if (cooldownTimer) {
    clearInterval(cooldownTimer);
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

async function handleResend() {
  resendMutation.reset();
  submitError.value = "";
  successMessage.value = "";

  try {
    const response = await resendMutation.submit(resolvedEmail.value);
    successMessage.value = `Enviamos um novo codigo para ${response.email}. Use apenas a mensagem mais recente.`;
    startCooldown();
  } catch {
    submitError.value = resendMutation.fieldError.value || resendMutation.errorMessage.value;
  }
}

onBeforeUnmount(() => {
  if (cooldownTimer) {
    clearInterval(cooldownTimer);
  }
});
</script>
