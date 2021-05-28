import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import { fetchSpeciality } from "../../../redux/actions/specialities";
import ReactHtmlParser from "react-html-parser";
import {
  LineChart,
  Line,
  CartesianGrid,
  XAxis,
  YAxis,
  Tooltip,
} from "recharts";
import { Row, Col, Image } from "react-bootstrap";

function Speciality() {
  const dispatch = useDispatch();
  const {
    name,
    imageUrl,
    description,
    scores,
    subjects,
    educationForms,
  } = useSelector(({ specialities }) => specialities.currentSpeciality);
  const { id } = useParams();

  React.useEffect(() => {
    dispatch(fetchSpeciality(id));
  }, []);
  return (
    <div>
      <Row className="d-flex justify-content-center">
        <Image src={imageUrl} alt="логоти специальности" />
      </Row>
      <Row className="text-center">
        <Col>
          <h3>{name}</h3>
        </Col>
      </Row>
      <Row className="text-center">
        <Col>
          <h3>Описание</h3>
        </Col>
      </Row>
      <Row>
        <Col className="text-center">
          <p>{ReactHtmlParser(description)}</p>
        </Col>
      </Row>
      <Row className="text-center">
        <Col>
          <h3>Проходные балы</h3>
        </Col>
      </Row>
      <Row className="justify-content-center chart">
        <Col md={6}>
          <LineChart width={600} height={400} data={scores}>
            <Line type="monotone" dataKey="budgetScore" stroke="#8884d8" />
            <Line type="monotone" dataKey="paidScore" stroke="#1848d8" />
            <CartesianGrid stroke="#ccc" strokeDasharray="5 5" />
            <XAxis dataKey="year" />
            <YAxis />
            <Tooltip />
          </LineChart>
        </Col>
      </Row>
    </div>
  );
}

export default Speciality;
