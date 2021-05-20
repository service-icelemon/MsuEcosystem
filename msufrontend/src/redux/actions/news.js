import newsApi from '../../api/newsApi';

export const setLoaded = (payload) => ({
    type: 'SET_PRODUCTS',
    payload,
});

export const fetchNews = () => (dispatch) => {
    newsApi.getNews().then(data => {
        dispatch(setNews(data));
    })
};

export const setNews = (items) => ({
    type: 'SET_NEWS',
    payload: items,
});
