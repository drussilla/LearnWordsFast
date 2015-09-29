import Reflux from 'reflux';
import {Language} from '../data/Api';

const Actions = Reflux.createActions([
    'getAll'
]);

let LanguagesStore = Reflux.createStore({
    init() {
        this.listenToMany(Actions);
        this.languages = null;
    },
    getAll() {
      if(this.languages) {
          this.trigger(this.languages);
      } else {
        Language.getAll().done(response => {
            this.languages = response;
            this.trigger(this.languages);
        }, response => console.log(response));
      }
    }

});

export default {
    LanguagesStore: LanguagesStore,
    LanguagesActions: Actions
}