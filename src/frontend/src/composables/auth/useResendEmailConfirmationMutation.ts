import { computed } from "vue";
import { ApiRequestError, ApiValidationError } from "@/lib/api/errors";
import { useApi } from "@/composables/useApi";
import type { ResendEmailConfirmationResponse } from "@/services/auth/auth.service";
import { authService } from "@/services/auth/auth.service";

const authMutationKeys = {
  resendEmailConfirmation: ["auth", "email-confirmation", "resend"] as const,
};

export function useResendEmailConfirmationMutation() {
  const { useApiMutation } = useApi();

  const mutation = useApiMutation({
    mutationKey: authMutationKeys.resendEmailConfirmation,
    mutationFn: (email: string) => authService.resendEmailConfirmation(email),
    meta: {
      successToast: {
        title: "Code resent",
        message: ({ data }: { data?: ResendEmailConfirmationResponse }) =>
          data?.email
            ? `We sent a new code to ${data.email}. Use only the most recent message.`
            : "We sent a new confirmation code to your email.",
      },
      errorToast: {
        title: "Could not resend confirmation",
        message: "Could not resend confirmation.",
      },
    },
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
    fieldError,
    errorMessage,
  };
}
