<template>
  <div class="comment-box col-8" id="comment">
    <div class="loader" v-show="loading">
      <span class="spinner"></span>

      <ul class="error-messages" v-if="errors">
        <li v-for="error in errors" :key="error.message">{{ error }}</li>
      </ul>
    </div>

    <form action="" method="post" @submit.prevent="submit">
      <fieldset class="form-group">
        <p>Enter your comment:</p>
        <textarea
          v-model="comment"
          class="form-control form-control-sm"
          type="textrea"
          placeholder="Type your comment..."
          rows="3"
          required
        />
      </fieldset>
      <fieldset class="form-group" v-if="!usernamestore">
        <p>Your user name:</p>
        <input
          v-model="username"
          class="form-control form-control-sm"
          type="text"
          placeholder="User Name"
        />
      </fieldset>
      <button
        @click="submit"
        class="btn btn-lg btn-primary pull-xs-right"
        :disabled="loading"
        type="submit"
      >
        Reply
      </button>
    </form>
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name:"AddTopicReplyForm",
  data() {
    return {
      loading: false,
      regusername: /^[a-z0-9_-]{3,16}$/,
      errors: []
    };
  },
  methods: {
    submit() {
      console.log("start Submit");
      this.errors = [];
      if (!this.username && !this.usernamestore) {
        this.errors.push("Username is required");
      } 
        if (!this.regusername.test(this.username) && !this.usernamestore) {
          this.errors.push(
            "Username not valid form( a-z and 0-9 values allow, 3-16 symbols)"
          );
      }

      if (this.errors.length == 0) {
        this.loading = true;
        var usernameparameter = this.usernamestore  == null
          ? this.username
          :  this.usernamestore;
          console.log(usernameparameter);
        this.$store
          .dispatch("topicreplies/createTopicReply", {
            username: usernameparameter,
            description: this.comment,
            topicid: this.crnTop.id,
            isAnonym: !this.isAuth
          })
          .then(() => {
            this.comment = "";
            this.username = "";
          });
          
      }
    }
  },
  computed: {
    ...mapGetters({
      crnTop: "topics/crnTopic",
      isAuth: "users/isAuthenticated",
      usernamestore: "users/username"
    }),
    comment: {
      //геттер
      get: function() {
        return localStorage["comment"];
      },
      // сеттер:
      set: function(newValue) {
        localStorage["comment"] = newValue;
        this.description = newValue;
      }
    },
    username: {
      //геттер
      get: function() {
        return localStorage["username"];
      },
      // сеттер:
      set: function(newValue) {
          localStorage["username"] = newValue;
      }
    }
  }
};
</script>
