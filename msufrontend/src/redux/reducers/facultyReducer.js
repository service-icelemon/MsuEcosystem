const initialState = {
    faculties: [],
    currentFaculty: {},
    isLoaded: false,
  };
  
  const faculties = (state = initialState, action) => {
    switch (action.type) {
      case "SET_FACULTIES":
        return {
          ...state,
          faculties: action.payload,
          isLoaded: true,
        };
      case "SET_FACULTY":
        return {
          ...state,
          currentFaculty: action.payload,
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
  
  export default faculties;
  