<template>
  <div>
    <div class="form-group">
      <label for="username">Username</label>
      <input type="text" id="username" class="form-control" placeholder="Enter username" required v-model="authen.username" />
    </div>
    <div class="form-group">
      <label for="password">Password</label>
      <input type="password" id="password" class="form-control" placeholder="Enter password" required v-model="authen.password" />
    </div>
  </div>
</template>

<script>
export default {
  props: {
    authen: {
      type: Object,
      required: false
    }
  },

  methods: {}
};
</script>
