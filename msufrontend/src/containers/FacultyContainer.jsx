import React from "react";
import { useDispatch, useSelector } from "react-redux";
import FacultyPreview from "../components/info/faculty/FacultyPreview";
import { fetchFaculties } from "../redux/actions/faculties";
import { Col, Row } from "react-bootstrap";

function FacultyContainer() {
  const dispatch = useDispatch();
  const faculties = useSelector(({ faculties }) => faculties.faculties);

  React.useEffect(() => {
    dispatch(fetchFaculties());
  }, [dispatch]);

  return (
    <Row>
      {faculties.map((item) => (
        <Col md={4}>
          <FacultyPreview id={item.id} name={item.name} image={item.imageUrl} />
        </Col>
      ))}
    </Row>
  );
}

export default FacultyContainer;
