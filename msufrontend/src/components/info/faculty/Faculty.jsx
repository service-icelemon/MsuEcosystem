import React from "react";
import { useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { fetchFaculty } from "../../../redux/actions/faculties";
import ReactHtmlParser from "react-html-parser";
import DepartmentContainer from "../../../containers/DepartmentContainer";
import TeacherPreview from "../teacher/TeacherPreview";
import { Image, Row, Col } from "react-bootstrap";

function Faculty() {
  const dispatch = useDispatch();
  const { name, imageUrl, description, dean, departments } = useSelector(
    ({ faculties }) => faculties.currentFaculty
  );
  const { id } = useParams();

  React.useEffect(() => {
    dispatch(fetchFaculty(id));
  }, [dispatch]);

  return (
    <div>
      <Row className="d-flex justify-content-center">
        <Image src={imageUrl} alt="эмблема факультета" />
      </Row>
      <Row className="text-center">
        <Col>
          <h3>факультет {name}</h3>
        </Col>
      </Row>
      <Row>
        <Col className="text-center">
          <h3>Декан факультета</h3>
        </Col>
      </Row>
      <Row>
        <Col className="d-flex justify-content-center">
          {dean !== undefined && dean !== null ?
            <TeacherPreview
              key={dean.id}
              id={dean.id}
              photoUrl={dean.photoUrl}
              firstName={dean.firstName}
              lastName={dean.lastName}
              fatherName={dean.fatherName}
              scienceDegree={dean.scienceDegree}
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
          <h3>Кафедры</h3>
        </Col>
      </Row>
      <div>
        {departments !== undefined ? (
          <DepartmentContainer departments={departments} />
        ) : (
          <span>загрузка...</span>
        )}
      </div>
    </div>
  );
}

export default Faculty;
