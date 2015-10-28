import Reflux from 'reflux';
import {User} from '../data/Api';
import _ from 'lodash';

const Actions = Reflux.createActions([
    'getInfo',
    'changePassword',
    'changeLanguages'
]);

let UserSettingsStore = Reflux.createStore({
    init() {
        this.listenToMany(Actions);
        this.userInfo = null;
        this.errors = null;
    },
    getInfo() {
        User.getInfo().done(response => {
            this.userInfo = response;
            this._trigger();
        }, response => {
            this.errors = response.errors;
            this._trigger();
        });
    },
    changePassword(passwordsData) {
        User.changePassword(passwordsData.oldPassword, passwordsData.newPassword).done(_ => {
            this._trigger();
        }, response => {
            this.errors = response.errors;
            this._trigger();
        });
    },
    changeLanguages(languagesData) {
        User.changeLanguages(languagesData.trainingLanguage, languagesData.mainLanguage, languagesData.additionalLanguages)
            .done();
    }
    ,
    _trigger() {
        this.trigger({userInfo: this.userInfo, errors: this.errors});
    }
});

export default {
    UserSettingsActions: Actions,
    UserSettingsStore: UserSettingsStore
}

