import React from "react";
import { useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { fetchFaculty } from "../../../redux/actions/faculties";
import ReactHtmlParser from "react-html-parser";
import DepartmentContainer from "../../../containers/DepartmentContainer";
import { Image, Row, Col } from "react-bootstrap";

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
      <Row className="d-flex justify-content-center">
        <Image src={imageUrl} alt="эмблема факультета" />
      </Row>
      <Row className="text-center">
        <Col>
          <h2>факультет {name}</h2>
        </Col>
      </Row>
      <div>Декан: {dean !== undefined ? dean.firstName : "нет данных"}</div>
      <Row className="text-center">
        <Col>
          <h2>Описание</h2>
        </Col>
      </Row>
      <Row>
        <Col className="text-center">
          <p>{ReactHtmlParser(description)}</p>
        </Col>
      </Row>
      <Row className="text-center">
        <Col>
          <h2>Кафедры</h2>
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
