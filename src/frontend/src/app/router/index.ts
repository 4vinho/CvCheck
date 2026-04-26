import { createRouter, createWebHistory } from "vue-router";
import { hasApiSession } from "@/lib/api/apiSession";
import AppShell from "@/layouts/AppShell.vue";
import AppHomePage from "@/pages/AppHomePage.vue";
import AuthEmailConfirmationPage from "@/pages/AuthEmailConfirmationPage.vue";
import AuthLoginPage from "@/pages/AuthLoginPage.vue";
import AuthRegisterPage from "@/pages/AuthRegisterPage.vue";
import AuthRegisterPendingPage from "@/pages/AuthRegisterPendingPage.vue";
import AuthShell from "@/layouts/AuthShell.vue";
import NotFoundPage from "@/pages/NotFoundPage.vue";

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: "/",
      redirect: "/auth/register",
    },
    {
      path: "/auth",
      component: AuthShell,
      children: [
        {
          path: "login",
          name: "auth-login",
          component: AuthLoginPage,
        },
        {
          path: "register",
          name: "auth-register",
          component: AuthRegisterPage,
        },
        {
          path: "register/pending",
          name: "auth-register-pending",
          component: AuthRegisterPendingPage,
        },
        {
          path: "confirm-email",
          name: "auth-email-confirmation",
          component: AuthEmailConfirmationPage,
        },
      ],
    },
    {
      path: "/app",
      component: AppShell,
      meta: {
        requiresAuth: true,
      },
      children: [
        {
          path: "",
          name: "app-home",
          component: AppHomePage,
        },
      ],
    },
    {
      path: "/:pathMatch(.*)*",
      name: "not-found",
      component: NotFoundPage,
    },
  ],
});

router.beforeEach((to) => {
  if (to.meta.requiresAuth && !hasApiSession()) {
    return {
      name: "auth-login",
      query: {
        redirect: to.fullPath,
      },
    };
  }

  return true;
});

export default router;
