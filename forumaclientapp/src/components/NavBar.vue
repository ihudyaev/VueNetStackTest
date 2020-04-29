<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
    <router-link class="navbar-brand" to="/">
      Test Forum
    </router-link>
    <button
      class="navbar-toggler collapsed"
      type="button"
      data-toggle="collapse"
      data-target="#navbarColor02"
      aria-controls="navbarColor02"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="navbar-collapse collapse" id="navbarColor02" style="">
      <ul class="navbar-nav mr-auto">
        <li class="nav-item active">
          <router-link class="nav-link" to="/topiceditor"
            >New Topic</router-link
          >
        </li>
        <li class="nav-item" v-if="!username">
          <router-link class="nav-link" to="/register">Sign up</router-link>
        </li>
        <li class="nav-item" v-if="!username">
          <router-link class="nav-link" to="/login">Sign in</router-link>
        </li>
        <li class="nav-item" v-if="username">
          <router-link
            class="nav-link text-primary"
            :to="'/ProfileEditor/@' + username"
          >
            {{ username }}
          </router-link>
        </li>
        <li class="nav-item" v-if="username">
          <button
            v-if="username"
            @click="logout"
            class="btn btn-primary "
            style="margin: 0px 5px 0px 0px"
          >
            Logout
          </button>
        </li>
        <li class="nav-item">
          <button
            @click="refreshIndex"
            class="btn btn-primary mx-auto rounded"
            style="margin: 0px 5px 0px 0px"
          >
            Refresh Index
          </button>
        </li>
      </ul>
      <form class="form-inline my-2 my-lg-0">
        <input
          v-model="searchstring"
          class="form-control mr-sm-2"
          type="text"
          placeholder="Search"
          aria-label="Search"
        />

        <button
          @click="search"
          class="btn btn-success  my-2 my-sm-0"
          :to="'/SearchResult/' + this.searchstring"
        >
          Search
        </button>
      </form>
    </div>
  </nav>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "NavBar",
  data: function() {
    return {
      searchstring: ""
    };
  },
  computed: {
    ...mapGetters({
      username: "users/username"
    })
  },
  methods: {
    logout() {
      this.$store.dispatch("users/logoutUser");
    },
    refreshIndex() {
      this.$store.dispatch("search/refreshIndex");
    },
    search() {
      this.$router.push({
        name: "searchresult",
        params: { searchstring: this.searchstring }
      });
      this.$router.go();
    }
  }
};
</script>
