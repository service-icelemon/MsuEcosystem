import axios from 'axios';

const apiInstance = axios.create({
    withCredentials: true,
    baseURL: "https://localhost:44378/api/"
});

export default apiInstance;
