import { createApp } from "vue";
import App from "./App.vue";
import router from "@/app/router";
import { vueQueryPlugin } from "@/lib/query/client";
import "@/styles/globals.css";

const app = createApp(App);

app.use(router);
app.use(...vueQueryPlugin);
app.mount("#app");
