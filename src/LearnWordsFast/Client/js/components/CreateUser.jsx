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
            additionalLanguages: []
        }
    },

    componentDidMount() {
        LanguagesActions.getAll();
    },

    create() {
        if (this.state.password && this.state.password === this.state.passwordRepeat) {
            UserActions.create({
                email: this.state.email,
                password: this.state.password,
                trainingLanguage: this.state.trainingLanguage,
                mainLanguage: this.state.mainLanguage,
                additionalLanguages: this.state.additionalLanguages
            });
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
                } else {
                    return !_.contains(this.state.additionalLanguages, language.id)
                        && language.id !== this.state.mainLanguage;
                }
            }).map(language => {
                return <option value={language.id} key={language.id}>{language.name}</option>;
            });
        }
    },

    selectTrainingLanguage(e) {
        var value = e.target.value;
        this.setState({
            errors: null,
            trainingLanguage: value
        });
    },

    selectMainLanguage(e) {
        var value = e.target.value;
        this.setState({
            errors: null,
            mainLanguage: value
        })
    },

    selectAdditionalLanguage(id, e) {
        var additionalLanguages = _.clone(this.state.additionalLanguages);
        var errors = null;
        if (e.target.checked) {
            if (additionalLanguages.length < 5) {
                additionalLanguages.push(id);
            } else {
                errors = ["Don't select more than 5 languages as additional"]
            }
        } else {
            additionalLanguages = additionalLanguages.filter(language => language !== id);
        }
        this.setState({
            errors: errors,
            additionalLanguages: additionalLanguages
        });
    },

    createCheckboxesForAdditionalLanguages() {
        if (this.state.languages) {
            return this.state.languages.filter(language => {
                return language.id !== this.state.mainLanguage
                    && language.id !== this.state.trainingLanguage;
            }).map(language => {
                return <Input type='checkbox' label={language.name}
                              key={language.id}
                              checked={_.contains(this.state.additionalLanguages, language.id)}
                              onChange={this.selectAdditionalLanguage.bind(null, language.id)}/>
            });
        }
    },

    render() {
        let {isSamePasswords} = this.state;
        let errors = this.state.errors
            && this.state.errors.map((error, i) => <div bsStyle='error' key={'error-' + i}>
                {error}
            </div>);
        return (
            <form onSubmit={this.create}>
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
                <label>Additional Languages</label>
                {this.createCheckboxesForAdditionalLanguages()}

                <Button onClick={this.create}>Create User</Button>
                {errors ?
                    <Panel header="Errors" className="create-user-errors" bsStyle="danger">
                        {errors}
                    </Panel> : null}
            </form>
        );
    }
});

export default CreateUser;
