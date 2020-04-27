import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home
  },
  {
    path: "/login",
    name: "login",
    component: () => import("@/views/Login.vue")
  },
  {
    path: "/register",
    name: "register",
    component: () => import("@/views/Register.vue")
  },
  {
    path: "/TopicEditor",
    name: "topiceditor",
    component: () => import("@/views/TopicEditor.vue")
  },
  {
    path: "/Category/:id",
    name: "category",
    component: () => import("@/views/Category.vue")
  },
  {
    path: "/Topic/:id",
    name: "topic",
    component: () => import("@/views/Topic.vue")
  },
  {
    path: "/Profile/:username",
    name: "profile",
    component: () => import("@/views/Profile.vue")
  },
  {
    path: "/ProfileEditor/:username",
    name: "profileeditor",
    component: () => import("@/views/ProfileEditor.vue")
  },
  {
    path: "/SearchResult/:searchstring",
    name: "searchresult",
    component: () => import("@/views/SearchResult.vue")
  }
];

const router = new VueRouter({
  routes
});

export default router;
