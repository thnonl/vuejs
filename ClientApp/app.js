import Vue from "vue";
import axios from "axios";
import router from "./router/index";
import App from "components/app-root";
import { FontAwesomeIcon } from "./icons";

// Registration of global components
Vue.component("icon", FontAwesomeIcon);

Vue.prototype.$http = axios;

const app = new Vue({
  router,
  ...App
});

export { app, router };
