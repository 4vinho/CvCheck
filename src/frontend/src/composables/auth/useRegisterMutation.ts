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
        title: "Account created",
        message: "Sign up completed. Now confirm the code sent to your email.",
      },
      errorToast: {
        title: "Could not create account",
        message: "Could not create account.",
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
