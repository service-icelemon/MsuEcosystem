import React from "react";
import {Link} from 'react-router-dom';

function SpecialityPreview({ id, name, image }) {
  return (
    <div>
        <Link to={`/faculty/department/speciality/${id}`}>
            <img src={image} alt="фото специальности"/>
            <h2>{name}</h2>
        </Link>
    </div>
  );
}

export default SpecialityPreview;
