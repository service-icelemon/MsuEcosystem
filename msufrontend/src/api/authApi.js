import axiosInstance from "./axiosInstance";

const authApi = {
  login(email, password) {
    return axiosInstance
      .post(`User/gettoken`, { email, password })
      .then(({ data }) => data);
  },
  refreshToken() {
    return axiosInstance
      .post(`User/refreshtoken`)
      .then(({ data }) => data);
  },
  loadUserData() {
    return axiosInstance
      .get(`User/loaduserdata`)
      .then(({ data }) => data);
  }
};

export default authApi;
