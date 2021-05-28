import React from "react";
import { useDispatch } from "react-redux";
import { register } from "../redux/actions/auth";
import { Form, Button, Col, Row } from "react-bootstrap";
import { notification, Result } from "antd";
import { Redirect } from "react-router";

function Register() {
  const dispatch = useDispatch();
  const [isTeacher, setIsTeacher] = React.useState(false);
  const [email, setEmail] = React.useState("");
  const [password, setPassword] = React.useState("");
  const [username, setUsername] = React.useState("");
  const [teacherCode, setTeacherCode] = React.useState(0);
  const [studentCard, setstudentCard] = React.useState(0);

  const onSubmit = (e) => {
    e.preventDefault();
    dispatch(register(username, email, password, isTeacher, teacherCode, studentCard));
    openNotification();
  };

  const close = () => {
    console.log(
      "Notification was closed. Either the close button was clicked or duration time elapsed."
    );
  };

  const openNotification = () => {
    const key = `open${Date.now()}`;
    const btn = (
      <Button
        type="primary"
        size="small"
        onClick={() => notification.close(key)}
      >
        Ок
      </Button>
    );
    notification.open({
      message: "Почти готово",
      description:
        "Вам необходимо подтвердить ваш адрес электронной почты для завершения регистрации",
      btn,
      key,
      onClose: close,
    });
  };

  return (
    <Row className="d-flex justify-content-center">
      <Col md={4}>
        <Form>
          <Form.Group className="mb-3">
            <Form.Label>Имя пользователя:</Form.Label>
            <Form.Control
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              type="text"
            />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Адрес электронной почты:</Form.Label>
            <Form.Control
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="Enter email"
            />
          </Form.Group>

          {/* <Form.Group className="mb-3">
        <Form.Label>Кто вы?</Form.Label>
        <Form.Control
          value={isTeacher}
          onChange={(e) => setIsTeacher(e.target.value)}
          as="select"
        >
          <option value={0}>Студент</option>
          <option value={1}>Преподаватель</option>
        </Form.Control>
      </Form.Group> */}

          <Form.Group className="mb-3">
            <Form.Label>Номер студенческого билета:</Form.Label>
            <Form.Control
              value={studentCard}
              onChange={(e) => setstudentCard(e.target.value)}
              type="number"
            />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Пароль:</Form.Label>
            <Form.Control
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              type="password"
              placeholder="Password"
            />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Label>Повторите пароль:</Form.Label>
            <Form.Control type="password" placeholder="Password" />
          </Form.Group>
          <Button variant="primary" type="submit" onClick={(e) => onSubmit(e)}>
            Зарегистрироваться
          </Button>
        </Form>
      </Col>
    </Row>
  );
}

export default Register;
