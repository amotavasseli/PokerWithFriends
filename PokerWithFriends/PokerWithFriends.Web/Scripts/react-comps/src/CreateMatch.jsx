import React, {PropTypes} from 'react';
import {connect} from 'react-redux';

class CreateMatch extends React.Component{
    state = {
        challengerId: 1018,
        matchStartTime: "",
        matchStartDate: "",
        winner: null,
        opponent: "",
        opponents: [22555, 33432, 55555]
    }

    handleStartDate = date => {
        this.setState({matchStartDate: date.target.value})
    }
    handleStartTime = time => {
        this.setState({matchStartTime: time.target.value})
    }

    handleOpponents = opponent => {
        let newArr = [...this.state.opponents,opponent];
        this.setState({opponents: newArr});
    }
    handleOpponent = opponent => this.setState({opponent: opponent.target.value});
    handleAddOpponent = () => {
        this.handleOpponents(this.state.opponent);
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
                    <label>Opponent</label>
                    <input 
                        type="text"
                        onChange={e => this.handleOpponent(e)}
                        value={this.state.opponent}
                    />
                    <button onClick={() => this.handleAddOpponent()}>Add Opponent</button>
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