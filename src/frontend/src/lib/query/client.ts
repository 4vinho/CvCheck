import { MutationCache, QueryClient, VueQueryPlugin } from "@tanstack/vue-query";
import { ApiValidationError } from "@/lib/api/errors";
import type { MutationToastDescriptor, MutationToastMeta } from "@/lib/query/mutationToast";
import { pushToast } from "@/lib/toast/toast";

function resolveToastMessage(descriptor: MutationToastDescriptor, context: {
  data?: unknown;
  variables?: unknown;
  error?: unknown;
}) {
  if (typeof descriptor.message === "function") {
    return descriptor.message(context);
  }

  return descriptor.message;
}

export const queryClient = new QueryClient({
  mutationCache: new MutationCache({
    onSuccess: (data, variables, _context, mutation) => {
      const meta = mutation.meta as MutationToastMeta | undefined;

      if (!meta?.successToast) {
        return;
      }

      pushToast({
        variant: "success",
        title: meta.successToast.title,
        message: resolveToastMessage(meta.successToast, { data, variables }),
      });
    },
    onError: (error, variables, _context, mutation) => {
      if (error instanceof ApiValidationError) {
        pushToast({
          variant: "error",
          title: error.title,
          descriptionList: error.details,
        });
        return;
      }

      const meta = mutation.meta as MutationToastMeta | undefined;

      if (!meta?.errorToast) {
        return;
      }

      pushToast({
        variant: "error",
        title: meta.errorToast.title,
        message: resolveToastMessage(meta.errorToast, { error, variables }),
      });
    },
  }),
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
