import React from "react";
import { useParams } from "react-router-dom";
import authApi from "../api/authApi";
import { Form, Col, Row, Button } from "react-bootstrap";

function ChangePassword() {
  const [password, setPassword] = React.useState("");
  const [confirmPassword, setConfirmPassword] = React.useState("");
  const { token } = useParams();

  const onSubmit = (e) => {
    e.preventDefault();
    authApi.resetPassword(token, password);
  };

  return (
    <Row className="justify-content-md-center">
      <Col xs lg="6">
        <Form>
          <Form.Group>
            <Form.Label>Введите новый пароль:</Form.Label>
            <Form.Control
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </Form.Group>

          <Form.Group>
            <Form.Label>Повторите пароль:</Form.Label>
            <Form.Control
              type="password"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
            />
          </Form.Group>
          <Button variant="primary" onClick={(e) => onSubmit(e)}>
            Изменить пароль
          </Button>
        </Form>
      </Col>
    </Row>
  );
}

export default ChangePassword;
