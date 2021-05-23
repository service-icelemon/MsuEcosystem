import React from "react";
import { Link } from "react-router-dom";

function DepartmentPreview({ id, image, name }) {
  return (
    <div>
      <img src={image} alt="эмблема кафедры" />
      <Link to={`department/${id}`}>
        <h2>{name}</h2>
      </Link>
    </div>
  );
}

export default DepartmentPreview;
