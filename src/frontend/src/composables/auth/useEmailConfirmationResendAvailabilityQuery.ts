import { computed } from "vue";
import { useQuery } from "@tanstack/vue-query";
import { authService } from "@/services/auth/auth.service";

const authQueryKeys = {
  resendAvailability: (email: string) => ["auth", "email-confirmation", "resend-availability", email] as const,
};

export function useEmailConfirmationResendAvailabilityQuery(email: string) {
  const normalizedEmail = computed(() => email.trim());

  const query = useQuery({
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
