import authApi from "../../api/authApi";

export const login = (email, password) => (dispatch) => {
  authApi.login(email, password).then((data) => {
    dispatch(setUser(data));
  });
};

export const register =
  (username, email, password, isTeacher, teacherCode, studentCard) =>
  (dispatch) => {
    authApi
      .register(username, email, password, isTeacher, teacherCode, studentCard)
      .then((response) => {
        console.log(response);
      });
  };

  export const refreshToken = () => (dispatch) => {
    return authApi.refreshToken().then((data) => {
      dispatch(setToken(data));
      return data;
    });
  };


export const resetPassword = (email) => (dispatch) => {
  return authApi.resetPassword(email).then((data) => {
    console.log(data);
  });
};

export const loadUserdata = () => (dispatch) => {
  authApi.loadUserData().then((data) => {
    dispatch(setUserData(data));
  });
};

export const logout = () => (dispatch) => {
  authApi.logout().then((data) => {
    dispatch(resetUser(data));
  });
};

export const setToken = (item) => ({
  type: "REFRESH_TOKEN",
  payload: item,
});

export const setUserData = (item) => ({
  type: "REFRESH_USERDATA",
  payload: item,
});

export const setUser = (item) => ({
  type: "SET_USER",
  payload: item,
});

export const resetUser = () => ({
  type: "RESET_USER",
});
