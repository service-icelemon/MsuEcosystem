import React from "react";
import { Row, Col } from "react-bootstrap";

function Subject({
  index,
  isInUpperWeek,
  isInLowerWeek,
  isCanceled,
  subject,
  teacher,
  buildingNumber,
  audience,
}) {
  return (
    <div className="d-flex flex-column flex-start">
      <span>Подгруппа {index + 1}</span>
      <h6>{subject.name}</h6>
      <span>
        {teacher.lastName} {teacher.firstName[0]}. {teacher.fatherName[0]}.
      </span>
      к. {buildingNumber}, ауд. {audience}
    </div>
  );
}

export default Subject;
