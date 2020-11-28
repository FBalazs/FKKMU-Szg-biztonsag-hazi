export const requestOptions = {
    headers: {
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + sessionStorage.getItem("token"),
    },
};