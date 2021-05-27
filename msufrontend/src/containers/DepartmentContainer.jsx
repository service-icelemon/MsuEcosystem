import React from "react";
import { Row, Col } from "react-bootstrap";
import DepartmentPreview from "../components/info/department/DepartmentPreview";

function DepartmentContainer({ departments }) {
  return (
    <div>
      <Row>
        {departments !== undefined ? (
          departments.map((item, index) => (
            <Col md={4}>
              <DepartmentPreview
                key={index}
                id={item.id}
                name={item.name}
                image={item.imageUrl}
              />
            </Col>
          ))
        ) : (
          <span>загрузка..</span>
        )}
      </Row>
    </div>
  );
}

export default DepartmentContainer;
