import React from 'react';
import {deleteMatch} from './MatchServices';

class Home extends React.Component {
    state = {
        matchGuid: ""
    }

    addMatch = () => {
    }

    deleteMatch = (id, index) => {
        deleteMatch(id).then(
            response => {
                console.log(response);
                this.props.delete(index);
            },
            error => console.log(error)
        );
    }


    render() {
        return (
            <div>
                <h3>My Matches</h3>
                <button onClick={this.addMatch}>Add Match</button>
                {
                    this.props.matches && this.props.matches.map((match, index) => (
                        <div key={index}>
                            <h3>Match Invitation ID: {match.MatchGuid}></h3>
                            <h4>Match Start Time: {match.MatchStartTime}></h4>
                            <h5>Opponents: {match.Opponents}></h5>
                            <button onClick={() => this.deleteMatch(match.MatchId, index)}>Delete Match</button>
                        </div>
                    ))
                }
            </div>
        )
    }
}

// function mapStateToProps(state) {
//     return {
//         matches: state.matches
//     };
// }

// function mapDispatchToProps(dispatch) {
//     return {
//        // addMatch: task => dispatch({ type: "add match", task: task }),
//         delete: index => dispatch({ type: "delete match", index: index })
//     };
// }
//export default connect(mapStateToProps, mapDispatchToProps)(Home);
export default Home;