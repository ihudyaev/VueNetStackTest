<template>
  <div id="app">
    <NavBar> </NavBar>
    <main role="main" class="container mt-4">
      <router-view> </router-view>
    </main>
  </div>
</template>

<script>
import NavBar from "@/components/NavBar.vue";
import axios from "axios";

export default {
  components: {
    NavBar
  },
  created: function() {
    this.$store.dispatch("users/getUserInfo");
    axios.interceptors.response.use(undefined, function(err) {
      if (err.status === 401 && err.config && !err.config.__isRetryRequest) {
        this.$store.dispatch("auth_logout");
      }
      throw err;
    });
  }
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
