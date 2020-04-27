<template>
  <div class="auth-page">
    <div class="container page">
      <div class="row">
        <div class="col-md-6 offset-md-3 col-xs-12">
          <h1 class="text-xs-center">Sign up</h1>
          <p class="text-xs-center">
            <a href="">Have an account?</a>
          </p>

          <ul class="error-messages" v-if="errors">
            <li v-for="error in errors" :key="error.message">{{ error }}</li>
          </ul>

          <form>
            <fieldset class="form-group">
              <input
                v-model="username"
                class="form-control form-control-lg"
                type="text"
                placeholder="Your Name"
                required
              />
            </fieldset>
            <fieldset class="form-group">
              <input
                v-model="email"
                class="form-control form-control-lg"
                type="text"
                placeholder="Email"
                required
              />
            </fieldset>
            <fieldset class="form-group">
              <input
                v-model="password"
                class="form-control form-control-lg"
                type="password"
                placeholder="Password"
                required
              />
            </fieldset>
            <button
              @click="register"
              class="btn btn-lg btn-primary pull-xs-right"
            >
              Sign up
            </button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name:"Register",
  data: function() {
    return {
      username: "",
      email: "",
      password: "",
      errors: [],
      regemail: /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,24}))$/,
      regusername: /^[a-z0-9_-]{3,16}$/,
      passwordrules: [
        {
          message: "Password: One lowercase letter required.",
          regex: /[a-z]+/
        },
        {
          message: "Password: One uppercase letter required.",
          regex: /[A-Z]+/
        },
        { message: "Password: 8 characters minimum.", regex: /.{8,}/ },
        { message: "Password: One number required.", regex: /[0-9]+/ }
      ]
    };
  },
  computed: {
    ...mapGetters({
      messages: "users/messages"
    })
  },
  methods: {
    register() {
      this.errors = [];
      if (!this.username) {
        this.errors.push("Username is required");
      } else {
        if (!this.regusername.test(this.username)) {
          this.errors.push(
            "Username not valid form( a-z and 0-9 values allow, 3-16 symbols)"
          );
        }
      }
      if (!this.email) {
        this.errors.push("Email is required");
      } else {
        if (!this.regemail.test(this.email)) {
          this.errors.push("Email not valid form");
        }
      }
      if (!this.password) {
        this.errors.push("Password is required");
      } else {
        for (let condition of this.passwordrules) {
          if (!condition.regex.test(this.password)) {
            this.errors.push(condition.message);
          }
        }
      }
      //Для заведения тестовых пользователей
      //this.errors = [];
      if (this.errors.length == 0) {
        this.$store
          .dispatch("users/checkUser", {
            username: this.username,
            email: this.email
          })
          .then(() => {
            if (this.messages.length == 0) {
              this.$store
                .dispatch("users/registerUser", {
                  username: this.username,
                  email: this.email,
                  password: this.password
                })
                .then(() => {
                  this.errors = [];
                })
                .catch(err => {
                  this.errors.push(err);
                })
                .then(() => {
                  this.$router.push({ path: "/login" });
                });
            } else {
              for (let msg of this.messages) this.errors.push(msg);
            }
          });
      }
    }
  }
};
</script>
