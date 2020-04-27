<template>
  <div class="Home">
      <CategoryPreview
        v-for="category in feed"
        :key="category.id"
        :category="category"
        class="list-group-item list-group-item-action mb-3"
      />
  </div>
</template>

<script>
import CategoryPreview from "@/components/CategoryPreview.vue";
//import categories from "@/store/modules/categories";
import { mapGetters } from "vuex";

export default {
  name: "Home",
  components: {
    CategoryPreview
  },
  methods: {},
  setCategories() {},
  created() {
    this.$store.dispatch("categories/getCategories");
  },
  computed: {
    ...mapGetters({
      feed: "categories/allCategories"
    })
  }
};
</script>
