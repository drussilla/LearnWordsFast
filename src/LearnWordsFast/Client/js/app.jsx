import React from 'react';
import Router, {Route, NotFoundRoute} from 'react-router';
import App from './components/App';
import RouterContainer from './services/RouterContainer';
import readCookie from './helpers/readCookie';
import Login from './components/Login';
import CreateUser from './components/CreateUser';
import Home from './components/Home';

var routes = (
    <Route name="app" path="/" handler={App}>
        <Route name="login" handler={Login}/>
        <Route name="create" handler={CreateUser}/>
        <Route name="home" handler={Home}/>
        <NotFoundRoute handler={Home}/>
    </Route>
);

var router = Router.create({routes});
RouterContainer.set(router);

router.run((Handler) => {
    React.render(<Handler />, document.getElementById('app'));
});
