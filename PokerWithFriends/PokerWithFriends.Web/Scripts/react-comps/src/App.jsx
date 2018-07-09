import React, { Component } from 'react';
import './App.css';
import Register from './Register';
import Login from './Login';
import Home from './Home';
import CreateMatch from './CreateMatch';
import {Route, Redirect, Switch} from 'react-router-dom';
// import { 
//   ToggleButton,
//   Grid, 
//   Row, 
//   Col, 
//   ToggleButtonGroup} 
//   from 'react-bootstrap';

class App extends Component {
  state = {
    login: true
  }

  handleLogin = () => {
    this.setState({
      login: true
    })
    //this.props.history.push("/home")
  }
  handleRegister = () => {
    this.setState({
      login: false
    })
  }

  render() {
    return (
      <div className="App">
        <div>
          <Switch>
            <Route path="/regiser" component={Register} />
            <Route path="/login" component={Login} />
            <Route path="/home" component={Home} />
            <Route path="/createMatch" component={CreateMatch} />
            <Redirect to="/login" />
          </Switch>
        </div>
          {/* <div id="home">
            <React.Fragment>
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
            </React.Fragment>
          </div> */}
      </div>
    );
  }
}

export default App;
