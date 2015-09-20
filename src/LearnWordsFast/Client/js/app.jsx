import React from 'react';
import Router, {Route, IndexRoute, Redirect} from 'react-router';
import App from './components/App';
import Login from './components/Login';
import CreateUser from './components/CreateUser';
import Home from './components/Home';
import {UserStore} from './stores/UserStore';

function requireAuth(nextState, replaceState) {
    if (!UserStore.isLoggedIn) {
        replaceState({nextPathname: nextState.location.pathname}, '/login');
    }
}

React.render(<Router>
    <Route path="/" component={App}>
        <IndexRoute component={Home} onEnter={requireAuth} />
        <Route path="home" component={Home} onEnter={requireAuth} />
        <Route path="login" component={Login} />
        <Route path="create" component={CreateUser} />
        <Redirect from="*" to='/home'/>
    </Route>
</Router>, document.getElementById('app'));
