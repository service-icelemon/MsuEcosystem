import React from "react";
import TeacherPreview from "../components/info/teacher/TeacherPreview";
import { Col, Row } from "react-bootstrap";

function TeacherContainer({ teachers }) {
  return (
    <Row>
      {teachers !== undefined ? (
        teachers.map((item) => (
          <Col md={4}>
            <TeacherPreview
              key={item.id}
              id={item.id}
              photoUrl={item.photoUrl}
              firstName={item.firstName}
              lastName={item.lastName}
              fatherName={item.fatherName}
              scienceDegree={item.scienceDegree}
            />
          </Col>
        ))
      ) : (
        <span>загрузка...</span>
      )}
    </Row>
  );
}

export default TeacherContainer;
