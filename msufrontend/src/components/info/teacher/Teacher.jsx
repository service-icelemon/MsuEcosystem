import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import ReactHtmlParser from "react-html-parser";
import { fetchTeacher } from "../../../redux/actions/teachers";

function Teacher() {
  const dispatch = useDispatch();
  const {
    photoUrl,
    firstName,
    lastName,
    fatherName,
    biography,
    scienceDegree,
    departmentName,
    facultyName,
  } = useSelector(({ teachers }) => teachers.currentTeacher);
  const { id } = useParams();

  React.useEffect(() => {
    dispatch(fetchTeacher(id));
  }, []);
  return (
    <div>
      <img src={photoUrl} alt="фото преподавателя" />
      <h2>
        {lastName} {firstName} {fatherName}
      </h2>
      <div>
        <span>{scienceDegree}</span>
        <span>{departmentName}</span>
        <span>{facultyName}</span>
      </div>
      <p>{ReactHtmlParser(biography)}</p>
    </div>
  );
}

export default Teacher;
