import Vue from "vue";
import {BootstrapVue, IconsPlugin }  from 'bootstrap-vue'
import App from "./App.vue";
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import router from "./router";
import store from "./store";
import axios from "axios";


//import "./assets/main.css";
Vue.use(BootstrapVue)
Vue.use(IconsPlugin)

Vue.config.productionTip = false;

const token = localStorage.getItem("user-token");
if (token) {
  axios.defaults.headers.common["Authorization"] = token;
}

new Vue({
  router,
  store,
   BootstrapVue,
   IconsPlugin,
  render: h => h(App)
}).$mount("#app");
