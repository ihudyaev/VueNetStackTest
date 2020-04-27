import { api } from "../api";
export default {
  namespaced: true,
  state: {
    feed: []
  },
  getters: {
    allTopicReplies(state) {
      return state.feed || null;
    },
    hasReplies(state) {
      if (state.feed.length > 0) {
        return true;
      } else {
        return null;
      }
    }
  },
  mutations: {
    setTopicReplies(state, payload) {
      state.feed = payload;
    },
    addTopicReply(state, payload) {
      localStorage["username"] = "";
      localStorage["comment"] = "";
      state.feed.push(payload);
    }
  },
  actions: {
    getTopicReplies: async function({ commit }, topicId) {
      let config = {
        params: {
          topicId: topicId
          // userId: -1,
          // skip: -1,
          // take: -1
        }
      };
      const replies = await api.get("/topicReplies/", config);
      // console.log(replies.data);
      commit("setTopicReplies", replies.data);
    },
    createTopicReply: async function(
      { commit },
      { username, description, topicid, isAnonym }
    ) {
      const response = await api
        .post("/topicreplies", {
          username,
          description,
          topicid,
          isAnonym
        })
        .catch(err => {
          console.log(err);
        });

      if (response.data) {
        commit("addTopicReply", response.data);
      }
    }
  }
};
