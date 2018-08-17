<template>
  <div>
    <div class="form-group">
      <label for="lastname">Last name</label>
      <input type="text" id="lastname" class="form-control" placeholder="Enter last name" required v-model="newUser.lastName" />
    </div>
    <div class="form-group">
      <label for="firstname">First name</label>
      <input type="text" id="fitstname" class="form-control" placeholder="Enter first name" required v-model="newUser.firstName" />
    </div>
    <div class="form-group">
      <label for="username">Username</label>
      <input type="text" id="username" class="form-control" placeholder="Enter username" required v-model="newUser.username" />
    </div>
    <div class="form-group">
      <label for="password">Password</label>
      <input type="text" id="password" class="form-control" placeholder="Enter password" required v-model="newUser.password" />
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      isSignupData: false
    };
  },

  props: {
    newUser: {
      type: Object,
      required: false
    }
  },

  methods: {},

  created() {}
};
</script>
