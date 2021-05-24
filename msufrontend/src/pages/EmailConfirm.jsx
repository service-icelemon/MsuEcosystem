import React from "react";
import { Result, Button } from "antd";

function EmailConfirm() {
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
