import React, {PropTypes} from 'react';
import {connect} from 'react-redux';

class CreateMatch extends React.Component{
    state = {
        challengerId: 1018,
        matchStartTime: "",
        winner: null,
        opponents: []
    }

    handleStartTime = input => {
        this.setState({matchStartTime: input.target.value})
    }

    handleStartTime = input => {
        this.setState({opponents: input.target.value})
    }

    render(){
        return (
            <div>
                <h1>Create New Match</h1>
                <div>
                    <label>Start Time</label>
                    <input 
                        type="text"
                        onChange={e => this.handleStartTime(e)}
                        value={this.state.matchStartTime} />
                </div>
                <div>
                    <label>Opponents</label>
                    <input 
                        type="text"
                        onChange={e => this.handleOpponents(e)}
                        value={this.state.opponents}
                    />
                </div>
            </div>
        )
    }
}

function mapStateToProps(state, ownProps){
    return {
        matches: state.matches
    };
}

//function mapDispatchToProps()
export default connect(mapStateToProps)(CreateMatch);