<template>
<div class="profile-page">
    <div v-if="profile">
        <h4> 
          User: <strong> {{profile.displayName}} </strong>
        </h4>
        <p>
         <strong>Slogan: </strong> {{profile.slogan}}  
         </p>
         <p>
         <strong>Topic Count: </strong> {{profile.topicCnt}}  
         </p>
         <p>
         <strong>Topic Reply Count: </strong> {{profile.topicReplyCnt}}  
         </p>
    </div>
</div>
</template>


<script>
import { mapGetters } from "vuex";

export default {
  name: "Profile",
  components: {
  },
  methods: {},
  created() {
    
    this.$store.dispatch("users/getProfile", this.$route.params.username);
  },
  computed: {
    ...mapGetters({
      profile: "users/profile"
      
    })
  }
};
</script>