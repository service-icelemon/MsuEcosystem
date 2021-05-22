
import { combineReducers } from "redux";

import news from "./newsReducer";
import faculties from "./facultyReducer";

const rootReducer = combineReducers({
    news,
    faculties
});

export default rootReducer;