import Vue from "vue";

export default {
  async getAll() {
    return await Vue.prototype.$http.get(`/api/products/`);
  },
  async get(id) {
    return await Vue.prototype.$http.get(`/api/products/` + id);
  },
  async create(item) {
    let data = JSON.stringify(item);
    return await fetch("/api/products/", {
      method: "post",
      headers: {'Content-Type': 'application/json'},
      body: data
    });
  },
  async update(item) {
    let data = JSON.stringify(item);
    console.log(data);
    return await fetch("/api/products/update/" + item.id, {
      method: "post",
      headers: {'Content-Type': 'application/json'},
      body: data.replace("id","")
    });
  },
  async delete(id) {
    return await fetch("/api/products/" + id, { method: "delete" });
  }
};
