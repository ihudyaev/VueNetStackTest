<template>
  <div>
    <h1 class="col-12">{{ this.$route.params.searchstring }}</h1>
    <ul v-if="feed" class="list-group topic-previews mt-4">
      <SearchResultPreview
        v-for="sr in feed"
        :key="sr.id"
        :SearchResult="sr"
        class="list-group-item list-group-item-action mb-3"
      />
    </ul>
  </div>
</template>

<script>
import SearchResultPreview from "@/components/SearchResultPreview.vue";
import { mapGetters } from "vuex";

export default {
  name: "SearchResult",
  components: {
    SearchResultPreview
  },
  methods: {},
  setCategories() {},
  created() {
    this.$store.dispatch("search/getResults", {
      searchstring: this.$route.params.searchstring,
      size: 10
    });
  },
  computed: {
    ...mapGetters({
      feed: "search/getResults"
    })
  }
};
</script>
