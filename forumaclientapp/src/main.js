import Vue from "vue";
import { IconsPlugin } from "bootstrap-vue";
import App from "./App.vue";
import "bootswatch/dist/journal/bootstrap.min.css";
import router from "./router";
import store from "./store";
import axios from "axios";
import "material-design-icons-iconfont";

Vue.use(IconsPlugin);
Vue.config.productionTip = false;

const token = localStorage.getItem("user-token");
if (token) {
  axios.defaults.headers.common["Authorization"] = token;
}

new Vue({
  router,
  store,
  IconsPlugin,
  render: h => h(App)
}).$mount("#app");
