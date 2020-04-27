import { api, setToken } from "../api";
export default {
  namespaced: true,
  state: {
    user: null,
    profile: null,
    token: localStorage.getItem("user-token") || "",
    status: "",
    messages: []
  },
  getters: {
    username(state) {
      return (state.profile && state.profile.displayName) || null;
    },
    profile(state) {
      return state.profile  || null;
    },
    messages(state) {
      return state.messages || null;
    },
    isAuthenticated: state => !!state.token
  },
  mutations: {
    setUser(state, payload) {
      state.user = payload;
    },
    setProfile(state, payload) {
      state.profile = payload;
    },
    auth_request(state) {
      state.status = "loading";
    },
    auth_success(state, payload) {
      state.status = "success";
      state.token = payload;
      localStorage.setItem("user-token", payload);
    },
    auth_logout(state) {
      localStorage.removeItem("user-token");
      state.profile = null;
    },
    auth_error(state) {
      state.status = "error";
    },
    set_messages(state, payload) {
      state.messages = payload;
    }
  },
  actions: {
    getUser: async function({ commit }) {
      const user = await api.get("/users");
      commit("setUser", user);
    },
    checkUser: async function({ commit }, { email, username }) {
      const response = await api
        .post("/users/check", {
          email,
          username
        })
        .catch(err => {
          console.log(err);
        });
      console.log(response);
      commit("set_messages", response.data.messages);
    },
    registerUser: async function({ commit }, { email, password, username }) {
      commit("auth_request");
      const response = await api
        .post("/users", {
          email,
          password,
          username
        })
        .catch(err => {
          console.log(err);
        });

      if (response.data.user) {
        localStorage.setItem("user-token", response.data.token);
        setToken(response.data.token);
        commit("setUser", response.data.user);
        commit("auth_success", response.data.token);
      }
    },
    updateProfile: async function({ commit }, { name, surname,displayname,birthdate}) {
      setToken(localStorage["user-token"]);
      
      const response = await api
        .put("users/profileFull", {
          name,
          surname,
          displayname,
          birthdate
        })
        .catch(err => {
          console.log(err);
        });
      if (response.data) {
        commit("setProfile", response.data.profilefull);
      }
    },
    loginUser: async function({ commit }, { email, password }) {
      commit("auth_request");
      const response = await api
        .post("/token", {
          email,
          password
        })
        .catch(err => {
          console.log(err);
          localStorage.removeItem("user-token");
        });
      if (response.data.token) {
        setToken(response.data.token);
        commit("auth_success", response.data.token);
      }
      if (response.data.profile) {
        commit("setProfile", response.data.profile);
      }
    },
    getUserInfo: async function({ commit }) {
      
      setToken(localStorage["user-token"]);
      const response = await api.get("users/userinfo/");
      commit("setUser", response.data);
      if (response.data.profile) {
        commit("setProfile", response.data.profile);
      }
    },
    logoutUser: async function({ commit }) {
      commit("auth_logout");
    },
    getProfile: async function({ commit }, username) {
      const response = await api.get("users/profile/" + username);
      if (response.data) {
        commit("setProfile", response.data);
      }
    },
    getFullProfile: async function({ commit }, username) {
      setToken(localStorage["user-token"]);
      const response = await api.get("users/profileFull/" + username);
      if (response.data) {
        commit("setProfile", response.data);
      }
    }
  }
};
