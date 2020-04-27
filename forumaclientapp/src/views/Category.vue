<template>
  <div v-if="crnCat">
      <h1 class="col-12">{{ crnCat.title }}</h1>
    <router-link class="btn btn-info" :to="'/'">
      Return To CategoryList
    </router-link>
    <ul v-if="hasTopics" class="list-group topic-previews mt-4">
      <TopicPreview
        v-for="topic in feed"
        :key="topic.id"
        :topic="topic"
        class="list-group-item list-group-item-action mb-3"
      />
    </ul>
  </div>
</template>

<script>
import TopicPreview from "@/components/TopicPreview.vue";
import { mapGetters } from "vuex";

export default {
  name: "Category",
  components: {
    TopicPreview
  },
  methods: {},
  setCategories() {},
  created() {
    this.$store.dispatch("topics/getTopics", this.$route.params.id);
    this.$store.dispatch("categories/getCrnCategory", this.$route.params.id);
  },
  computed: {
    ...mapGetters({
      feed: "topics/allTopics",
      crnCat: "categories/crnCategory",
      hasTopics: "topics/hasTopics"
    })
  }
};
</script>
