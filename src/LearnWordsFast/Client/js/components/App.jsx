import React from 'react';
import { Route, RouteHandler, Link } from 'react-router';

export default class App extends React.Component {
    constructor() {
        super();
    }

    render() {
        return (
            <div className="container">
                <nav className="navbar navbar-default">
                    <div className="navbar-header">
                        <Link to='home'>Home</Link>
                        <Link to='create'>Create</Link>
                        <Link to='login'>Login</Link>
                    </div>
                </nav>
                <RouteHandler />
            </div>
        );
    }
}
