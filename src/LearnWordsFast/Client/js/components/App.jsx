import React from 'react';
import {Link} from 'react-router';
import {Grid, Row, Col, Navbar, Nav, NavItem, NavDropdown, MenuItem} from 'react-bootstrap';
import reactMixin from 'react-mixin';
import Reflux from 'reflux';

import {UserStore} from '../stores/UserStore';

const App = React.createClass({
    mixins: [
        Reflux.listenTo(UserStore, 'onUserDataLoad')
    ],

    getInitialState() {
        return {
            isLoggedIn: UserStore.isLoggedIn
        }
    },

    onUserDataLoad(data) {
        const {history} = this.props;
        if (data.isLoggedIn) {
            history.pushState(null, '/home');
        } else {
            if (!history.isActive('/create')) {
                history.pushState(null, '/login');
            }
        }
        this.setState({
            isLoggedIn: data.isLoggedIn
        });
    },

    logout() {
        UserStore.logout();
    },

    render() {
        var {isLoggedIn} = this.state;
        return (
            <div>
                <Navbar brand={<a href="#/home">Learn Words Fast</a>} staticTop inverse>
                    {isLoggedIn ?
                        <Nav>
                            <NavItem href="#/home">Home</NavItem>
                            <NavItem onClick={this.logout} href="#/login">Logout</NavItem>
                            <NavDropdown title="Settings" id="settings-dropdown" className="settings">
                                <MenuItem href="#/settings/password">Change Password</MenuItem>
                                <MenuItem href="#/settings/languages">Change Languages</MenuItem>
                            </NavDropdown>
                        </Nav>
                        :
                        <Nav>
                            <NavItem href="#/login">Login</NavItem>
                            <NavItem href="#/create">Create</NavItem>
                        </Nav>
                    }
                </Navbar>
                <Grid>
                    <Row>
                        <Col lg={3} md={3}/>
                        <Col lg={6} md={6}>
                            {this.props.children}
                        </Col>
                        <Col lg={3} md={3}/>
                    </Row>
                </Grid>
                <footer>
                    <a href="https://github.com/drussilla/LearnWordsFast">GitHub</a>
                    <a href="http://druss.co">Druss</a>
                </footer>
            </div>
        );
    }
});

export default App;
