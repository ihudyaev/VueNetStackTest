<template>
 <div class="col-lg-12 col-md-12">
        <div class="post row">
            <div class="wrap-ut pull-left col-lg-8 col-md-8">
                <div class="userinfo pull-left">
                </div>
                <div class="posttext pull-left">
                    <h3><router-link class="nav-link text-success " :to="'/Topic/' + topic.id">{{ topic.title }}</router-link></h3> 
                    <p>{{ topic.description }}</p>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="postinfo pull-left">
                <div class="comments">
                    <div class="commentbg">
                        Replies: <span class="bg-gradient-success bg-success  p-1 mb-1 text-white rounded">{{ topic.topicReplyCnt }}</span>
                    </div>
                </div>
                <div class="text-success"> Update at: {{ formatDate(topic.updatedAt) }}</div>
                <div class="text-success"> Create at: {{ formatDate(topic.createdAt) }}</div>
                <div >Author:  
                  <router-link 
                    v-if="!topic.isAnonym"
                    class="link text-primary"
                    :to="'/Profile/@' + username">    
                    {{topic.userName}}
                  </router-link>
                  <span v-if="topic.isAnonym">{{topic.userName}}</span>
                </div>
                <div class="bg-gradient-success bg-success  p-1 mb-1 text-white rounded" v-if="!topic.isAnonym">Auth User</div>
            </div>
        <div class="clearfix"></div>
        </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";

import moment from "moment";
export default {
  name:"TopicPreview",
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
