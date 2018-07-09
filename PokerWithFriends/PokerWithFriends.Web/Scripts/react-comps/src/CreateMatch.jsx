import React, {PropTypes} from 'react';
import {connect} from 'react-redux';

class CreateMatch extends React.Component{
    state = {
        challengerId: 1018,
        matchStartTime: "",
        matchStartDate: "",
        winner: null,
        opponents: [22555, 33432]
    }

    handleStartDate = date => {
        this.setState({matchStartDate: date.target.value})
    }
    handleStartTime = time => {
        this.setState({matchStartTime: time.target.value})
    }

    handleOpponents = opponent => {
        this.setState({opponents: opponent.target.value})
    }

    render(){
        return (
            <div>
                <h1>Create New Match</h1>
                <div>
                    <label>Start Date</label>
                    <input 
                        type="date"
                        onChange={e => this.handleStartDate(e)}
                        value={this.state.matchStartDate} />
                </div>
                <div>
                    <label>Start Time</label>
                    <input
                        type= "time"
                        onChange = {e => this.handleStartTime(e)}
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
                <div>
                    {
                        this.state.opponents && this.state.opponents.map((opponent, index) => (
                            <h3 key={index}>{opponent}</h3>
                        ))
                    }
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