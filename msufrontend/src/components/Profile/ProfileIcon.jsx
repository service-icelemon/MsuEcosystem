import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link } from "react-router-dom";
import Avatar from "@material-ui/core/Avatar";
import { Menu, Dropdown, Col, Row } from "antd";
import { logout } from "../../redux/actions/auth";

function ProfileIcon() {
  const dispatch = useDispatch();
  const user = useSelector(({ auth }) => auth.user);

  const onExitClick = () => {
    dispatch(logout());
  }

  const menu = (
    <Menu>
      <Menu.Item>
        <Link to="/profile">Личный кабинет</Link>
      </Menu.Item>
      <Menu.Item>
        <Link to="/schedule">Расписание</Link>
      </Menu.Item>
      <Menu.Item onClick={() => onExitClick()}>Выход</Menu.Item>
    </Menu>
  );
  return (
    <Dropdown overlay={menu} placement="bottomCenter" arrow>
      <div className="d-flex align-items-center">
        <span className="mr-2">{user.firstName}</span>
        <Avatar src={user.photoUrl}></Avatar>
      </div>
    </Dropdown>
  );
}

export default ProfileIcon;
