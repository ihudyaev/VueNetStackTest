<template>
  <div class="card border-info col-12 container">
    <div class="row">
      <div class="cardpost col-8">
        <div class="card-body">
          <h4 class="card-title">{{ topicReply.description }}</h4>
        </div>
      </div>
      <Postinfo
        v-bind:displayname="topicReply.userName"
        v-bind:updatedAt="topicReply.updatedAt"
        v-bind:createdAt="topicReply.createdAt"
        v-bind:isAnonym="topicReply.isAnonym"
      >
      </Postinfo>
    </div>
  </div>
</template>

<script>
import moment from "moment";
import Postinfo from "@/components/Postinfo.vue";

export default {
  name: "TopicReplyPreview",
  components: {
    Postinfo
  },
  props: ["topicReply"],
  methods: {
    formatDate(dateString) {
      return moment(dateString).format("MMMM Do, YYYY  h:mm:ss a");
    }
  }
};
</script>
