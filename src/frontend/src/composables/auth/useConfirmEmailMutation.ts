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
    meta: {
      successToast: {
        title: "Account unlocked",
        message: "Email confirmed successfully. Sign in now works normally for this account.",
      },
      errorToast: {
        title: "Could not confirm email",
        message: "Could not confirm email.",
      },
    },
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
