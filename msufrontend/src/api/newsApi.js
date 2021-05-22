import axiosInstance from "./axiosInstance";

const newsApi = {
  getPosts() {
    return axiosInstance
      .get(`news/GetPublicationList`)
      .then(({ data }) => data);
  },

  getPost(id) {
    return axiosInstance
      .get(`news/publications/${id}`)
      .then(({ data }) => data);
  },
};

export default newsApi;
