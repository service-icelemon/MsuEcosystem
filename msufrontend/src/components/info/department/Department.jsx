import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import { fetchDepartment } from "../../../redux/actions/departments";
import ReactHtmlParser from "react-html-parser";
import { Image, Row, Col } from "react-bootstrap";
import TeacherContainer from "../../../containers/TeacherContainer";
import SpecialityContainer from "../../../containers/SpecialityContainer";
import TeacherPreview from '../teacher/TeacherPreview';

function Department() {
  const dispatch = useDispatch();
  const { name, imageUrl, description, manager, teachers, specialities } =
    useSelector(({ departments }) => departments.currentDepartment);
  const { id } = useParams();

  React.useEffect(() => {
    dispatch(fetchDepartment(id));
  }, [dispatch]);

  return (
    <div>
      <Row className="d-flex justify-content-center">
        <Image src={imageUrl} alt="эмблема кафедры" />
      </Row>

      <Row className="text-center">
        <Col>
          <h3>кафедра {name}</h3>
        </Col>
      </Row>
      <Row>
        <Col className="d-flex justify-content-center">
          {manager !== undefined && manager !== null ?
            <TeacherPreview
              key={manager.id}
              id={manager.id}
              photoUrl={manager.photoUrl}
              firstName={manager.firstName}
              lastName={manager.lastName}
              fatherName={manager.fatherName}
              scienceDegree={manager.scienceDegree}
            /> : <h3>нет данных</h3>}
        </Col>
      </Row>
      <Row className="text-center">
        <Col>
          <h3>Описание</h3>
        </Col>
      </Row>
      <Row>
        <Col className="text-center">
          <p>{ReactHtmlParser(description)}</p>
        </Col>
      </Row>
      <Row className="text-center">
        <Col>
          <h3>Преподаватели</h3>
        </Col>
      </Row>
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
