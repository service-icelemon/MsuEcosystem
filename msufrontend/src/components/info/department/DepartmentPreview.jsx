import React from "react";
import { Link } from "react-router-dom";
import { Card } from "antd";

function DepartmentPreview({ id, image, name }) {
  return (
    <div>
      {/* <img src={image} alt="эмблема кафедры" />
      <Link to={`department/${id}`}>
        <h2>{name}</h2>
      </Link> */}

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
