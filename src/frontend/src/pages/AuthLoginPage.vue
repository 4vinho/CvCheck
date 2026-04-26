<template>
  <section class="grid gap-6 lg:grid-cols-[1.15fr_0.85fr]">
    <AuthPanel
      eyebrow="Acesso com email"
      title="Entre com a conta que ja confirmou o email."
      description="Use o email principal da conta para acessar a area autenticada com seguranca."
    >
      <template #header>
        <div class="rounded-2xl border border-border/70 bg-background/70 p-4 text-sm leading-6 text-muted-foreground">
          Contas locais ainda pendentes de confirmacao precisam concluir esse passo antes do acesso completo.
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
            :disabled="isSubmitting"
            :aria-invalid="Boolean(errors.email)"
          />
          <p class="text-sm text-muted-foreground">
            O email e o identificador principal da sua conta.
          </p>
          <p v-if="errors.email" class="text-sm font-medium text-red-600">
            {{ errors.email }}
          </p>
        </div>

        <div class="space-y-2">
          <Label for="password">Senha</Label>
          <div class="relative">
            <Input
              id="password"
              v-model="form.password"
              :type="showPassword ? 'text' : 'password'"
              autocomplete="current-password"
              placeholder="Informe sua senha"
              :disabled="isSubmitting"
              :aria-invalid="Boolean(errors.password)"
              class="pr-24"
            />
            <button
              type="button"
              class="absolute inset-y-0 right-3 my-auto inline-flex h-8 w-8 items-center justify-center rounded-full text-muted-foreground transition-colors hover:bg-accent hover:text-accent-foreground"
              :disabled="isSubmitting"
              :aria-label="showPassword ? 'Ocultar senha' : 'Mostrar senha'"
              :title="showPassword ? 'Ocultar senha' : 'Mostrar senha'"
              @click="showPassword = !showPassword"
            >
              <EyeOff v-if="showPassword" class="h-4 w-4" aria-hidden="true" />
              <Eye v-else class="h-4 w-4" aria-hidden="true" />
            </button>
          </div>
          <p class="text-sm text-muted-foreground">
            O acesso so e liberado para contas com email ja confirmado.
          </p>
          <p v-if="errors.password" class="text-sm font-medium text-red-600">
            {{ errors.password }}
          </p>
        </div>

        <div class="flex flex-wrap gap-3 pt-2">
          <Button type="submit" size="lg" :disabled="isSubmitting">
            {{ isSubmitting ? "Entrando..." : "Entrar" }}
          </Button>
          <RouterLink
            :to="{ name: 'auth-register' }"
            :class="buttonVariants({ variant: 'outline', size: 'lg' })"
          >
            Criar conta
          </RouterLink>
        </div>
      </form>
    </AuthPanel>

    <div class="grid gap-6">
      <AuthInfoCard
        badge="Confirmacao"
        title="Se a conta estiver pendente, o proprio fluxo te orienta."
        description="Quando o login identificar que falta confirmar o email, voce segue direto para a etapa de reenvio e validacao do codigo."
      >
        <div class="grid gap-3 text-sm leading-6 text-muted-foreground">
          <div class="rounded-2xl border border-border/70 px-4 py-3">
            Use o mesmo email informado no cadastro local.
          </div>
          <div class="rounded-2xl border border-border/70 px-4 py-3">
            Se a confirmacao ainda estiver pendente, voce podera reenviar o codigo.
          </div>
          <div class="rounded-2xl border border-dashed border-border bg-background/70 px-4 py-3">
            Depois da confirmacao, o login passa a liberar o acesso pleno a conta.
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
