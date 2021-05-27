import React from "react";

function PasswordForgot() {
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
            Войти
          </Button>
          <Button variant="secondary">Зарегистрироваться</Button>
        </Form>
      </Col>
    </Row>
  );
}


function ForgotPasswordModal() {
    const [isModalVisible, setIsModalVisible] = useState(false);

    return (
        <>
      <Modal title="Basic Modal" visible={isModalVisible} onOk={handleOk} onCancel={handleCancel}>
        <p>Some contents...</p>
        <p>Some contents...</p>
        <p>Some contents...</p>
      </Modal>
      </>
    )
}

export default PasswordForgot;
