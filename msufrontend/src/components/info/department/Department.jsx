import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import { fetchDepartment } from "../../../redux/actions/departments";
import ReactHtmlParser from "react-html-parser";
import TeacherContainer from "../../../containers/TeacherContainer";
import SpecialityContainer from "../../../containers/SpecialityContainer";

function Department() {
  const dispatch = useDispatch();
  const { name, imageUrl, description, manager, teachers, specialities } =
    useSelector(({ departments }) => departments.currentDepartment);
  const { id } = useParams();

  React.useEffect(() => {
    dispatch(fetchDepartment(id));
  }, []);

  return (
    <div>
      <img src={imageUrl} alt="эмблема кафедры" />
      <h2>{name}</h2>
      <div>
        Председатель: {manager !== undefined && manager !== null ? manager.firstName : "нет данных"}
      </div>
      <p>{ReactHtmlParser(description)}</p>
      <h2>Преподаватели</h2>
      <div>
        {teachers !== undefined ? (
          <TeacherContainer teachers={teachers} />
        ) : (
          <span>загрузка...</span>
        )}
      </div>
      <h2>Специальности</h2>
      <div>
        {specialities !== undefined && specialities !== null ? (
          <SpecialityContainer specialities={specialities} />
        ) : (
          <span>загрузка...</span>
        )}
      </div>
    </div>
  );
}

export default Department;
