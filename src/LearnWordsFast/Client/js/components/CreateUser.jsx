import React from 'react';
import Reflux from 'reflux';
import {Input, Button, Panel} from 'react-bootstrap';
import reactMixin from 'react-mixin';
import {UserActions, UserStore} from '../stores/UserStore';

class CreateUser extends React.Component {
    constructor() {
        super();
        this.state = {
            password: null,
            email: null,
            passwordRepeat: null,
            isSamePasswords: true,
            errors: null
        };
        this.create = this.create.bind(this);
        this.changeField = this.changeField.bind(this);
        this.onUserDataLoad = this.onUserDataLoad.bind(this);
    }

    create() {
        if (this.state.password && this.state.password === this.state.passwordRepeat) {
            UserActions.create(this.state.email, this.state.password);
        } else {
            this.setState({
                isSamePasswords: false
            })
        }
    }

    changeField(type, e) {
        var value = e.target.value;
        var newState = {isSamePasswords: true};
        newState[type] = value;
        this.setState(newState);
    }

    onUserDataLoad(data) {
        this.setState({
            errors: data.errors
        })
    }

    render() {
        let {isSamePasswords} = this.state;
        let errors = this.state.errors && this.state.errors.map((error, i) => <div bsStyle='error'
                key={'error-' + i}>{error}</div>);
        return (
            <div onSubmit={this.create}>
                <Input onChange={this.changeField.bind(null, 'email')}
                       type="email" label="Email Address"
                       placeholder="Enter email"/>
                <Input bsStyle={!isSamePasswords || errors ? 'error' : null}
                       onChange={this.changeField.bind(null, 'password')}
                       type="password"
                       label="Password"/>
                <Input bsStyle={!isSamePasswords || errors ? 'error' : null}
                       onChange={this.changeField.bind(null, 'passwordRepeat')}
                       type="password"
                       label="Repeat Password"/>
                <Button onClick={this.create}>Create User</Button>
                {errors ?
                    <Panel header="Errors" className="create-user-errors" bsStyle="danger">
                       {errors}
                    </Panel> : null}
            </div>
        );
    }
}

reactMixin(CreateUser.prototype, Reflux.listenTo(UserStore, 'onUserDataLoad'));

export default CreateUser;
