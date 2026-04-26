import { reactive } from "vue";

export type ToastVariant = "success" | "error";

export interface ToastInput {
  title: string;
  message?: string;
  descriptionList?: string[];
  variant?: ToastVariant;
  durationMs?: number;
}

export interface ToastRecord {
  id: number;
  title: string;
  message: string;
  descriptionList: string[];
  variant: ToastVariant;
  durationMs: number;
}

const toasts = reactive<ToastRecord[]>([]);

let nextToastId = 1;

export function pushToast(input: ToastInput) {
  const toast: ToastRecord = {
    id: nextToastId++,
    variant: input.variant ?? "success",
    durationMs: input.durationMs ?? 4000,
    title: input.title,
    message: input.message ?? "",
    descriptionList: input.descriptionList ?? [],
  };

  toasts.push(toast);

  setTimeout(() => {
    dismissToast(toast.id);
  }, toast.durationMs);

  return toast.id;
}

export function dismissToast(id: number) {
  const index = toasts.findIndex((toast) => toast.id === id);

  if (index >= 0) {
    toasts.splice(index, 1);
  }
}

export function useToastStore() {
  return {
    toasts,
    dismissToast,
  };
}
