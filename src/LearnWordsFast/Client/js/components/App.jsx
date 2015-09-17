import React from 'react';
import { Route, RouteHandler, Link } from 'react-router';
import {Grid, Row, Col, Navbar, Nav, NavItem} from 'react-bootstrap';

export default class App extends React.Component {
    constructor() {
        super();
    }

    render() {
        return (
            <Grid>
                <Row>
                    <Col lg={12} md={12}>
                        <Navbar brand="Learn Words Fast">
                            <Nav>
                                <NavItem eventKey={1} href="#/login">Login</NavItem>
                                <NavItem eventKey={2} href="#/create">Create</NavItem>
                                <NavItem eventKey={3} href="#/home">Home</NavItem>
                            </Nav>
                        </Navbar>
                    </Col>
                </Row>
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
        );
    }
}
