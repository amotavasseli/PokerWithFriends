import React from 'react';
import {connect} from 'react-redux';

class Home extends React.Component {
    state = {
        matchGuid: ""
    }

    addMatch = () => {
        this.props.addMatch(this.state.matchGuid);
    }

    deleteMatch = index => {
        this.props.deleteMatch(index);
    }


    render(){
        return(
            <div>
                <button onClick={this.addMatch}>Add Match</button>
                {
                    this.props.matches.map((match, index) => 
                        <div>
                            {match.task}
                            <button onClick={() => this.deleteMatch(index)}>Delete</button>
                        </div>
                    )
                }
            </div>
        )
    }
}

function mapStateToProps(state){
    return {
        matches: state.matches
    };
}

function mapDispatchToProps(dispatch){
    return {
        addMatch: task => dispatch({type: "add match", task: task}),
        deleteMatch: index => dispatch({type: "delete match", index: index})
    };
}
export default connect(mapStateToProps, mapDispatchToProps)(Home);