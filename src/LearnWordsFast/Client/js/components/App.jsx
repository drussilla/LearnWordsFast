import React from 'react';
import {Link} from 'react-router';
import {Grid, Row, Col, Navbar, Nav, NavItem} from 'react-bootstrap';
import reactMixin from 'react-mixin';
import Reflux from 'reflux';

import {UserStore} from '../stores/UserStore';

class App extends React.Component {
    constructor() {
        super();
        this.state = {
            isLoggedIn: UserStore.isLoggedIn
        };
        this.onUserDataLoad = this.onUserDataLoad.bind(this);
        this.logout = this.logout.bind(this);
    }

    onUserDataLoad(data) {
        const {history} = this.props;
        if (data.isLoggedIn) {
            history.pushState('home', '/home');
        } else {
            if(!history.isActive('login') && !history.isActive('create')) {
                history.pushState('home', '/home');
            }
        }
        this.setState({
            isLoggedIn: data.isLoggedIn || this.state.isLoggedIn
        });
    }

    logout() {
        UserStore.logout();
    }

    render() {
        var {isLoggedIn} = this.state;
        return (
            <div>
                <Navbar brand={<a href="#/home">Learn Words Fast</a>} staticTop inverse>
                    {isLoggedIn ?
                        <Nav>
                            <NavItem href="#/home">Home</NavItem>
                            <NavItem onClick={this.logout} href="#/login">Logout</NavItem>
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
}

reactMixin(App.prototype, Reflux.listenTo(UserStore, 'onUserDataLoad'));

export default App;
