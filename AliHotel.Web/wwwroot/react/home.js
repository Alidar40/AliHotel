import React from 'react';

class Home extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            myOrdersRequested: this.props.myOrdersRequested
        }
    }

    render() {
        return <div className="container body-content">
            <h1>Hello, world!</h1>
        </div> 
    }
}

export default Home;