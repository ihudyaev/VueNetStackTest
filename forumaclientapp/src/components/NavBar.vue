<template>
  <b-navbar type="light" variant="light">
    <b-navbar-nav class="h4 mb-1">
      <router-link class="nav-link" to="/">
       <b-icon icon="house-door-fill"  style="width: 60px; height: 60px;" variant="success"  href="#"></b-icon>
       </router-link>
      <b-nav-item >
        <router-link class="nav-link" to="/topiceditor">New Topic</router-link>
      </b-nav-item>
      <b-nav-item v-if="!username">
        <router-link class="nav-link" to="/register">Sign up</router-link>
      </b-nav-item>
      <b-nav-item v-if="!username">
        <router-link class="nav-link" to="/login">Sign in</router-link>
      </b-nav-item>
      <b-nav-item v-if="username">
        <router-link
            class="nav-link text-primary"
            :to="'/ProfileEditor/@' + username">
            {{ username }}
          </router-link>
      </b-nav-item>
      <b-nav-item>
        <button v-if="username"
              @click="logout"
              class="btn btn-lg btn-primary pull-xs-right">
              Logout
        </button>
      </b-nav-item>
      <b-nav-item >
        <button
              @click="refreshIndex"
              class="btn btn-lg btn-primary pull-xs-right">
              Refresh Index
        </button>
      </b-nav-item>
      <form class="form-inline my-2 my-lg-0">
          <input v-model="searchstring" class="form-control mr-sm-2" type="text" placeholder="Search" aria-label="Search">
          <button @click="search" class="btn btn-success  my-2 my-sm-0" :to="'/SearchResult/' + this.searchstring">Search</button>
        </form>
    </b-navbar-nav>
  </b-navbar>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name:"NavBar",
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
        this.$router.push({ name: 'searchresult', params: { searchstring: this.searchstring } })
        this.$router.go();
    }
  }
};
</script>
