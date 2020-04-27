import { api } from "../api";
export default {
  namespaced: true,
  state: {
    token: localStorage.getItem("user-token") || "",
    results: []
  },
  getters: {
    getResults(state) {
      return state.results || null;
    }
  },
  mutations: {
    setResults(state, payload) {
      state.results = payload;
    }
  },
  actions: {
    getResults: async function({ commit }, { searchstring, size }) {
      const response = await api
        .post("/search/dataseacrh/",{searchstring,size})
        .catch(err => {
          console.log(err);
        });
      console.log(response);
      commit("setResults", response.data);
    },
    refreshIndex: async function() {
      await api
        .get("/search/refreshindex")
        .catch(err => {
          console.log(err);
        });
    }
  }
};
