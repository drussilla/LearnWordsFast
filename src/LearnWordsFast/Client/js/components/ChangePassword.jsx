import React from 'react';
import Reflux from 'reflux';
import {Input, Button, Panel} from 'react-bootstrap';
import {UserSettingsStore, UserSettingsActions} from '../stores/UserSettingsStore';

const ChangePassword = React.createClass({
    mixins: [
        Reflux.listenTo(UserSettingsStore, 'onUserSettingsLoad')
    ],

    getInitialState() {
        return {
            oldPassword: null,
            newPassword: null,
            errors: null,
            messages: null
        }
    },

    onUserSettingsLoad(data) {
        if(data.errors) {
            this.setState({
                errors: data.errors
            });
        } else {
            this.setState({
                oldPassword: null,
                newPassword: null,
                errors: null,
                message: 'Password was changed'
            })
        }
    },

    changePassword() {
        UserSettingsActions.changePassword({
            oldPassword: this.state.oldPassword,
            newPassword: this.state.newPassword
        });
        this.setState({
            errors: null,
            message: null
        });
    },

    onKeyPress(e) {
        if (e.which === 13 && this.state.oldPassword && this.state.newPassword) {
            this.changePassword();
        }
    },

    changeField(type, e) {
        var value = e.target.value;
        var newState = {};
        newState[type] = value;
        this.setState(newState);
    },

    render() {
        let errors = this.state.errors
            && this.state.errors.map((error, i) => <div bsStyle='error' key={'error-' + i}>
                {error}
            </div>);
        let {message} = this.state;
        return (
            <div>
                <Input onChange={this.changeField.bind(null, 'oldPassword')}
                       onKeyPress={this.onKeyPress}
                       value={this.state.oldPassword}
                       type='password'
                       label='Old Password'
                       placeholder='Enter old password'/>
                <Input onChange={this.changeField.bind(null, 'newPassword')}
                       onKeyPress={this.onKeyPress}
                       value={this.state.newPassword}
                       type='password'
                       label='New Password'
                       placeholder='Enter new password'/>
                <Button bsStyle="primary" disabled={!this.state.oldPassword || !this.state.newPassword}
                        onClick={this.changePassword}>Change password</Button>
                {errors ?
                    <Panel header='Errors' className='validation-errors' bsStyle='danger'>
                        {errors}
                    </Panel> : null}
                {message ?
                    <Panel header='Message' className='success-message' bsStyle='success'>
                        {message}
                    </Panel> : null}
            </div>
        );
    }
});

export default ChangePassword;
