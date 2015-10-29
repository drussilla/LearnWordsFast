import React from 'react';
import {ListGroupItem} from 'react-bootstrap';

const Word = React.createClass({
    propTypes: {
        word: React.PropTypes.object,
        key: React.PropTypes.number
    },

    render() {
        const {word, key} = this.props;
        return(
            <ListGroupItem header={word.original} key={`${key}-${word.original}`}>
                <div>Translation: {word.translation && word.translation.translation}</div>
                <div>Context: {word.context} </div>
            </ListGroupItem>
        )
    }
});

export default Word;
