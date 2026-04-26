import { computed } from "vue";
import { useMutation } from "@tanstack/vue-query";
import { ApiRequestError, ApiValidationError, PendingEmailConfirmationError } from "@/lib/api/errors";
import type { LoginFormErrors, LoginFormValues } from "@/lib/auth/login";
import { authService } from "@/services/auth/auth.service";

const authMutationKeys = {
  login: ["auth", "login"] as const,
};

export function useLoginMutation() {
  const mutation = useMutation({
    mutationKey: authMutationKeys.login,
    mutationFn: (values: LoginFormValues) => authService.login(values),
    meta: {
      successToast: {
        title: "Sign in completed",
        message: "Your authenticated account is ready for the next flows.",
      },
      errorToast: {
        title: "Could not sign in",
        message: "Could not sign in.",
      },
    },
  });

  const fieldErrors = computed<LoginFormErrors>(() => {
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

  const pendingEmail = computed(() => {
    const error = mutation.error.value;

    if (error instanceof PendingEmailConfirmationError) {
      return error.email;
    }

    return "";
  });

  return {
    submit: mutation.mutateAsync,
    reset: mutation.reset,
    isSubmitting: mutation.isPending,
    fieldErrors,
    errorMessage,
    pendingEmail,
  };
}
