import departmentApi from '../../api/departmentApi';

export const setLoaded = (payload) => ({
    type: 'SET_LOADED',
    payload,
});

export const fetchDepartment = (id) => (dispatch) => {
    departmentApi.getDepartmentByFaculty(id).then(data => {
        dispatch(setDepartment(data));
    })
};

export const setDepartment = (item) => ({
    type: 'SET_DEPARTMENT',
    payload: item,
});
