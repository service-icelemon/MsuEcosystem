import React from "react";
import { useDispatch, useSelector } from "react-redux";
import scheduleApi from "../../../api/scheduleApi";
import Day from "./Day";

function Schedule() {
  const dispatch = useDispatch();
  const groupNum = useSelector(({ auth }) => auth.user.groupNumber);
  const [schedule, setSchedule] = React.useState(null);

  React.useEffect(() => {
    scheduleApi.getSchedule(groupNum).then((data) => setSchedule(data));
  }, []);

  return (
    <div>
      {schedule !== null ? (
        schedule.days.map((day) => (
          <Day
            key={day.index}
            index={day.index}
            isCanceled={day.isCanceled}
            classes={day.classes}
          />
        ))
      ) : (
        <span>загрузка..</span>
      )}
    </div>
  );
}

export default Schedule;
