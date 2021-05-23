import specialityApi from '../../api/specialityApi';

export const setLoaded = (payload) => ({
    type: 'SET_LOADED',
    payload,
});

export const fetchSpeciality = (id) => (dispatch) => {
    specialityApi.getSpeciality(id).then(data => {
        dispatch(setSpeciality(data));
    })
};

export const setSpeciality = (item) => ({
    type: 'SET_SPECIALITY',
    payload: item,
});
