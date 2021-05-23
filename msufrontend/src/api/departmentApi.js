import axiosInstance from "./axiosInstance";

const departmentApi = {
  getDepartmentByFaculty(facultyId) {
    return axiosInstance
      .get(`Departments/${facultyId}`)
      .then(({ data }) => {
        return data;
      });
  },
};

export default departmentApi;
