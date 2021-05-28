import axiosInstance from "./axiosInstance";

const scheduleApi = {
    getSchedule(groupNum) {
      return axiosInstance
        .get(`Schedule/${groupNum}`)
        .then(({ data }) => data);
    }
  };
  
  export default scheduleApi;
  