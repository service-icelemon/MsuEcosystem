import axiosInstance from "./axiosInstance";

const teacherApi = {
  getTeacher(id) {
    return axiosInstance
      .get(`Teachers/${id}`)
      .then(({ data }) => {
        console.log(data);
        return data;
      });
  },
};

export default teacherApi;
