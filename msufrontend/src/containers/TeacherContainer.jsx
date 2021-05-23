import React from "react";
import TeacherPreview from "../components/info/teacher/TeacherPreview";

function TeacherContainer({ teachers }) {
  return (
    <div>
      {teachers !== undefined ? (
        teachers.map((item) => (
          <TeacherPreview
            key={item.id}
            id={item.id}
            photoUrl={item.photoUrl}
            firstName={item.firstName}
            lastName={item.lastName}
            fatherName={item.fatherName}
            scienceDegree={item.scienceDegree}
          />
        ))
      ) : (
        <span>загрузка...</span>
      )}
    </div>
  );
}

export default TeacherContainer;
