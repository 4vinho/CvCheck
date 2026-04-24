import { createRouter, createWebHistory } from "vue-router";
import AppShell from "@/layouts/AppShell.vue";
import AppHomePage from "@/pages/AppHomePage.vue";
import NotFoundPage from "@/pages/NotFoundPage.vue";

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: "/",
      redirect: "/app",
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

