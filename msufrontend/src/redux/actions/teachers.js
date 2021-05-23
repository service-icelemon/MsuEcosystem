import teacherApi from '../../api/teacherApi';

export const setLoaded = (payload) => ({
    type: 'SET_LOADED',
    payload,
});

export const fetchTeacher = (id) => (dispatch) => {
    teacherApi.getTeacher(id).then(data => {
        dispatch(setTeacher(data));
    })
};

export const setTeacher = (item) => ({
    type: 'SET_TEACHER',
    payload: item,
});
