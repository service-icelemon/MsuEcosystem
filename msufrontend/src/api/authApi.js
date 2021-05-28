import axiosInstance from "./axiosInstance";

const authApi = {
  login(email, password) {
    return axiosInstance
      .post(`User/gettoken`, { email, password })
      .then(({ data }) => data);
  },
  refreshToken() {
    return axiosInstance.post(`User/refreshtoken`).then(({ data }) => data);
  },
  loadUserData() {
    return axiosInstance.get(`User/loaduserdata`).then(({ data }) => data);
  },
  confirmEmail(token) {
    return axiosInstance
      .get(`User/verifyemail?code=${token}`)
      .then(({ data }) => data);
  },
  resetPassword(token, password) {
    return axiosInstance
      .get(`User/verifyemail?code=${token}?newPassword=${password}`)
      .then(({ data }) => data);
  },
  logout() {
    return axiosInstance.post(`User/logout`).then(({ data }) => data);
  },
  register(username, email, password, isTeacher, teacherCode, studentCard) {
    return axiosInstance
      .post(`User/register`, {
        userName: username,
        email: email,
        password: password,
        studentCardId: studentCard,
        isTeacher: isTeacher,
        teacherCode: teacherCode,
      })
      .then(({ data }) => data);
  },
  resetPassword(email) {
    return axiosInstance
      .post(`User/requestpasswordreset?email=${email}`)
      .then(({ data }) => data);
  },
};

export default authApi;
