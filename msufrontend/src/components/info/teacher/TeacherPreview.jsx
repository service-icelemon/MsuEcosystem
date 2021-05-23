import React from "react";
import { Link } from "react-router-dom";

function TeacherPreview({
  id,
  photoUrl,
  firstName,
  lastName,
  fatherName,
  scienceDegree,
}) {
  return (
    <div>
      <img src={photoUrl} alt="фото преподавателя" />
      <Link to={`/faculty/department/teacher/${id}`}>
        <h2>
          {lastName} {firstName} {fatherName}
        </h2>
        <span>{scienceDegree}</span>
      </Link>
    </div>
  );
}

export default TeacherPreview;
