import ApiHanlder from "./api-handler"
import authHeader from '../helper/auth-header'

export default {
  async getAll() {
    return await fetch("/api/products/", {
      method: "get",
      headers: authHeader()
    }).then(ApiHanlder.handleResponse, ApiHanlder.handleError);
  },
  async get(id) {
    return await fetch("/api/products/" + id, {
      method: "get",
      headers: authHeader()
    }).then(ApiHanlder.handleResponse, ApiHanlder.handleError);
  },
  async create(item) {
    let data = JSON.stringify(item);
    return await fetch("/api/products/", {
      method: "get",
      headers: authHeader(),
      body: data
    }).then(ApiHanlder.handleResponse, ApiHanlder.handleError);
  },
  async update(item) {
    let data = JSON.stringify(item);
    console.log(data);
    return await fetch("/api/products/update/" + item.id, {
      method: "post",
      headers: authHeader(),
      body: data.replace("id","")
    }).then(ApiHanlder.handleResponse, ApiHanlder.handleError);
  },
  async delete(id) {
    return await fetch("/api/products/" + id, { 
      method: "delete",
      headers: authHeader()
    }).then(ApiHanlder.handleResponse, ApiHanlder.handleError);
  }
};
