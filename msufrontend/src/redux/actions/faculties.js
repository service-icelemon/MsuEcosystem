import facultyApi from '../../api/facultyApi';

export const setLoaded = (payload) => ({
    type: 'SET_PRODUCTS',
    payload,
});

export const fetchFaculties = () => (dispatch) => {
    facultyApi.getFaculties().then(data => {
        dispatch(setFaculties(data));
    })
};

export const fetchFaculty = (id) => (dispatch) => {
    facultyApi.getFaculty(id).then(data => {
        dispatch(setFaculty(data));
    })
};


export const setFaculties = (items) => ({
    type: 'SET_FACULTIES',
    payload: items,
});

export const setFaculty = (item) => ({
    type: 'SET_FACULTY',
    payload: item,
});
