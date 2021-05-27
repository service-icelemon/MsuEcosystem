import React from "react";
import { useSelector } from "react-redux";
import { useParams } from "react-router-dom";

function TeacherProfile() {
  const teacher = useSelector(({ teachers }) => teachers.currentTeacher);
  const { id } = useParams();
  return <div></div>;
}

export default TeacherProfile;
