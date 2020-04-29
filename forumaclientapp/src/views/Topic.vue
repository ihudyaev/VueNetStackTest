<template>
  <div class="constainer featurebox col-12">
    <div class="navbuttons pull-left row" v-if="crnTopic">
      <router-link class="btn btn-info border rounded" :to="'/'">
        Return To Category List
      </router-link>
      <router-link
        class="btn btn-primary border rounded"
        :to="'/Category/' + crnTopic.categoryId"
      >
        Return To Category
      </router-link>
    </div>
    <div class="topic-main" v-if="crnTopic">
      <div class="border border-info">
        <h1 class="">{{ crnTopic.title }}</h1>
        <div class="topic-post row">
          <div class="topic-meta col-8">
            {{ formatDate(crnTopic.createdAt) }}
          </div>
          <div class="col-8">
            Author:
            <router-link
              class=".stretched-link"
              :to="'/Profile/@' + crnTopic.UserName"
            >
              {{ crnTopic.userName }}
            </router-link>
          </div>
        </div>
        <h4 class="col-8 ">
          {{ crnTopic.description }}
        </h4>
      </div>
      <TopicReplyPreview
        v-for="topicReply in feed"
        :topicReply="topicReply"
        :key="topicReply.id"
      />
    </div>
    <AddTopicReplyForm />
  </div>
</template>

<script>
import TopicReplyPreview from "@/components/TopicReplyPreview.vue";
import AddTopicReplyForm from "@/components/AddTopicReplyForm.vue";
import { mapGetters } from "vuex";
import moment from "moment";

export default {
  name: "Topic",
  components: {
    TopicReplyPreview,
    AddTopicReplyForm
  },
  methods: {
    formatDate(dateString) {
      return moment(dateString).format("MMMM Do, YYYY  h:mm:ss a");
    },
    setTopicReplies() {}
  },
  created() {
    this.$store.dispatch("topicreplies/getTopicReplies", this.$route.params.id);
    this.$store.dispatch("topics/getCrnTopic", this.$route.params.id);
  },
  computed: {
    ...mapGetters({
      feed: "topicreplies/allTopicReplies",
      crnTopic: "topics/crnTopic",
      hasReplies: "topicreplies/hasReplies"
    })
  }
};
</script>
