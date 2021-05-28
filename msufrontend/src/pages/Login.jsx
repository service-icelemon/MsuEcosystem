import React from "react";
import { useDispatch } from "react-redux";
import { login } from "../redux/actions/auth";
import { Form, Button, Col, Row } from "react-bootstrap";
import { Link } from "react-router-dom";

function Login() {
  const dispatch = useDispatch();
  const [email, setEmail] = React.useState("");
  const [password, setPassword] = React.useState("");

  const onSubmit = (e) => {
    e.preventDefault();
    console.log(email);
    console.log(password);
    dispatch(login(email, password));
  };

  return (
    <Row className="justify-content-center h-100">
      <Col xs lg="6">
        <Form>
          <Form.Group>
            <Form.Label>Адрес электронной почты</Form.Label>
            <Form.Control
              type="email"
              value={email}
              placeholder="example@example.com"
              onChange={(e) => setEmail(e.target.value)}
            />
          </Form.Group>

          <Form.Group>
            <Form.Label>Пароль</Form.Label>
            <Form.Control
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </Form.Group>
          <Form.Group>
            <Form.Check type="checkbox" label="Запомнить меня" />
          </Form.Group>
          <Button
            variant="primary"
            type="submit"
            onClick={(e) => onSubmit(e)}
            className="mr-2"
          >
            Войти
          </Button>
          <Link to="/passwordforgot" className="mr-2">Забыли пароль?</Link>
          <Link to="/register">ещё нет аккаунта?</Link>
        </Form>
        
      </Col>
    </Row>
  );
}

export default Login;
