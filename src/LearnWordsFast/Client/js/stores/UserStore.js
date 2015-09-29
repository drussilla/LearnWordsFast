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
        this.isLoggedIn = AuthCookie.readCookie();
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
    create(userData) {
        this.errors = null;
        User.create(userData).then(() => {
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
            this.isLoggedIn = false;
            this._trigger();
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
