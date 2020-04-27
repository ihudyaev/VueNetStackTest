<template>  
  <div class="col-lg-12 col-md-12">
        <div class="post row">
            <div class="wrap-ut pull-left col-lg-8 col-md-8">
                <div class="userinfo pull-left">
                </div>
                <div class="posttext pull-left">
                    <h2><router-link class="nav-link text-success " :to="'/Category/' + category.id">{{ category.title }}</router-link></h2> 
                    <p>{{ category.description }}</p>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="postinfo pull-left">
                <div class="comments">
                    <div class="commentbg">
                        Topics: <span class="bg-gradient-success bg-success rounded  p-2 mb-1 text-white">{{ category.topicCount }}</span>
                    </div>
                </div>
                <div class="text-success"> {{ formatDate(category.lastNewTopic) }}</div>   
            </div>
        <div class="clearfix"></div>
        </div>
  </div>
</template>

<script>
import moment from "moment";
export default {
  name:"CategoryPreview",
  props: ["category"],
  methods: {
    formatDate(dateString) {
      return moment(dateString).format("MMMM Do, YYYY  h:mm:ss a");
    }
  }
};
</script>
