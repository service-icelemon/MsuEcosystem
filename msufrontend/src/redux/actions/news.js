import newsApi from '../../api/newsApi';

export const setLoaded = (payload) => ({
    type: 'SET_PRODUCTS',
    payload,
});

export const fetchPosts = () => (dispatch) => {
    newsApi.getPosts().then(data => {
        dispatch(setPosts(data));
    })
};

export const fetchPost = (id) => (dispatch) => {
    newsApi.getPost(id).then(data => {
        dispatch(setPost(data));
    })
};


export const setPosts = (items) => ({
    type: 'SET_POSTS',
    payload: items,
});

export const setPost = (item) => ({
    type: 'SET_POST',
    payload: item,
});
