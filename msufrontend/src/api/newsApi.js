import axiosInstance from "./axiosInstance";

const newsApi = {
  getPosts() {
    return axiosInstance
      .get(`news/GetPublicationList`)
      .then(({ data }) => data);
  },

  getPost(id) {
    return axiosInstance
      .get(`https://localhost:44378/api/News/publications/${id}`)
      .then(({ data }) => data);
  },
};

export default newsApi;
