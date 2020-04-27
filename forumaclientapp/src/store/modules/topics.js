import { api } from "../api";
export default {
  namespaced: true,
  state: {
    feed: [],
    crnTop: null
  },
  getters: {
    allTopics(state) {
      return state.feed || null;
    },
    hasTopics(state) {
      if (state.feed.length > 0) {
        return true;
      } else {
        return null;
      }
    },
    crnTopic(state) {
      return state.crnTop || null;
    }
  },
  mutations: {
    setTopics(state, payload) {
      state.feed = payload;
    },
    setCrnTopic(state, payload) {
      state.crnTop = payload;
    }
  },
  actions: {
    getTopics: async function({ commit }, categoryId) {
      let config = {
        params: {
          categoryId: categoryId
          // userId: -1,
          // skip: -1,
          // take: -1
        }
      };
      const topics = await api.get("/topics/", config);
      // console.log(topics.data);
      commit("setTopics", topics.data);
    },
    getCrnTopic: async function({ commit }, topicId) {
      const categories = await api.get("/topics/" + topicId);
      commit("setCrnTopic", categories.data);
    },
    createTopic: async function(
      { commit },
      { title, description, username, category }
    ) {
      const response = await api.post("/topics", {
        title,
        description,
        username,
        category
      });
      // console.log("create response");
      // console.log(response.data);
      commit("setCrnTopic", response.data);
    }
  }
};
