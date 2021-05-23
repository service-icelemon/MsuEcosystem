import React from "react";
import DepartmentPreview from "../components/info/department/DepartmentPreview";

function DepartmentContainer({ departments }) {
  return (
    <div>
      {departments !== undefined ? (
        departments.map((item, index) => (
          <DepartmentPreview
            key={index}
            id={item.id}
            name={item.name}
            image={item.imageUrl}
          />
        ))
      ) : (
        <span>загрузка..</span>
      )}
    </div>
  );
}

export default DepartmentContainer;
