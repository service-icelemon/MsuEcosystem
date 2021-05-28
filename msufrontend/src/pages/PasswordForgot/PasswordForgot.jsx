import React from "react";
import { notification } from "antd";
import { Form, Button, Col, Row } from "react-bootstrap";
import authApi from "../../api/authApi";

function PasswordForgot() {
  const [email, setEmail] = React.useState("");
  const onSubmit = (e) => {
    e.preventDefault();
    authApi.resetPassword(email)
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
      message: "Подтвердите смену пароля",
      description:
        "на этот адрес электронной почты отправлено письмо с ссылкой для сброса пароля",
      btn,
      key,
      onClose: close,
    });
  };

  return (
    <Row className="justify-content-md-center">
      <Col xs lg="6">
        <Form>
          <Form.Group>
            <Form.Label>Введите адрес электронной почты</Form.Label>
            <Form.Control
              type="email"
              value={email}
              placeholder="example@example.com"
              onChange={(e) => setEmail(e.target.value)}
            />
          </Form.Group>
          <Button
            variant="primary"
            type="submit"
            onClick={(e) => onSubmit(e)}
            className="mr-2"
          >
            Сбросить пароль
          </Button>
        </Form>
      </Col>
    </Row>
  );
}

export default PasswordForgot;
