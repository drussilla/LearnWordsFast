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
        this.info = null;
        this.errors = null;
    },
    getInfo() {
        User.info().done(response => {
            debugger;
            //TODO get info
            this.info = response.info;
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
        User.
            changeLanguages(languagesData.trainingLanguage, languagesData.mainLanguage, languagesData.additionalLanguages)
            .done();
    }
    ,
    _trigger() {
        this.trigger({info: _.cloneDeep(this.info), errors: this.errors});
    }
});

export default {
    UserSettingsActions: Actions,
    UserSettingsStore: UserSettingsStore
}

