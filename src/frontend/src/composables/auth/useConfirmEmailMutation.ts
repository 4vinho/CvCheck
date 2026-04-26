import { computed } from "vue";
import { useMutation } from "@tanstack/vue-query";
import { ApiRequestError, ApiValidationError } from "@/lib/api/errors";
import type {
  EmailConfirmationFormErrors,
  EmailConfirmationFormValues,
} from "@/lib/auth/emailConfirmation";
import { authService } from "@/services/auth/auth.service";

const authMutationKeys = {
  confirmEmail: ["auth", "email-confirmation", "confirm"] as const,
};

export function useConfirmEmailMutation() {
  const mutation = useMutation({
    mutationKey: authMutationKeys.confirmEmail,
    mutationFn: (values: EmailConfirmationFormValues) => authService.confirmEmail(values),
  });

  const fieldErrors = computed<EmailConfirmationFormErrors>(() => {
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
