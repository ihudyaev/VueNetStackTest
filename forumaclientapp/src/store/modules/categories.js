import { api } from "../api";
export default {
  namespaced: true,
  state: {
    feed: [],
    crnCat: null
  },
  getters: {
    allCategories(state) {
      return state.feed || null;
    },
    crnCategory(state) {
      return state.crnCat || null;
    }
  },
  mutations: {
    setCategories(state, payload) {
      state.feed = payload;
    },
    setCrnCategory(state, payload) {
      state.crnCat = payload;
    }
  },
  actions: {
    getCategories: async function({ commit }) {
      const categories = await api.get("/categories");
      commit("setCategories", categories.data);
    },
    getCrnCategory: async function({ commit }, categoryId) {
      const categories = await api.get("/categories/" + categoryId);
      commit("setCrnCategory", categories.data);
    }
  }
};
