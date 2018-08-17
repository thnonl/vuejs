<template>
  <div id="app" class="container-fluid">
    <div class="row authen-container" v-if="!isLoggedin">
      <div class="col-sm-12">
        <form v-on:submit.prevent="onSubmit()">
          <login-form v-if="!isSignup" :isSignup="isSignup" :authen="authen"></login-form>
          <signup-form v-else :isSignup="isSignup" :newUser="newUser"></signup-form>

          <button v-bind:disabled="isProcessing" class="btn btn-primary" type="submit">{{submitText}}</button>
          <button v-bind:disabled="isProcessing" class="btn btn-default" type="button" v-on:click.prevent="changeMode()">{{changeModeText}}</button>
        </form>
      </div>
    </div>
    <div class="row" v-else>
      <div class="col-md-3">
        <nav-menu params="route: route"></nav-menu>
      </div>
      <div class="col-sm-9">
        <router-view></router-view>
      </div>
    </div>
  </div>
</template>

<script>
import NavMenu from "./nav-menu";
import LoginForm from "./login-form";
import SignupForm from "./signup-form";
import UserApi from "../api/user-api";

export default {
  components: {
    "nav-menu": NavMenu,
    "login-form": LoginForm,
    "signup-form": SignupForm
  },

  computed: {
    submitText: function() {
      return !this.isSignup ? "Login" : "Save";
    },
    changeModeText: function() {
      return !this.isSignup ? "Sign up" : "Back";
    }
  },

  data() {
    return {
      isLoggedin: false,
      isSignup: false,
      authen: {},
      newUser: {},
      isProcessing: false,
      currentUser: {}
    };
  },

  methods: {
    changeMode() {
      this.isSignup = !this.isSignup;
    },
    async onSubmit() {
      this.isProcessing = true;
      try {
        if (this.isSignup) {
          UserApi.authenticate(this.authen).then(user => {
            this.isProcessing = false;
            this.currentUser = user;
            this.isLoggedin = true;
          });
        } else {
          UserApi.register(this.newUser).then(() => {
            this.isProcessing = false;
            this.isSignup = false;
          });
        }
      } catch (err) {
        window.alert(err);
      }
    }
  }
};
</script>

<style>
</style>
