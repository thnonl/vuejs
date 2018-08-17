import ApiHanlder from "./api-handler"
import authHeader from '../helper/auth-header'

export default {
  async authenticate(user) {
    let data = JSON.stringify(user);
    return await fetch("/api/users/authenticate", {
      method: "post",
      headers: {'Content-Type': 'application/json'},
      body: data
    })
    .then(ApiHanlder.handleResponse, ApiHanlder.handleError)
    .then(user => {
      // login successful if there's a jwt token in the response
      if (user && user.token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('user', JSON.stringify(user));
      }

      return user;
  });
  },
  async register(user) {
    let data = JSON.stringify(user);
    return await fetch("/api/users/register", {
      method: "post",
      headers: {'Content-Type': 'application/json'},
      body: data
    }).then(ApiHanlder.handleResponse, ApiHanlder.handleError);
  },
  async getAll() {
    return await fetch("/api/users/", {
      method: "get",
      headers: authHeader()
    }).then(ApiHanlder.handleResponse, ApiHanlder.handleError);
  },
  async get(id) {
    return await fetch("/api/users/" + id, {
      method: "get",
      headers: authHeader()
    }).then(ApiHanlder.handleResponse, ApiHanlder.handleError);
  },
  async update(item) {
    let data = JSON.stringify(item);
    return await fetch("/api/users/update/" + item.id, {
      method: "post",
      headers: authHeader(),
      body: data.replace("id","")
    }).then(ApiHanlder.handleResponse, ApiHanlder.handleError);
  },
  async deleteUser(id) {
    return await fetch("/api/users/" + id, {
      method: "delete", 
      headers: authHeader() 
    }).then(ApiHanlder.handleResponse, ApiHanlder.handleError);
  }
};
