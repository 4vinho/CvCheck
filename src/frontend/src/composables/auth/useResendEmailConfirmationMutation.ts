import { computed } from "vue";
import { useMutation } from "@tanstack/vue-query";
import { ApiRequestError, ApiValidationError } from "@/lib/api/errors";
import { authService } from "@/services/auth/auth.service";

const authMutationKeys = {
  resendEmailConfirmation: ["auth", "email-confirmation", "resend"] as const,
};

export function useResendEmailConfirmationMutation() {
  const mutation = useMutation({
    mutationKey: authMutationKeys.resendEmailConfirmation,
    mutationFn: (email: string) => authService.resendEmailConfirmation(email),
  });

  const fieldError = computed(() => {
    const error = mutation.error.value;

    if (error instanceof ApiValidationError) {
      return error.fieldErrors.email ?? "";
    }

    return "";
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
    fieldError,
    errorMessage,
  };
}
