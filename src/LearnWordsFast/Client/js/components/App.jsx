import React from 'react';
import { Route, RouteHandler, Link } from 'react-router';
import {Grid, Row, Col, Navbar, Nav, NavItem} from 'react-bootstrap';

export default class App extends React.Component {
    constructor() {
        super();
    }

    render() {
        return (
            <div>
                <Navbar brand={<a href="#/home">Learn Words Fast</a>} staticTop inverse>
                    <Nav>
                        <NavItem eventKey={1} href="#/home">Home</NavItem>
                        <NavItem eventKey={2} href="#/login">Login</NavItem>
                        <NavItem eventKey={3} href="#/create">Create</NavItem>
                        <NavItem eventKey={4} href="#/logout">Logout</NavItem>
                    </Nav>
                </Navbar>
                <Grid>
                    <Row>
                        <Col lg={3} md={3}>
                        </Col>
                        <Col lg={6} md={6}>
                            <RouteHandler />
                        </Col>
                        <Col lg={3} md={3}>
                        </Col>
                    </Row>
                </Grid>
            </div>
        );
    }
}
