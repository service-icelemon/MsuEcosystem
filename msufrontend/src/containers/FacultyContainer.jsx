import React from "react";
import { useDispatch, useSelector } from "react-redux";
import FacultyPreview from "../components/info/faculty/FacultyPreview";
import { fetchFaculties } from "../redux/actions/faculties";

function FacultyContainer() {
  const dispatch = useDispatch();
  const faculties = useSelector(({ faculties }) => faculties.faculties);

  React.useEffect(() => {
    dispatch(fetchFaculties());
  }, [dispatch]);

  return (
    <div>
      {faculties.map((item) => (
        <FacultyPreview
          id={item.id}
          name={item.name}
          image={item.imageUrl}
        />
      ))}
    </div>
  );
}

export default FacultyContainer;
