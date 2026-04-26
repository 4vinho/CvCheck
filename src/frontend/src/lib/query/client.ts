import { QueryClient, VueQueryPlugin } from "@tanstack/vue-query";

export const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: false,
      refetchOnWindowFocus: false,
    },
    mutations: {
      retry: false,
    },
  },
});

export const vueQueryPlugin = [VueQueryPlugin, { queryClient }] as const;
