import Reflux from 'reflux';
import {Word} from '../data/Api';

const Actions = Reflux.createActions([
    'getAll'
]);

let WordsStore = Reflux.createStore({
    init() {
        this.listenToMany(Actions);
        this.words = null;
    },
    getAll() {
        Word.getAll().done( response => {
                this.words = response;
                this.trigger(this.words);
            }, response => {

            }
        );
    }
});

export default {
    WordsActions: Actions,
    WordsStore: WordsStore
}