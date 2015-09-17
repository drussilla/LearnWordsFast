import qajax from 'qajax';
import q from 'q';

import baseUrl from '../constants/baseUrl';

let makeRequest = (data) => {
    let request = {
        url: baseUrl + data.url,
        method: data.method || 'GET',
        data: data.data
    };
    return qajax(request)
        .then(qajax.toJSON);
};

let joinUrl = () => {
    let result = '';
    for (var i in arguments) {
        if(arguments.hasOwnProperty(i)) {
            result += arguments[i] + '/';
        }
    }
    return result;
};

class Api {
    static userLogin(email, password) {
        return makeRequest({url: joinUrl('user', 'login'), data: {email, password}, method: 'POST'});
    }
    static userCreate(email, password) {
        return makeRequest({url: joinUrl('user', 'create'), data: {email, password}, method: 'POST'});
    }
    static userLogout() {
        return makeRequest({url: joinUrl('user', 'logout'), data: {}, method: 'POST'});
    }
};

export default Api();
