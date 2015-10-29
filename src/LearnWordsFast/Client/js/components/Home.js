import React from 'react';
import Reflux from 'reflux';
import {ListGroup} from 'react-bootstrap';
import Word from './word/Word';
import {WordsActions, WordsStore} from '../stores/WordsStore';


const Home = React.createClass({
    mixins: [
        Reflux.connect(WordsStore, 'words')
    ],

    getInitialState() {
        return {
            words: WordsStore.words
        }
    },

    componentDidMount() {
        WordsActions.getAll();
    },

    render() {
        const {words} = this.state;
        if(!words) {
            return (<div>Loading...</div>);
        }
        if(words.length === 0) {
            return (<div>No words, please add some</div>);
        }
        return (
            <ListGroup>
                {words.map((word, i) => <Word word={word} key={i}/>)}
            </ListGroup>
        );
    }
});

export default Home;
