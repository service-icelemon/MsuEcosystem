import { combineReducers } from "redux";

import news from "./newsReducer";
import faculties from "./facultyReducer";
import departments from "./departmentReducer";
import teachers from './teacherReducer';
import specialities from './specialityReducer';

const rootReducer = combineReducers({
  news,
  faculties,
  teachers,
  departments,
  specialities
});

export default rootReducer;
