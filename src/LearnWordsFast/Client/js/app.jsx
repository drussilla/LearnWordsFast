import React from 'react';
import Router, {Route, IndexRoute, Redirect} from 'react-router';
import App from './components/App';
import Login from './components/Login';
import CreateUser from './components/CreateUser';
import Home from './components/Home';
import {UserStore} from './stores/UserStore';

const requireAuth = (nextState, replaceState) => {
    if (!UserStore.isLoggedIn) {
        replaceState({nextPathname: nextState.location.pathname}, '/login');
    }
};

const redirectHome = (nextState, replaceState) => {
    if (UserStore.isLoggedIn) {
        replaceState({nextPathName: nextState.location.pathname}, '/home');
    }
};

React.render(<Router>
    <Route path="/" component={App}>
        <IndexRoute component={Home} onEnter={requireAuth}/>
        <Route path="home" component={Home} onEnter={requireAuth}/>
        <Route path="login" component={Login} onEnter={redirectHome}/>
        <Route path="create" component={CreateUser} onEnter={redirectHome}/>
        <Redirect from="*" to='/home'/>
    </Route>
</Router>, document.getElementById('app'));
