import React from 'react';

const Main = React.createClass({
   render() {
       return (
           <div>Hello world</div>
       );
   }
});

React.render(<Main />, document.getElementById('app'));
