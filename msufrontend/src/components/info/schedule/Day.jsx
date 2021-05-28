import React from "react";
import { Col, Row } from "react-bootstrap";
import Class from "./Class";

function Day({ index, isCanceled, classes }) {
  const daynames = {
    1: "понедельник",
    2: "вторник",
    3: "среда",
    4: "четверг",
    5: "пятница",
    6: "суббота",
    7: "воскресение",
  };
  return (
    <div>
      <h3>{daynames[index]}</h3>
      {classes.map((item, index) => (
        <Row className="d-flex align-items-center">
            <Class
              key={index}
              type={item.type}
              time={item.time}
              isCanceled={isCanceled}
              subjects={item.subjects}
            />
        </Row>
      ))}
    </div>
  );
}

export default Day;
