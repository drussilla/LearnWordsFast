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
        .then(qajax.toJSON)
        .then(data => {
            if (data.suceeded) {
                return q.resolve(data);
            } else {
                return q.reject(data);
            }
        });
};

let joinUrl = (...args) => {
    return args.join('/');
};

class Api {
    static userLogin(email, password) {
        return makeRequest({url: joinUrl('user', 'login'), data: {email: email, password: password}, method: 'POST'});
    }

    static userCreate(email, password) {
        return makeRequest({url: joinUrl('user', 'create'), data: {email: email, password: password}, method: 'POST'});
    }

    static userLogout() {
        return makeRequest({url: joinUrl('user', 'logout'), data: {}, method: 'POST'});
    }
}
;

export default Api;
