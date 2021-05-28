import React from "react";
import { Link } from "react-router-dom";
import { Card } from "antd";

function DepartmentPreview({ id, image, name }) {
  return (
    <div>
      <Link to={`department/${id}`}>
        <Card
          hoverable
          cover={<img alt="example" src={image} width={250} height={400} />}
          className="mb-4"
        >
          <Card.Meta
            title={name}
            className="text-center"
          />
        </Card>
      </Link>
    </div>
  );
}

export default DepartmentPreview;
