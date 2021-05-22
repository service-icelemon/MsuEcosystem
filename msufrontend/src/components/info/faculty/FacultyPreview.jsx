import React from "react";
import { Link } from "react-router-dom";

function FacultyPreview({ id, image, name }) {
  return (
    <div>
      <img src={image} alt="превью фото факультета" />
      <Link to={`/faculty/${id}`}>
        <h3>{name}</h3>
      </Link>
    </div>
  );
}

export default FacultyPreview;
