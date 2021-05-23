import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import { fetchSpeciality } from "../../../redux/actions/specialities";
import ReactHtmlParser from "react-html-parser";

function Speciality() {
  const dispatch = useDispatch();
  const {
    name,
    imageUrl,
    description,
    budgetScores,
    teachers,
    paidScores,
    subjects,
    educationForms,
  } = useSelector(({ specialities }) => specialities.currentSpeciality);
  const { id } = useParams();

  React.useEffect(() => {
    dispatch(fetchSpeciality(id));
  }, []);
  return (
    <div>
      <img src={imageUrl} alt="логоти специальности" />
      <h2>{name}</h2>
      <p>{ReactHtmlParser(description)}</p>
    </div>
  );
}

export default Speciality;
