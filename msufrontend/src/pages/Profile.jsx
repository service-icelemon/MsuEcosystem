import React from "react";
import { useSelector } from "react-redux";
import { Image, Row, Col } from "react-bootstrap";

function Profile() {
  const user = useSelector(({ auth }) => auth.user);

  return (
    <>
      <Row >
        <Col className="text-center mb-3">
          <Image src={user.photoUrl} className="mx-auto"/>
        </Col>
      </Row>
      <Row>
        <Col className="text-center">
          <h2>{user.lastName} {user.firstName} {user.fatherName}</h2>
        </Col>
      </Row>
    </>
  );
}

export default Profile;
