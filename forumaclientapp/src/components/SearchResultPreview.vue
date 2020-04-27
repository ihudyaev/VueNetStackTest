<template>  
  <div class="col-lg-12 col-md-12 d-flex flex-row">
    <div class=" p-2"> {{SearchResult.type}}</div>
        <div class=" p-2" v-if="SearchResult.type!='TopicReply'">
            <div class="">
                <div class="posttext pull-left" >
                    <h2><router-link class="nav-link text-success " :to="'/'+ SearchResult.type + '/' + SearchResult.id">{{ SearchResult.title }}</router-link></h2> 
                    <p>{{ SearchResult.description }}</p>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
        <div class=" p-2" v-else>
            <div class="">
                <div class="posttext pull-left" >
                    <h2><router-link class="nav-link text-info " :to="'/Topic/' + SearchResult.Rootid">To topic</router-link></h2> 
                    <p>{{ SearchResult.description }}</p>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
  </div>
</template>
<script>

export default {
  name:"SearchResultPreview",
  props: ["SearchResult"]
};
</script>
