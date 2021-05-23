const initialState = {
    currentTeacher: {},
    isLoaded: false,
  };
  
  const teachers = (state = initialState, action) => {
    switch (action.type) {
      case "SET_TEACHER":
        return {
          ...state,
          currentTeacher: action.payload,
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
  
  export default teachers;
  