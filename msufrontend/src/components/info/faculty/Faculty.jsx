import React from "react";
import { useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { fetchFaculty } from "../../../redux/actions/faculties";
import ReactHtmlParser from "react-html-parser";

function Faculty() {
  const dispatch = useDispatch();
  const { name, imageUrl, description, dean, departments } = useSelector(
    ({ faculties }) => faculties.currentFaculty
  );
  const { id } = useParams();

  React.useEffect(() => {
    dispatch(fetchFaculty(id));
  }, []);

  return (
    <div>
      <img src={imageUrl} alt="эмблема факультета" />
      <h2>факультет {name}</h2>
      <div>
          Декан: {dean.firstName}
      </div>
      <p>{ReactHtmlParser(description)}</p>
    </div>
  );
}

export default Faculty;
