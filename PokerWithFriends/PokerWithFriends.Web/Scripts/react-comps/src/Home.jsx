import React from 'react';
//import {connect} from 'react-redux';
import {deleteMatch} from './MatchServices';

class Home extends React.Component {
    state = {
        matchGuid: ""
    }

    addMatch = () => {
    }

    deleteMatch = id => {
        deleteMatch(id).then(
            response => console.log(response),
            error => console.log(error)
        );
    }


    render() {
        return (
            <div>
                <h3>My Matches</h3>
                <button onClick={this.addMatch}>Add Match</button>
                {
                    this.props.userdata && this.props.userdata.map((match, index) => (
                        <div key={match.MatchId}>
                            <h3>Match Invitation ID: {match.MatchGuid}></h3>
                            <h4>Match Start Time: {match.MatchStartTime}></h4>
                            <h5>Opponents: {match.Opponents}></h5>
                            <button onClick={() => this.deleteMatch(match.MatchId)}>Delete Match</button>
                        </div>
                    ))
                }
            </div>
        )
    }
}

function mapStateToProps(state) {
    return {
        matches: state.matches
    };
}

function mapDispatchToProps(dispatch) {
    return {
        addMatch: task => dispatch({ type: "add match", task: task }),
        deleteMatch: index => dispatch({ type: "delete match", index: index })
    };
}
//export default connect(mapStateToProps, mapDispatchToProps)(Home);
export default Home;