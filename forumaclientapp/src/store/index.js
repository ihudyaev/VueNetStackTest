import Vue from "vue";
import Vuex from "vuex";
import users from "./modules/users";
import categories from "./modules/categories";
import topics from "./modules/topics";
import topicreplies from "./modules/topicreplies";
import search from "./modules/search";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {},
  mutations: {},
  actions: {},
  modules: {
    users,
    categories,
    topics,
    topicreplies,
    search
  }
});
