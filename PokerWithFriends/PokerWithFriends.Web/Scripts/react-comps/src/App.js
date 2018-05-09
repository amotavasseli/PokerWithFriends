import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import Register from './Register';
import Login from './Login';
import { 
  ToggleButton,
  Grid, 
  Row, 
  Col, 
  ToggleButtonGroup} 
  from 'react-bootstrap';

class App extends Component {
  state = {
    login: true
  }

  handleLogin = () => {
    this.setState({
      login: true
    })
  }
  handleRegister = () => {
    this.setState({
      login: false
    })
  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <h1 className="App-title">Poker With Friends</h1>
        </header>
        <Grid>
          <Row>
            <Col xs={10} sm={8} md={6} xsOffset={1} smOffset={2} mdOffset={3}>
              <ToggleButtonGroup type="radio" name="options" defaultValue={1} justified>
                <ToggleButton value={1} bsStyle="info" bsSize="large" block onClick={() => this.handleLogin()}>Login</ToggleButton>
                <ToggleButton value={2} bsStyle="info" bsSize="large" block onClick={() => this.handleRegister()}>Register</ToggleButton>
              </ToggleButtonGroup>
            </Col>
          </Row>
        </Grid>
        {
          this.state.login ? <Login /> : <Register />
        }
      </div>
    );
  }
}

export default App;
