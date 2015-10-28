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
        .then(data => {
            var response = data.response ? JSON.parse(data.response) : null;
            if (data.status === 200 || data.status === 201) {
                return q.resolve(response);
            } else {
                return q.reject(response);
            }
        })
};

let joinUrl = (...args) => {
    return args.join('/');
};

class User {
    static login(email, password) {
        return makeRequest({url: joinUrl('user', 'login'), data: {email: email, password: password}, method: 'POST'});
    }

    static create(userData) {
        return makeRequest({
            url: joinUrl('user'),
            data: {
                email: userData.email,
                password: userData.password,
                trainingLanguage: userData.trainingLanguage,
                mainLanguage: userData.mainLanguage,
                additionalLanguages: userData.additionalLanguages
            },
            method: 'POST'
        });
    }

    static logout() {
        return makeRequest({url: joinUrl('user', 'logout'), data: {}, method: 'POST'});
    }

    static getInfo() {
        return makeRequest({url: joinUrl('user', 'info')});
    }

    static changePassword(oldPassword, newPassword) {
        return makeRequest({
            url: joinUrl('user', 'password'),
            data: {oldPassword: oldPassword, newPassword: newPassword},
            method: 'PUT'
        });
    }

    static changeLanguages(trainingLanguage, mainLanguage, additionalLanguages) {
        return makeRequest({
            url: joinUrl('user', 'languages'),
            data: {
                trainingLanguage: trainingLanguage,
                mainLanguage: mainLanguage,
                additionalLanguages: additionalLanguages
            },
            method: 'PUT'
        })
    }
}

class Language {
    static getAll() {
        return makeRequest({url: joinUrl('language')});
    }
}

export default {User, Language};
