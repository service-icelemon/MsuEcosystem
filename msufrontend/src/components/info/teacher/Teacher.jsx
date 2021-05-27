import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import ReactHtmlParser from "react-html-parser";
import { fetchTeacher } from "../../../redux/actions/teachers";
import { Image, Row, Col } from "react-bootstrap";

function Teacher() {
  const dispatch = useDispatch();
  const {
    photoUrl,
    firstName,
    lastName,
    fatherName,
    biography,
    scienceDegree,
    departmentName,
    facultyName,
  } = useSelector(({ teachers }) => teachers.currentTeacher);
  const { id } = useParams();

  React.useEffect(() => {
    dispatch(fetchTeacher(id));
  }, []);
  return (
    <div>
      <Row className="justify-content-center mb-4">
        <Image
          src={photoUrl}
          alt="фото преподавателя"
          fluid
          width={250}
          height={350}
        />
      </Row>
      <Row className="text-center">
        <Col>
          <h2 className="mb-2">
            {lastName} {firstName} {fatherName}
          </h2>
          <span>{scienceDegree}</span>
        </Col>
      </Row>
      <Row className="text-center">
        <Col>
          <span>Факультет {facultyName}</span>
        </Col>
      </Row>
      <Row className="text-center">
        <Col>
          <span>Кафедра {departmentName}</span>
        </Col>
      </Row>
      <Row className="text-center">
        <Col>
          <h2>Биография</h2>
        </Col>
      </Row>
      <Row>
        <Col className="text-center">
          <p>{ReactHtmlParser(biography)}</p>
        </Col>
      </Row>
    </div>
  );
}

export default Teacher;
