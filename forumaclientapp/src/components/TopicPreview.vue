<template>
  <div class="card border-info col-12 container">
    <div class="row">
      <div class="cardpost col-8">
        <div class="card-header">
          <router-link :to="'/Topic/' + topic.id">{{
            topic.title
          }}</router-link>
        </div>
        <div class="card-body">
          <h4 class="card-title">{{ topic.description }}</h4>
        </div>
      </div>
      <Postinfo
        v-bind:repliescnt="topic.topicReplyCnt"
        v-bind:displayname="topic.userName"
        v-bind:updatedAt="topic.updatedAt"
        v-bind:createdAt="topic.createdAt"
        v-bind:isAnonym="topic.isAnonym"
      >
      </Postinfo>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import Postinfo from "@/components/Postinfo.vue";

import moment from "moment";
export default {
  name: "TopicPreview",
  components: {
    Postinfo
  },
  props: ["topic"],
  computed: {
    ...mapGetters({
      username: "users/username"
    })
  },
  methods: {
    formatDate(dateString) {
      return moment(dateString).format("MMMM Do, YYYY  h:mm:ss a");
    }
  }
};
</script>
