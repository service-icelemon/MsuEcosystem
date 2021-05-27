import React from "react";
import { useParams } from "react-router-dom";
import authApi from '../api/authApi';
import { Result, Button } from "antd";

function EmailConfirm() {
  const { token } = useParams();

  React.useEffect(() => {
    authApi.confirmEmail(token);
  }, []);

  return (
    <Result
      status="success"
      title="Адрес электронной почты успешно подтверждён!"
      subTitle="Можете войти в аккаунт"
      extra={
        <Button type="primary" key="console">
          На страницу входа
        </Button>
      }
    />
  );
}

export default EmailConfirm;
