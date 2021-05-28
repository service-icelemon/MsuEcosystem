import React from "react";
import { Col, Row } from "react-bootstrap";
import SpecialityPreview from "../components/info/speciality/SpecialityPreview";

function SpecialityContainer({ specialities }) {
  return (
    <Row>
      {specialities !== undefined ? (
        specialities.map((item, index) => (
          <Col md={4}>
            <SpecialityPreview
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
  );
}

export default SpecialityContainer;
