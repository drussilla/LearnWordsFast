import authCookie from '../constants/authCookie';

class AuthCookie {
    static readCookie() {
        var nameEQ = authCookie + "=";
        var ca = document.cookie.split(';');
        for(var i=0;i < ca.length;i++) {
            var c = ca[i];
            while (c.charAt(0)==' ') c = c.substring(1,c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
        }
        return null;
    }
    static deleteCookie() {
        document.cookie = authCookie + '=;expires=Thu, 01 Jan 1970 00:00:01 GMT;';
    }
}

export default AuthCookie;