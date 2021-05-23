import axiosInstance from "./axiosInstance";

const facultyApi = {
  getFaculties() {
    return axiosInstance
      .get(`Faculties`)
      .then(({ data }) => data);
  },

  getFaculty(id) {
    return axiosInstance
      .get(`Faculties/${id}`)
      .then(({ data }) => data);
  },
};

export default facultyApi;
