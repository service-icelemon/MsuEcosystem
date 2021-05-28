import React from "react";
import { Row, Col } from "react-bootstrap";
import Subject from "./Subject";

function Class({ type, time, isCanceled, subjects }) {
  console.log(subjects);
  return (
    <>
      <Col xs={6} md={2} lg={2}>
        {time.startTime} - {time.endTime}
      </Col>
      <Col xs={6} md={2} lg={2}>
        {type.name}
      </Col>
      {subjects.map((item, index) => (
        <Col>
          <Subject
            key={index}
            index={index}
            isCanceled={item.isCanceled}
            subject={item.subject}
            teacher={item.teacher}
            audience={item.audience}
            buildingNumber={item.buildingNumber}
          />
        </Col>
      ))}
    </>
  );
}

export default Class;
