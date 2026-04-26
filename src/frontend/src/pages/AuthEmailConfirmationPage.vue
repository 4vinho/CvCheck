<template>
  <section class="grid gap-6 lg:grid-cols-[1.15fr_0.85fr]">
    <AuthPanel
      eyebrow="Confirmacao de email"
      title="Confirme o codigo enviado para liberar sua conta."
      description="Use o email principal da conta e o codigo de 6 caracteres recebido por email."
    >
      <template #header>
        <div class="grid gap-3 rounded-[1.5rem] border border-cyan-200 bg-cyan-50/90 p-5 text-sm leading-6 text-cyan-950">
          <p class="font-semibold uppercase tracking-[0.24em] text-cyan-700">
            Antes de continuar
          </p>
          <p>
            A confirmacao libera o acesso completo da conta local e invalida codigos antigos que tenham sido substituidos por um novo reenvio.
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
            placeholder="voce@empresa.com"
            :disabled="isSubmitting"
            :aria-invalid="Boolean(errors.email)"
          />
          <p class="text-sm text-muted-foreground">
            Use o mesmo endereco informado no cadastro.
          </p>
          <p v-if="errors.email" class="text-sm font-medium text-red-600">
            {{ errors.email }}
          </p>
        </div>

        <div class="space-y-2">
          <Label for="code">Codigo de confirmacao</Label>
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
            O codigo tem 6 caracteres e pode incluir letras e numeros.
          </p>
          <p v-if="errors.code" class="text-sm font-medium text-red-600">
            {{ errors.code }}
          </p>
        </div>

        <div class="flex flex-wrap gap-3 pt-2">
          <Button type="submit" size="lg" :disabled="isSubmitting">
            {{ isSubmitting ? "Confirmando..." : "Confirmar email" }}
          </Button>
          <RouterLink
            :to="{ name: 'auth-register-pending', query: { email: form.email || undefined } }"
            :class="buttonVariants({ variant: 'outline', size: 'lg' })"
          >
            Voltar para reenvio
          </RouterLink>
        </div>
      </form>

      <div v-else class="grid gap-4">
        <div class="flex flex-wrap gap-3">
          <RouterLink :to="{ name: 'auth-login', query: { email: confirmedEmail } }" :class="buttonVariants({ size: 'lg' })">
            Ir para login
          </RouterLink>
          <RouterLink :to="{ name: 'auth-register' }" :class="buttonVariants({ variant: 'outline', size: 'lg' })">
            Criar outra conta
          </RouterLink>
        </div>
      </div>
    </AuthPanel>

    <div class="grid gap-6">
      <AuthInfoCard
        badge="Orientacao"
        title="Se o codigo falhar, reenvie antes de tentar de novo."
        description="Codigos expirados ou substituidos deixam de validar. Quando isso acontecer, gere um novo envio e use apenas o codigo mais recente."
      >
        <div class="grid gap-3 text-sm leading-6 text-muted-foreground">
          <div class="rounded-2xl border border-border/70 px-4 py-3">
            Revise o email digitado para garantir que ele corresponde ao cadastro local.
          </div>
          <div class="rounded-2xl border border-border/70 px-4 py-3">
            Copie o codigo exatamente como foi recebido e use apenas o envio mais recente.
          </div>
          <div class="rounded-2xl border border-dashed border-border bg-background/70 px-4 py-3">
            Se ainda nao recebeu o email, volte para a etapa de reenvio da confirmacao.
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
