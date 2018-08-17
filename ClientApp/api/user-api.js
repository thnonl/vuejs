import Vue from "vue";

export default {
  async authenticate(user) {
    let data = JSON.stringify(user);
    return await fetch("/api/users/authenticate", {
      method: "post",
      headers: {'Content-Type': 'application/json'},
      body: data
    });
  },
  async register(user) {
    let data = JSON.stringify(user);
    return await fetch("/api/users/register", {
      method: "post",
      headers: {'Content-Type': 'application/json'},
      body: data
    });
  },
  async getAll() {
    return await Vue.prototype.$http.get(`/api/users/`);
  },
  async get(id) {
    return await Vue.prototype.$http.get(`/api/users/` + id);
  },
  async update(item) {
    let data = JSON.stringify(item);
    return await fetch("/api/users/update/" + item.id, {
      method: "post",
      headers: {'Content-Type': 'application/json'},
      body: data.replace("id","")
    });
  },
  async delete(id) {
    return await fetch("/api/users/" + id, { method: "delete" });
  }
};
