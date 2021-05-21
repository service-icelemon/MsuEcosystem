const initialState = {
  posts: [],
  currentPost: {},
  isLoaded: false,
};

const news = (state = initialState, action) => {
  switch (action.type) {
    case "SET_POSTS":
      return {
        ...state,
        posts: action.payload,
        isLoaded: true,
      };
    case "SET_POST":
      return {
        ...state,
        currentPost: action.payload,
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
