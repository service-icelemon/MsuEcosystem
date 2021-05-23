const initialState = {
    currentSpeciality: {},
    isLoaded: false,
  };
  
  const specialities = (state = initialState, action) => {
    switch (action.type) {
      case "SET_SPECIALITY":
        return {
          ...state,
          currentSpeciality: action.payload,
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
  
  export default specialities;
  