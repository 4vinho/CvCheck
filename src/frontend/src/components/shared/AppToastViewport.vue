<template>
  <div class="pointer-events-none fixed inset-x-0 top-4 z-[100] flex justify-center px-4">
    <TransitionGroup
      tag="div"
      enter-active-class="transition-all duration-200 ease-out"
      enter-from-class="-translate-y-3 opacity-0"
      enter-to-class="translate-y-0 opacity-100"
      leave-active-class="transition-all duration-150 ease-in"
      leave-from-class="translate-y-0 opacity-100"
      leave-to-class="-translate-y-2 opacity-0"
      class="flex w-full max-w-md flex-col gap-3"
    >
      <div
        v-for="toast in toasts"
        :key="toast.id"
        class="pointer-events-auto overflow-hidden rounded-2xl border shadow-lg backdrop-blur"
        :class="toast.variant === 'error'
          ? 'border-red-200 bg-red-50/95 text-red-900'
          : 'border-emerald-200 bg-white/95 text-slate-950'"
      >
        <div class="flex items-start gap-3 p-4">
          <div
            class="mt-0.5 flex h-9 w-9 shrink-0 items-center justify-center rounded-full"
            :class="toast.variant === 'error' ? 'bg-red-100 text-red-700' : 'bg-emerald-100 text-emerald-700'"
          >
            <CircleAlert v-if="toast.variant === 'error'" class="h-4 w-4" />
            <CircleCheckBig v-else class="h-4 w-4" />
          </div>

          <div class="min-w-0 flex-1 space-y-1">
            <p class="text-sm font-semibold tracking-tight">
              {{ toast.title }}
            </p>
            <p v-if="toast.message" class="text-sm leading-6 text-current/80">
              {{ toast.message }}
            </p>
            <ul v-if="toast.descriptionList.length" class="list-disc space-y-1 pl-5 text-sm leading-6 text-current/80">
              <li v-for="item in toast.descriptionList" :key="item">
                {{ item }}
              </li>
            </ul>
          </div>

          <button
            type="button"
            class="rounded-full p-1 text-current/60 transition-colors hover:bg-black/5 hover:text-current"
            @click="dismissToast(toast.id)"
          >
            <X class="h-4 w-4" />
          </button>
        </div>
      </div>
    </TransitionGroup>
  </div>
</template>

<script setup lang="ts">
import { CircleAlert, CircleCheckBig, X } from "lucide-vue-next";
import { useToastStore } from "@/lib/toast/toast";

const { toasts, dismissToast } = useToastStore();
</script>
