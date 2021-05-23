import React from "react";
import SpecialityPreview from "../components/info/speciality/SpecialityPreview";

function SpecialityContainer({ specialities }) {
  return (
    <div>
      {specialities !== undefined ? (
        specialities.map((item, index) => (
          <SpecialityPreview
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

export default SpecialityContainer;
