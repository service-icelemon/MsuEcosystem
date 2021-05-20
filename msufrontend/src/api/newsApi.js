import axiosInstance from './axiosInstance';


const newsApi = {
    getNews() {
        return axiosInstance.get(`GetPublicationList`).then(({ data }) => data);
    }
};

export default newsApi;