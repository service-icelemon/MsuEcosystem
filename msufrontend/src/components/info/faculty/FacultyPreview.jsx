import React from "react";
import { Link } from "react-router-dom";
import { Card } from "antd";

function FacultyPreview({ id, image, name }) {
  return (
    <Link to={`/faculty/${id}`}>
      <Card
        hoverable
        cover={<img alt="example" src={image} width={100} height={200}/>}
        className="mb-4"
      >
        <Card.Meta title={name} className="text-center"/>
      </Card>
    </Link>
  );
}

export default FacultyPreview;
