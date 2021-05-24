import React from "react";
import { Result, Button } from "antd";
import { Link } from "react-router-dom";

function NotFound() {
  return (
    <Result
      status="404"
      title="404"
      subTitle="Страницы, которую вы ищете не существует"
      extra={
        <Button type="primary">
          <Link to="/">На главную</Link>
        </Button>
      }
    />
  );
}

export default NotFound;
