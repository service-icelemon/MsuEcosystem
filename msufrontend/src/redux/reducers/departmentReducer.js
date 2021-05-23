const initialState = {
  currentDepartment: {},
  isLoaded: false,
};

const departments = (state = initialState, action) => {
  switch (action.type) {
    case "SET_DEPARTMENT":
      return {
        ...state,
        currentDepartment: action.payload,
        isLoaded: true,
      };
    case "SET_LOADED":
      return {
        ...state,
        isLoaded: action.payload,
      };
    default:
      return state;
  }
};

export default departments;
