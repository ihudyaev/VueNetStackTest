<template>
  <div class="editor-page">
    <div class="container page">
      <div class="row">
        <div class="col-md-10 offset-md-1 col-xs-12">
          <form>
            <ul class="error-messages" v-if="errors">
              <li v-for="error in errors" :key="error.message">{{ error }}</li>
            </ul>
            <fieldset>
              <fieldset class="form-group">
                <input
                  type="text"
                  v-model="title"
                  class="form-control form-control-lg"
                  placeholder="Topic Title"
                  required
                />
              </fieldset>
              <fieldset class="form-group">
                <input
                  type="text"
                  v-model="description"
                  class="form-control"
                  placeholder="What's this topic about?"
                  required
                />
              </fieldset>
              <fieldset class="form-group" v-if="!isAuth">
                <input
                  v-model="username"
                  type="text"
                  class="form-control"
                  placeholder="Enter User Name"
                />
              </fieldset>
              <fieldset class="form-group">
                <select v-model="crncategory" class="form-control">
                  <option
                    v-for="categ in feed"
                    v-bind:key="categ.Id"
                    placeholder="ChooseCategory"
                    required
                  >
                    {{ categ.title }}
                  </option>
                </select>
              </fieldset>
              <button
                class="btn btn-lg pull-xs-right btn-primary"
                @click="create()"
                type="button"
              >
                Publish Topic
              </button>
            </fieldset>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name:"TopicEditor",
  data: function() {
    return {
      title: "",
      description: "",
      username: "",
      crncategory: "",
      errors: [],
      regtitle: /^\s*(?:\S\s*){10,150}$/,
      regdescription: /^\s*(?:\S\s*){10,250}$/
    };
  },
  created() {
    this.$store.dispatch("categories/getCategories");
  },
  computed: {
    ...mapGetters({
      feed: "categories/allCategories",
      crnTop: "topics/crnTopic",
      isAuth: "users/isAuthenticated",
      usernamestore: "users/username"
    })
  },
  methods: {
    create() {
      this.errors = [];
      if (!this.title) {
        this.errors.push("Title is required");
      } else {
        if (!this.regtitle.test(this.title)) {
          this.errors.push(
            "Title not valid form( A-z and 0-9 values allow, 10-150 of nonwhitespaced characters)"
          );
        }
      }
      if (!this.isAuth) {
        if (!this.username) {
          this.errors.push("Username is required");
        }
      }
      if (!this.description) {
        this.errors.push("Description is required");
      } else {
        if (!this.regdescription.test(this.description)) {
          this.errors.push(
            "Description not valid: length 10-250 of nonwhitespaced characters)"
          );
        }
      }
      if (!this.crncategory) {
        this.errors.push("Category is required");
      }

      if (this.errors.length == 0) {
        var usernameparameter = this.isAuth
          ? "a" //this.usernamestore
          : this.username;
        this.$store
          .dispatch("topics/createTopic", {
            title: this.title,
            description: this.description,
            username: usernameparameter,
            category: this.crncategory
          })
          .then(() => {
            console.log("New Category Id =" + this.crnTop.id);
            this.$router.push("/topic/" + this.crnTop.id);
          });
      }
    }
  }
};
</script>
