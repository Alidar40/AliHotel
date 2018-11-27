import React from 'react';

class Footer extends React.Component {
    constructor(props) {
        super(props);
        this.state = {}
    }

    render() {
        return <footer className="container" id="footer" >
                    <hr />
                    <p>&copy; 2018 - AliHotel Inc.</p>
                    <p>Everything is imagined. Hotel is not real. Pictures are not mine</p>
                </footer>
    }
}

export default Footer;