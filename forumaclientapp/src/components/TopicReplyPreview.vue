<template>
<div class="col-lg-12 col-md-12">
        <div class="post row  border border-primary">
            <div class="col-8">
                <div class=" posttext lead">
                    <p>{{ topicReply.description }}</p>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="col-4 postinfo pull-left border border-success">
                <div class="text-success"> {{ formatDate(topicReply.createdAt) }}</div>
                <div >Author:  
                  <router-link 
                    v-if="!topicReply.isAnonym"
                    class="link text-primary"
                    :to="'/Profile/@' + username">    
                    {{topicReply.userName}}
                  </router-link>
                  <span v-if="topicReply.isAnonym">{{topicReply.userName}}</span>
                </div>
                <div class="bg-gradient-success bg-success  p-1 mb-1 text-white rounded" v-if="!topicReply.isAnonym">Auth User</div>
            </div>
        <div class="clearfix"></div>
        </div>
  </div>
</template>

<script>
import moment from "moment";
export default {
  name:"TopicReplyPreview",
  props: ["topicReply"],
  methods: {
    formatDate(dateString) {
      return moment(dateString).format("MMMM Do, YYYY  h:mm:ss a");
    }
  }
};
</script>
