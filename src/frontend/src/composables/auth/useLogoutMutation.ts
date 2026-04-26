import { useApi } from "@/composables/useApi";
import { authService } from "@/services/auth/auth.service";

const authMutationKeys = {
  logout: ["auth", "logout"] as const,
};

export function useLogoutMutation() {
  const { markUnauthenticated, useApiMutation } = useApi();

  const mutation = useApiMutation({
    mutationKey: authMutationKeys.logout,
    mutationFn: () => authService.logout(),
    onSuccess: () => {
      markUnauthenticated();
    },
    meta: {
      successToast: {
        title: "Signed out",
        message: "Your session has been closed successfully.",
      },
      errorToast: {
        title: "Could not sign out",
        message: "Could not sign out.",
      },
    },
  });

  return {
    submit: mutation.mutateAsync,
    reset: mutation.reset,
    isSubmitting: mutation.isPending,
  };
}
