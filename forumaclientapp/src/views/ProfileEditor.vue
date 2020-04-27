<template>
<div class="profile-page" v-if="profile">
    <div v-if="profile">
          <ul class="error-messages" v-if="errors">
            <li v-for="error in errors" :key="error.message">{{ error }}</li>
          </ul>
          <form action="" method="post" @submit.prevent="submit">
            <fieldset class="form-group">
              Username
              <input
                v-model="profile.username"
                class="form-control form-control-lg"
                type="text"
                placeholder="Your UserName"
                disabled=true
              />
            </fieldset>
            <fieldset class="form-group">
              Email
              <input
                v-model="profile.email"
                class="form-control form-control-lg"
                type="text"
                placeholder="Your Email"
                disabled=true
              />
            </fieldset>
            <fieldset class="form-group">
              Name
              <input
                v-model="profile.name"
                class="form-control form-control-lg"
                type="text"
                placeholder="Your Name"
              />
            </fieldset>
            <fieldset class="form-group">
              Surname
              <input
                v-model="profile.surname"
                class="form-control form-control-lg"
                type="text"
                placeholder="Your surname"
              />
            </fieldset>
            <fieldset class="form-group">
              Birthdate
              <input
                v-model="profile.birthdate"
                class="form-control form-control-lg"
                type="Date"
                placeholder="Birthdate"
              />
            </fieldset>
            <fieldset class="form-group">
              Display Name
              <input
                v-model="profile.displayname"
                class="form-control form-control-lg"
                type="text"
                placeholder="Your display name"
              />
            </fieldset>
          </form>
    </div>
    <button
                class="btn btn-lg pull-xs-right btn-primary"
                @click="submit()"
                type="button"
              >
                Update Profile
              </button>
</div>

</template>


<script>
import { mapGetters } from "vuex";

export default {
  name:"ProfileEditor",
  data: function() {
    return {
      errors: []
    };
  },
  created() {
    this.$store.dispatch("users/getFullProfile", this.$route.params.username);
    this.crnProfile = this.$store.profile;
  },
  computed: {
    ...mapGetters({
      isAuth: "users/isAuthenticated",
      usernamestore: "users/username",
      profile: "users/profile"
    })
  },
  methods: {
    submit() {
      this.errors = [];
      if (this.errors.length == 0) {
        console.log(this.profile.name);
        this.$store
          .dispatch("users/updateProfile", {
            name: this.profile.name,
            surname: this.profile.surname,
            displayname: this.profile.displayname,
            birthdate: this.profile.birthdate
          });
      }
    }
  }
};
</script>