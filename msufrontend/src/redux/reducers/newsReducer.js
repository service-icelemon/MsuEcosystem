const initialState = {
    items: [],
    isLoaded: false,
  };
  
  const news = (state = initialState, action) => {
    switch (action.type) {
      case "SET_NEWS":
        return {
          ...state,
          items: action.payload,
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
  
  export default news;