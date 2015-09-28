import React from 'react';
import Reflux from 'reflux';
import _ from 'lodash';
import {Input, Button, Panel} from 'react-bootstrap';
import {UserActions, UserStore} from '../stores/UserStore';
import {LanguagesStore, LanguagesActions} from '../stores/LanguagesStore';

const CreateUser = React.createClass({
    mixins: [
        Reflux.listenTo(UserStore, 'onUserDataLoad'),
        Reflux.listenTo(LanguagesStore, 'onLanguagesLoad')
    ],

    getInitialState() {
        return {
            password: null,
            email: null,
            passwordRepeat: null,
            isSamePasswords: true,
            errors: null,
            languages: LanguagesStore.languages,
            mainLanguage: null,
            trainingLanguage: null,
            additionalLanguages: null
        }
    },

    componentDidMount() {
        LanguagesActions.getAll();
    },

    create() {
        if (this.state.password && this.state.password === this.state.passwordRepeat) {
            UserActions.create(this.state.email, this.state.password);
        } else {
            this.setState({
                isSamePasswords: false
            })
        }
    },

    changeField(type, e) {
        var value = e.target.value;
        var newState = {isSamePasswords: true};
        newState[type] = value;
        this.setState(newState);
    },

    onUserDataLoad(data) {
        this.setState({
            errors: data.errors
        })
    },

    onLanguagesLoad(languages) {
        this.setState({
            languages: languages,
            trainingLanguage: languages[0].id,
            mainLanguage: languages[1].id
        });
    },

    createLanguagesOptions(type) {
        if (this.state.languages) {
            return this.state.languages.filter(language => {
                if (type === 'main') {
                    return !_.contains(this.state.additionalLanguages, language.id)
                        && language.id !== this.state.trainingLanguage;
                } else if (type === 'training') {
                    return !_.contains(this.state.additionalLanguages, language.id)
                        && language.id !== this.state.mainLanguage;
                } else {
                    return language.id !== this.state.mainLanguage
                        && language.id !== this.state.trainingLanguage;
                }
            }).map(language => {
                return <option value={language.id} key={language.id}>{language.name}</option>;
            });
        }
    },

    selectTrainingLanguage(e) {
        var value = e.target.value;
        this.setState({
            trainingLanguage: value
        });
    },

    selectMainLanguage(e) {
        var value = e.target.value;
        this.setState({
            mainLanguage: value
        })
    },

    selectAdditionalLanguages(e) {
        var selectedLanguages = _.map(e.target.selectedOptions, option => option.value);
        if (selectedLanguages.length < 6) {
            this.setState({
                errors: null,
                additionalLanguages: selectedLanguages
            })
        } else {
            this.setState({
                errors: ["Don't select more than 5 languages as additional"]
            })
        }
    },

    render() {
        let {isSamePasswords} = this.state;
        let errors = this.state.errors && this.state.errors.map((error, i) => <div bsStyle='error'
                                                                                   key={'error-' + i}>{error}</div>);
        return (
            <div onSubmit={this.create}>
                <Input onChange={this.changeField.bind(null, 'email')}
                       type="email" label="Email Address"
                       placeholder="Enter email"/>
                <Input bsStyle={!isSamePasswords ? 'error' : null}
                       onChange={this.changeField.bind(null, 'password')}
                       type="password"
                       label="Password"/>
                <Input bsStyle={!isSamePasswords ? 'error' : null}
                       onChange={this.changeField.bind(null, 'passwordRepeat')}
                       type="password"
                       label="Repeat Password"/>
                <Input type="select"
                       label="Training language"
                       placeholder="select"
                       value={this.state.trainingLanguage}
                       onChange={this.selectTrainingLanguage}>
                    {this.createLanguagesOptions.call(null, 'training')}
                </Input>
                <Input type="select"
                       label="Main language"
                       placeholder="select"
                       value={this.state.mainLanguage}
                       onChange={this.selectMainLanguage}>
                    {this.createLanguagesOptions.call(null, 'main')}
                </Input>
                <Input type="select"
                       className="additional-languages"
                       label="Additional languages"
                       onChange={this.selectAdditionalLanguages}
                       multiple>
                    {this.createLanguagesOptions.call(null, 'additional')}
                </Input>
                <Button onClick={this.create}>Create User</Button>
                {errors ?
                    <Panel header="Errors" className="create-user-errors" bsStyle="danger">
                        {errors}
                    </Panel> : null}
            </div>
        );
    }
});

export default CreateUser;
