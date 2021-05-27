import React from "react";
import { Link } from "react-router-dom";
import { Card } from "antd";

function TeacherPreview({
  id,
  photoUrl,
  firstName,
  lastName,
  fatherName,
  scienceDegree,
}) {
  return (
    <div>
      <Link to={`/faculty/department/teacher/${id}`}>
        <Card
          hoverable
          cover={<img alt="example" src={photoUrl} height={400} />}
          className="mb-4"
        >
          <Card.Meta
            title={`${lastName} ${firstName} ${fatherName}`}
            description={scienceDegree}
            className="text-center"
          />
        </Card>
      </Link>
    </div>
  );
}

export default TeacherPreview;
