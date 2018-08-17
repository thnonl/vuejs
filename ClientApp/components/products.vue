<template>
  <div>
    <h1>Products</h1>

    <p>This component demonstrates fetching data from the server.</p>

    <div id='product-form'>
      <product-form :currentProduct="currentProduct" :isProcessing="isProcessing" :onSubmit="submit"></product-form>
    </div>

    <div v-if="!products" class="text-center">
      <p>
        <em>Loading...</em>
      </p>
      <h1>
        <icon icon="spinner" pulse/>
      </h1>
    </div>

    <template v-if="products">
      <table class="table">
        <thead class="bg-dark text-white">
          <tr>
            <th>Product Id</th>
            <th>Title</th>
            <th>Description</th>
            <th>Price</th>
            <th>Created On</th>
            <th>Updated At</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          <tr :class="index % 2 == 0 ? 'bg-white' : 'bg-light'" v-for="(product, index) in products" :key="index">
            <td class="product-id">
              <a href="javascript:void(0)" @click.prevent="getProduct">{{ product.id }}</a>
            </td>
            <td>{{ product.title }}</td>
            <td>{{ product.description }}</td>
            <td>{{ product.price }}</td>
            <td>{{ product.createdOn }}</td>
            <td>{{ product.updatedAt }}</td>
            <td>
              <button class='btn btn-danger' :id="product.id" @click.prevent="deleteProduct">Delete</button>
            </td>
          </tr>
        </tbody>
      </table>
      <!-- <nav aria-label="...">
                <ul class="pagination justify-content-center">
                    <li :class="'page-item' + (currentPage == 1 ? ' disabled' : '')">
                        <a class="page-link" href="#" tabindex="-1" @click="loadPage(currentPage - 1)">Previous</a>
                    </li>
                    <li :class="'page-item' + (n == currentPage ? ' active' : '')" v-for="(n, index) in totalPages" :key="index">
                        <a class="page-link" href="#" @click="loadPage(n)">{{n}}</a>
                    </li>
                    <li :class="'page-item' + (currentPage < totalPages ? '' : ' disabled')">
                        <a class="page-link" href="#" @click="loadPage(currentPage + 1)">Next</a>
                    </li>
                </ul>
            </nav> -->
    </template>
  </div>
</template>

<script>
import Vue from "vue";
import ProductApi from "../api/product-api";
import ProductForm from "./product-form";
import productApi from "../api/product-api";

export default {
  components: {
    "product-form": ProductForm
  },

  computed: {},

  data() {
    return {
      currentProduct: {},
      isProcessing: false,
      products: null,
      total: 0,
      pageSize: 5,
      currentPage: 1
    };
  },

  methods: {
    async loadPage() {
      try {
        let response = await ProductApi.getAll();
        this.products = response.data;
      } catch (err) {
        window.alert(err);
        console.log(err);
      }
    },
    async getProduct(event) {
      this.isProcessing = true;
      let td = event.target;
      let id = td.text;
      let response = await ProductApi.get(id);
      this.currentProduct = response.data;
      this.isProcessing = false;
    },
    async submit(event) {
      this.isProcessing = true;
      if (Object.keys(this.currentProduct).length !== 0) {
        if (!this.currentProduct.id) {
          ProductApi.create(this.currentProduct).then(() => {
            this.currentProduct = {};
            this.loadPage();
            this.isProcessing = false;
          });
        } else {
          ProductApi.update(this.currentProduct).then(() => {
            this.currentProduct = {};
            this.loadPage();
            this.isProcessing = false;
          });
        }
      } else this.isProcessing = false;
    },
    async deleteProduct(event) {
      let button = event.target;
      button.disabled = true;
      let id = button.id;
      ProductApi.delete(id).then(() => {
        this.loadPage();
        button.disabled = false;
      });
    }
  },

  async created() {
    this.loadPage();
  }
};
</script>

<style>
#product-form {
  width: 400px;
  margin: 20px 0;
  padding: 15px;
  border: 0.5px solid #ccc;
}
</style>
