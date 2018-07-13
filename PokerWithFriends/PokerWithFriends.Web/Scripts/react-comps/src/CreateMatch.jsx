import React, {PropTypes} from 'react';
import {connect} from 'react-redux';
import { 
    FormGroup, 
    FormControl, 
    ControlLabel, 
    Button, 
    ButtonToolbar, 
    Grid, 
    Row, 
    Col } 
    from 'react-bootstrap';
import {createMatch} from './MatchServices';



class CreateMatch extends React.Component{
    state = {
        challengerId: 1018,
        matchStartTime: "",
        matchStartDate: "",
        winner: null,
        opponent: "",
        opponents: []
    }

    handleStartDate = date => {
        this.setState({matchStartDate: date.target.value})
    }
    handleStartTime = time => {
        this.setState({matchStartTime: time.target.value})
    }

    handleOpponents = opponent => {
        let newArr = [...this.state.opponents,parseInt(opponent)];
        this.setState({opponents: newArr});
    }
    handleOpponent = opponent => this.setState({opponent: opponent.target.value});
    handleAddOpponent = () => {
        this.handleOpponents(this.state.opponent);
    }
    handleCreateMatch = () => {
        const matchData = {
            challengerId: this.state.challengerId,
            opponents: this.state.opponents,
            winner: this.state.winner,
            matchStartTime: new Date(this.state.matchStartDate + " " + this.state.matchStartTime)
        }
        createMatch(matchData).then(
            response => console.log(response),
            error => console.log(error)
        )
    }

    render(){
        //const {match} = this.state;
        return (
            <div>
                <Grid className="login-registration-form">
                    <Row>
                        <Col xs={10} sm={8} md={6} xsOffset={1} smOffset={2} mdOffset={3}>
                            <form>
                                <FormGroup>
                                    <ControlLabel>Start Date</ControlLabel>
                                    <FormControl
                                        type="date"
                                        value={this.state.matchStartDate}
                                        onChange={e => this.handleStartDate(e)}
                                    />
                                </FormGroup>
                                <FormGroup>
                                    <ControlLabel>Start Time</ControlLabel>
                                    <FormControl
                                        type="time"
                                        value={this.state.matchStartTime}
                                        placeholder="5:00PM"
                                        onChange={e => this.handleStartTime(e)}
                                    />
                                </FormGroup>
                                <FormGroup>
                                    <ControlLabel>Opponents</ControlLabel>
                                    <FormControl 
                                        type="text"
                                        onChange={e => this.handleOpponent(e)}
                                        value={this.state.opponent}
                                    />
                                    <Button bsStyle="success" onClick={() => this.handleAddOpponent()}>Add Opponent</Button>
                                </FormGroup>
                                <ButtonToolbar>
                                    <Button bsStyle="primary" block onClick={() => this.handleCreateMatch()}>Create Match</Button>
                                </ButtonToolbar>
                            </form>
                        </Col>
                    </Row>
                </Grid>
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