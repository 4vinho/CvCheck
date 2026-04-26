<template>
  <section class="grid gap-6 lg:grid-cols-[1.15fr_0.85fr]">
    <AuthPanel
      eyebrow="Acesso com email"
      title="Crie sua conta local com email e senha."
      description="Use seu email para criar uma conta e acompanhar o acesso com mais seguranca."
    >
      <template #header>
        <div class="rounded-2xl border border-border/70 bg-background/70 p-4 text-sm leading-6 text-muted-foreground">
          O email sera o identificador principal da conta. Ate a confirmacao, o acesso pleno continua bloqueado.
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
            placeholder="voce@empresa.com"
            :aria-invalid="Boolean(errors.email)"
          />
          <p class="text-sm text-muted-foreground">
            Use um email que voce consiga acessar para confirmar a conta depois.
          </p>
          <p v-if="errors.email" class="text-sm font-medium text-red-600">
            {{ errors.email }}
          </p>
        </div>

        <div class="space-y-2">
          <Label for="password">Senha</Label>
          <Input
            id="password"
            v-model="form.password"
            type="password"
            autocomplete="new-password"
            placeholder="Defina uma senha"
            :aria-invalid="Boolean(errors.password)"
          />
          <p v-if="errors.password" class="text-sm font-medium text-red-600">
            {{ errors.password }}
          </p>
        </div>

        <div class="space-y-2">
          <Label for="confirmPassword">Confirmar senha</Label>
          <Input
            id="confirmPassword"
            v-model="form.confirmPassword"
            type="password"
            autocomplete="new-password"
            placeholder="Repita a senha"
            :aria-invalid="Boolean(errors.confirmPassword)"
          />
          <p v-if="errors.confirmPassword" class="text-sm font-medium text-red-600">
            {{ errors.confirmPassword }}
          </p>
        </div>

        <div class="flex flex-wrap gap-3 pt-2">
          <Button type="submit" size="lg">
            Criar conta
          </Button>
        </div>
      </form>
    </AuthPanel>

    <div class="grid gap-6">
      <AuthInfoCard
        badge="Seguranca"
        title="Sua conta comeca pelo email"
        description="Depois do cadastro, enviaremos a confirmacao para o endereco informado. Ate esse passo ser concluido, alguns recursos permanecem indisponiveis."
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
          Apos o cadastro, a conta segue em estado pendente ate a confirmacao por email.
        </div>
      </AuthInfoCard>
    </div>
  </section>
</template>

<script setup lang="ts">
import { reactive } from "vue";
import AuthInfoCard from "@/components/shared/AuthInfoCard.vue";
import AuthPanel from "@/components/shared/AuthPanel.vue";
import { useRouter } from "vue-router";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  createRegisterFormValues,
  getRegisterPasswordGuidance,
  type RegisterFormErrors,
  validateRegisterForm,
} from "@/lib/auth/register";

const router = useRouter();

const form = reactive(createRegisterFormValues());
const errors = reactive<RegisterFormErrors>({});
const passwordGuidance = getRegisterPasswordGuidance();

function handleSubmit() {
  const nextErrors = validateRegisterForm(form);

  errors.email = nextErrors.email;
  errors.password = nextErrors.password;
  errors.confirmPassword = nextErrors.confirmPassword;

  if (nextErrors.email || nextErrors.password || nextErrors.confirmPassword) {
    return;
  }

  router.push({
    name: "auth-register-pending",
    query: {
      email: form.email.trim(),
    },
  });
}
</script>
