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
            <button @click="login" class="btn btn-lg btn-primary pull-xs-right">
              Sign in
            </button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Login",
  data: function() {
    return {
      password: "",
      email: "",
      errors: [],
      regemail: /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,24}))$/,
      regusername: /^[a-z0-9_-]{3,16}$/
    };
  },
  methods: {
    login() {
      this.errors = [];
      // if (!this.email) {
      //   this.errors.push("Email is required");
      // } else {
      //   if (!this.regemail.test(this.email)) {
      //     this.errors.push("Email not valid form");
      //   }
      // }
      if (this.errors.length == 0) {
        this.$store
          .dispatch("users/loginUser", {
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
            this.$router.push({ path: "/" });
          });
      }
    }
  }
};
</script>
