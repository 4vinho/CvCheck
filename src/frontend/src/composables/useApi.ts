import { computed } from "vue";
import { useMutation, useQuery } from "@tanstack/vue-query";
import { clearApiSession, markApiAuthenticated, useApiSessionState } from "@/lib/api/apiSession";

export function useApi() {
  const sessionState = useApiSessionState();

  return {
    isAuthenticated: computed(() => sessionState.value),
    markAuthenticated: markApiAuthenticated,
    markUnauthenticated: clearApiSession,
    useApiMutation: useMutation,
    useApiQuery: useQuery,
  };
}
