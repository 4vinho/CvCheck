import { computed } from "vue";
import { useApi } from "@/composables/useApi";
import { authService } from "@/services/auth/auth.service";

const authQueryKeys = {
  resendAvailability: (email: string) => ["auth", "email-confirmation", "resend-availability", email] as const,
};

export function useEmailConfirmationResendAvailabilityQuery(email: string) {
  const normalizedEmail = computed(() => email.trim());
  const { useApiQuery } = useApi();

  const query = useApiQuery({
    queryKey: computed(() => authQueryKeys.resendAvailability(normalizedEmail.value)),
    queryFn: () => authService.getEmailConfirmationResendAvailability(normalizedEmail.value),
    enabled: computed(() => Boolean(normalizedEmail.value)),
    retry: false,
    staleTime: 0,
  });

  return {
    availability: query.data,
    isLoading: query.isLoading,
    isFetching: query.isFetching,
    refetch: query.refetch,
  };
}
