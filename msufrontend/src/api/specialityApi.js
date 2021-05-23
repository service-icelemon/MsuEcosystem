import axiosInstance from "./axiosInstance";

const specialityApi = {
  getSpecialities(departmentId) {
    return axiosInstance
      .get(`Specialities/departmentId?departmentId=${departmentId}`)
      .then(({ data }) => data);
  },

  getSpeciality(id) {
    return axiosInstance
      .get(`Specialities?specialityId=${id}`)
      .then(({ data }) => data);
  },
};

export default specialityApi;
