<template>
  <div class="postinfo col-4">
    <div class="comments">
      <div class="commentbg" v-if="repliescnt">
        Replies:
        <span
          class="bg-gradient-success bg-success  p-1 mb-1 text-white rounded"
          >{{ repliescnt }}</span
        >
      </div>
    </div>
    <div class="text-success" v-if="updatedAt">
      Update at: {{ formatDate(updatedAt) }}
    </div>
    <div class="text-success" v-if="createdAt">
      Create at: {{ formatDate(createdAt) }}
    </div>
    <div v-if="displayname">
      Author:
      <button v-if="!isAnonym" @click="showPopupInfo" class="btn btn-link">
        {{ displayname }}
      </button>
      <tpopup v-if="isPopupVisible" v-on:closePopup="closePopupInfo">
        <div>
          <p>Email: {{ this.profile.email }}</p>
          <p>Slogan: {{ this.profile.slogan }}</p>
          <p>Topic Count: {{ this.profile.topicCnt }}</p>
          <p>Reply Count: {{ this.profile.topicReplyCnt }}</p>
        </div>
        <span></span>
        <span></span>
      </tpopup>
      <span v-if="isAnonym">{{ displayname }}</span>
    </div>
    <div
      class="bg-gradient-success bg-success  p-1 mb-1 text-white rounded"
      v-if="!isAnonym"
    >
      Auth User
    </div>
  </div>
</template>

<script>
import moment from "moment";
import tpopup from "@/components/popup/tpopup.vue";
import { mapGetters } from "vuex";
export default {
  name: "Postinfo",
  components: {
    tpopup
  },
  data() {
    return {
      isPopupVisible: false
    };
  },
  props: {
    repliescnt: Number,
    displayname: String,
    updatedAt: String,
    createdAt: String,
    isAnonym: Boolean
  },
  methods: {
    showPopupInfo() {
      this.$store.dispatch("users/getProfile", this.displayname);
      this.isPopupVisible = true;
    },
    closePopupInfo() {
      this.isPopupVisible = false;
    },
    formatDate(dateString) {
      return moment(dateString).format("MMMM Do, YYYY  h:mm a");
    }
  },
  computed: {
    ...mapGetters({
      profile: "users/profile"
    })
  }
};
</script>
