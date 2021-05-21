import React from "react";
import { Link } from "react-router-dom";

export default function Navbar() {
  return (
    <div className="navbar">
      <div className="navbar__container">
        <ul className="navbar">
          <li>
            <Link to="/">Главная</Link>
          </li>
          <li>
            <Link to="/abiturient">Абитуриенту</Link>
          </li>
          <li>
            <Link to="/student">Студенту</Link>
          </li>
          <li>
            <Link to="/about">Библиотека</Link>
          </li>
          <li>
            <Link to="/about">Об университете</Link>
          </li>
        </ul>
      </div>
    </div>
  );
}
