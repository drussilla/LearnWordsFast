import Reflux from 'reflux';
import readCookie from '../helpers/readCookie';
import Api from '../data/Api';

const Actions = Reflux.createActions([
    'login',
    'create'
]);

let UserStore = Reflux.createStore({
    init() {
        this.listenToMany(Actions);
        this.isLoggedIn = !!readCookie();
        this.errors = null;
        this._trigger();
    },
    login(email, password) {
        Api.userLogin(email, password).then(() => {
            this.isLoggedIn = true;
            this._trigger();
        }, respose => {
            console.log(respose);
            this.isLoggedIn = false;
            this._trigger();
        });
    },
    create(email, password) {
        this.errors = null;
        Api.userCreate(email, password).then((response) => {
            this.isLoggedIn = true;
            this._trigger();
            console.log(response);
        }, response => {
            this.errors = response.errors;
            this._trigger();
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
