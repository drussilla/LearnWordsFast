import React from 'react';
import Reflux from 'reflux';
import {Input, Button, Panel} from 'react-bootstrap';
import {UserActions, UserStore} from '../stores/UserStore';

const Login = React.createClass({
    mixins: [
        Reflux.listenTo(UserStore, 'onUserDataLoad')
    ],

    getInitialState() {
        return {
            email: null,
            password: null,
            errors: null
        }
    },

    onUserDataLoad(data) {
        if (!data.isLoggedIn) {
            this.setState({
                errors: data.errors
            })
        }
    },

    login() {
        UserActions.login(this.state.email, this.state.password);
        this.setState({
            errors: null
        });
    },

    onKeyPress(e) {
        if(e.which === 13 && this.state.password && this.state.email) {
            this.login();
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
            && this.state.errors.map((error, i) => <div bsStyle="error" key={"error-" + i}>
                {error}
            </div>);
        return (
            <div>
                <Input onChange={this.changeField.bind(null, "email")}
                       onKeyPress={this.onKeyPress}
                       type="email" label="Email Address"
                       placeholder="Enter email"/>
                <Input onChange={this.changeField.bind(null, "password")}
                       onKeyPress={this.onKeyPress}
                       type="password"
                       label="Password"
                       placeholder="Enter password"/>
                <Button bsStyle="primary" disabled={!this.state.password || !this.state.email} onClick={this.login}>Login</Button>
                {errors ?
                    <Panel header="Errors" className="validation-errors" bsStyle="danger">
                        {errors}
                    </Panel> : null}
            </div>
        );
    }
});

export default Login;
