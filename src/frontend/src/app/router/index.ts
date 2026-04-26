import { createRouter, createWebHistory } from "vue-router";
import AppShell from "@/layouts/AppShell.vue";
import AppHomePage from "@/pages/AppHomePage.vue";
import AuthRegisterPage from "@/pages/AuthRegisterPage.vue";
import AuthRegisterPendingPage from "@/pages/AuthRegisterPendingPage.vue";
import AuthShell from "@/layouts/AuthShell.vue";
import NotFoundPage from "@/pages/NotFoundPage.vue";

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: "/",
      redirect: "/auth/cadastro",
    },
    {
      path: "/auth",
      component: AuthShell,
      children: [
        {
          path: "cadastro",
          name: "auth-register",
          component: AuthRegisterPage,
        },
        {
          path: "cadastro/pendente",
          name: "auth-register-pending",
          component: AuthRegisterPendingPage,
        },
      ],
    },
    {
      path: "/app",
      component: AppShell,
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

export default router;
