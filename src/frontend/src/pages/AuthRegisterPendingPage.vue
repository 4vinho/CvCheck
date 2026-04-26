<template>
  <section class="mx-auto grid max-w-4xl gap-6">
    <AuthPanel
      eyebrow="Confirmacao pendente"
      title="Cadastro recebido. Falta confirmar o email."
      description="Enviamos a proxima etapa para o endereco informado. A conta sera liberada apos a confirmacao do email."
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
            Verifique sua caixa de entrada e siga as instrucoes para confirmar o cadastro.
          </p>
        </div>
      </div>

      <template #footer>
        <div class="flex flex-wrap gap-3">
          <Button variant="outline" size="lg" disabled>
            Reenviar email
          </Button>
          <RouterLink :to="{ name: 'auth-register' }" :class="buttonVariants({ size: 'lg' })">
            Alterar email
          </RouterLink>
        </div>
      </template>
    </AuthPanel>
  </section>
</template>

<script setup lang="ts">
import { computed } from "vue";
import { RouterLink, useRoute } from "vue-router";
import AuthPanel from "@/components/shared/AuthPanel.vue";
import { Button, buttonVariants } from "@/components/ui/button";

const route = useRoute();

const resolvedEmail = computed(() => {
  const email = route.query.email;

  if (typeof email === "string" && email.trim()) {
    return email.trim();
  }

  return "email nao informado";
});
</script>
