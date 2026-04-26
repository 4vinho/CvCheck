import { ref } from "vue";

const apiSessionStorageKey = "cvcheck.api.authenticated";

function readStoredSession() {
  if (typeof window === "undefined") {
    return false;
  }

  return window.localStorage.getItem(apiSessionStorageKey) === "true";
}

const isAuthenticated = ref(readStoredSession());

export function useApiSessionState() {
  return isAuthenticated;
}

export function hasApiSession() {
  return isAuthenticated.value;
}

export function markApiAuthenticated() {
  isAuthenticated.value = true;

  if (typeof window !== "undefined") {
    window.localStorage.setItem(apiSessionStorageKey, "true");
  }
}

export function clearApiSession() {
  isAuthenticated.value = false;

  if (typeof window !== "undefined") {
    window.localStorage.removeItem(apiSessionStorageKey);
  }
}

export function redirectToLoginPage() {
  if (typeof window === "undefined") {
    return;
  }

  if (window.location.pathname === "/auth/login") {
    return;
  }

  window.location.replace("/auth/login");
}
