<template>
  <form v-on:submit.prevent="onSubmit">
    <div class="form-group">
      <label for="title">Title</label>
      <input type="text" class="form-control" id="title" placeholder="Enter title" required v-model="currentProduct.title" />
    </div>
    <div class="form-group">
      <label for="description">Description</label>
      <textarea class="form-control" id="description" placeholder="Enter description" v-model="currentProduct.description"></textarea>
    </div>
    <div class="form-group">
      <label for="price">Price</label>
      <input type="number" class="form-control" id="price" placeholder="Enter price" v-model="currentProduct.price" />
    </div>
    <button v-bind:disabled="isProcessing" class="btn btn-primary" type="submit">Submit</button>
  </form>
</template>

<script>
export default {
  props: {
    currentProduct: {
      type: Object,
      required: false
    },
    isProcessing: {
      type: Boolean
    },
    onSubmit: {
      type: Function
    }
  }
};
</script>
