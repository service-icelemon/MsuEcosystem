import axiosInstance from "./axiosInstance";

const facultyApi = {
  getFaculties() {
    return axiosInstance
      .get(`Faculties`)
      .then(({ data }) => data);
  },

  getFaculty(id) {
    return axiosInstance
      .get(`Faculties/60a6b34a8e490bdfcc8b1ca9`)
      .then(({ data }) => data);
  },
};

export default facultyApi;
