<template>
  <div>
    <h1>Users</h1>

    <p>This component demonstrates fetching data from the server.</p>

    <div v-if="!users" class="text-center">
      <p>
        <em>Loading...</em>
      </p>
      <h1>
        <icon icon="spinner" pulse/>
      </h1>
    </div>

    <template v-else>
      <table class="table">
        <thead class="bg-dark text-white">
          <tr>
            <th>User Id</th>
            <th>Last Name</th>
            <th>First Name</th>
            <th>Username</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          <tr :class="index % 2 == 0 ? 'bg-white' : 'bg-light'" v-for="(user, index) in users" :key="index">
            <td class="user-id">
              <a href="javascript:void(0)">{{ user.id }}</a>
            </td>
            <td>{{ user.lastName }}</td>
            <td>{{ user.firstName }}</td>
            <td>{{ user.username }}</td>
            <td>
              <button class='btn btn-danger' :id="user.id" @click.prevent="deleteUser">Delete</button>
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
import UserApi from "../api/user-api";

export default {
  components: {},

  computed: {},

  data() {
    return {
      users: null,
      total: 0,
      pageSize: 5,
      currentPage: 1,
      currentUser: JSON.parse(localStorage.getItem("user"))
    };
  },

  methods: {
    async loadPage() {
      try {
        let response = await UserApi.getAll();
        this.users = response;
      } catch (err) {
        window.alert(err);
        console.log(err);
      }
    },
    async deleteUser(event) {
      let button = event.target;
      button.disabled = true;
      let id = button.id;
      if (this.currentUser.id !== id) {
        UserApi.deleteUser(id).then(() => {
          this.loadPage();
          button.disabled = false;
        });
      } else {
        window.alert(`Can't delete current logged in user`);
        button.disabled = false;
      }
    }
  },

  async created() {
    this.loadPage();
  }
};
</script>

<style>
</style>
