import React from "react";
import { Link } from "react-router-dom";
import { useSelector } from "react-redux";
import { Navbar, Nav, NavDropdown, Container, Button } from "react-bootstrap";
import logo from "../assets/images/logo.png";
import ProfileIcon from "./Profile/ProfileIcon";
import { SpaceContext } from "antd/lib/space";

export default function NavigationBar() {
  const isAuthenticated = useSelector(({ auth }) => auth.isAuthenticated);
  return (
    <>
      <Navbar bg="light" expand="lg" className="mb-4">
        <Container>
          <Link to="/">
            <Navbar.Brand href="#home">
              <img
                src={logo}
                width="30"
                height="30"
                className="d-inline-block align-top"
                alt="React Bootstrap logo"
              />
            </Navbar.Brand>
          </Link>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="mr-auto">
              <Nav.Link>
                <Link to="/">Главная</Link>
              </Nav.Link>
              <Nav.Link>
                <Link to="/faculties">Факультеты</Link>
              </Nav.Link>
              <NavDropdown title="Абитуриетнту" id="basic-nav-dropdown">
                <NavDropdown.Item href="#action/3.1">Action</NavDropdown.Item>
                <NavDropdown.Item href="#action/3.2">
                  Как поступить?
                </NavDropdown.Item>
                <NavDropdown.Item href="#action/3.3">
                  Приёмная компания
                </NavDropdown.Item>
              </NavDropdown>
              <NavDropdown title="Студенту" id="basic-nav-dropdown">
                <NavDropdown.Item href="#action/3.1">Action</NavDropdown.Item>
                <NavDropdown.Item href="#action/3.2">События</NavDropdown.Item>
                <NavDropdown.Item href="#action/3.3">
                  Приёмная компания
                </NavDropdown.Item>
              </NavDropdown>
            </Nav>
            {isAuthenticated ? (
              <ProfileIcon />
            ) : (
              <Link to="/login">
                <Button>Войти</Button>
              </Link>
            )}
          </Navbar.Collapse>
        </Container>
      </Navbar>
    </>
  );
}
