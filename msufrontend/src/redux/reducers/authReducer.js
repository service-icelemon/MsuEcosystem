const initialState = {
  isAuthenticated: false,
  user: {},
  accessToken: "",
};
function extractUserData(userData) {
  var user = !userData.isTeacher
    ? { ...userData.studentData }
    : { ...userData.teacherData };
  user.email = userData.email;
  user.accountId = userData.accountId;
  user.roles = userData.roles;
  user.isTeacher = userData.isTeacher;
  return user;
}
const auth = (state = initialState, action) => {
  switch (action.type) {
    case "SET_USER": {
      return {
        ...state,
        user: extractUserData(action.payload.user),
        accessToken: action.payload.accessToken,
        isAuthenticated: true,
      };
    }
    case "REFRESH_USERDATA": {
        return {
            ...state,
            user: extractUserData(action.payload),
            isAuthenticated: true
        };
    }
    case "REFRESH_TOKEN": {
        return {
            ...state,
            accessToken: action.payload,
            isAuthenticated: true
        };
    }
    case "RESET_USER": {
      return {
          initialState
      };
      
  }
    default:
      return state;
  }
};

export default auth;
