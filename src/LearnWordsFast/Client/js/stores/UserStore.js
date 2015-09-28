import Reflux from 'reflux';
import AuthCookie from '../helpers/AuthCookie';
import {User} from '../data/Api';

const Actions = Reflux.createActions([
    'login',
    'create',
    'logout'
]);

let UserStore = Reflux.createStore({
    init() {
        this.listenToMany(Actions);
        this.isLoggedIn = AuthCookie.readCookie() !== null;
        this.errors = null;
        this._trigger();
    },
    login(email, password) {
        this.errors = null;
        User.login(email, password).then(() => {
            this.isLoggedIn = true;
            this._trigger();
        }, response => {
            this.errors = response.errors;
            this._trigger();
        });
    },
    create(email, password) {
        this.errors = null;
        User.create(email, password).then(() => {
            this.isLoggedIn = true;
            this._trigger();
        }, response => {
            this.errors = response.errors;
            this._trigger();
        })
    },
    logout() {
        this.errors = null;
        User.logout().then(() => {
            AuthCookie.deleteCookie();
        }, response => {
            console.log(response);
        })
    },
    _trigger() {
        this.trigger({isLoggedIn: this.isLoggedIn, errors: this.errors});
    }
});

export default {
    UserActions: Actions,
    UserStore: UserStore
}
