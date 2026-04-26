import { computed } from "vue";
import { useMutation } from "@tanstack/vue-query";
import { ApiRequestError, ApiValidationError } from "@/lib/api/errors";
import type { RegisterFormErrors, RegisterFormValues } from "@/lib/auth/register";
import { authService } from "@/services/auth/auth.service";

const authMutationKeys = {
  register: ["auth", "register"] as const,
};

export function useRegisterMutation() {
  const mutation = useMutation({
    mutationKey: authMutationKeys.register,
    mutationFn: (values: RegisterFormValues) => authService.register(values),
    meta: {
      successToast: {
        title: "Conta criada",
        message: "Cadastro concluido. Agora confirme o codigo enviado para o seu email.",
      },
      errorToast: {
        title: "Nao foi possivel criar a conta",
        message: "Nao foi possivel criar a conta.",
      },
    },
  });

  const fieldErrors = computed<RegisterFormErrors>(() => {
    const error = mutation.error.value;

    if (error instanceof ApiValidationError) {
      return error.fieldErrors;
    }

    return {};
  });

  const errorMessage = computed(() => {
    const error = mutation.error.value;

    if (error instanceof ApiValidationError) {
      return error.details[0] ?? "";
    }

    if (error instanceof ApiRequestError) {
      return error.message;
    }

    return "";
  });

  return {
    submit: mutation.mutateAsync,
    reset: mutation.reset,
    isSubmitting: mutation.isPending,
    fieldErrors,
    errorMessage,
  };
}
