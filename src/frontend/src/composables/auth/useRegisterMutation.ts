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
      return error.userMessage ?? "";
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
