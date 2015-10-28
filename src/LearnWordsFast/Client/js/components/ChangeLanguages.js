import React from 'react';
import Reflux from 'reflux';
import _ from 'lodash';
import {Input, Button, Panel} from 'react-bootstrap';
import {UserSettingsStore, UserSettingsActions} from '../stores/UserSettingsStore';
import {LanguagesStore, LanguagesActions} from '../stores/LanguagesStore';

const ChangeLanguages = React.createClass({
    mixins: [
        Reflux.listenTo(UserSettingsStore, 'onUserSettingsLoad'),
        Reflux.listenTo(LanguagesStore, 'onLanguagesLoad')
    ],

    getInitialState() {
        let {userInfo} = UserSettingsStore;
        let {mainLanguage, trainingLanguage, additionalLanguages} = userInfo || {};
        let {languages} = LanguagesStore;
        return {
            userInfo,
            languages,
            mainLanguage,
            trainingLanguage,
            additionalLanguages
        }
    },

    componentDidMount() {
        UserSettingsActions.getInfo();
        LanguagesActions.getAll();
    },

    onUserSettingsLoad(data) {
        let {userInfo} = data;
        let {mainLanguage, trainingLanguage, additionalLanguages} = userInfo || {};
        this.setState({
            userInfo,
            mainLanguage,
            trainingLanguage,
            additionalLanguages
        });
    },

    onLanguagesLoad(languages) {
        this.setState({
            languages
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
                return <Input type="checkbox" label={language.name}
                              key={language.id}
                              checked={_.contains(this.state.additionalLanguages, language.id)}
                              onChange={this.selectAdditionalLanguage.bind(null, language.id)}/>
            });
        }
    },

    changeLanguages() {
        let {trainingLanguage, mainLanguage, additionalLanguages} = this.state;
        UserSettingsActions.changeLanguages({trainingLanguage, mainLanguage, additionalLanguages})
    },

    render() {
        let errors = this.state.errors
            && this.state.errors.map((error, i) => <div bsStySle="error" key={'error-' + i}>
                {error}
            </div>);
        return (
            <div>
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

                <Button bsStyle="primary"
                        onClick={this.changeLanguages}>Change Languages</Button>
                {errors ?
                    <Panel header="Errors" className="validation-errors" bsStyle="danger">
                        {errors}
                    </Panel> : null}
            </div>
        );
    }
});

export default ChangeLanguages;
