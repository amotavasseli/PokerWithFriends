import React from "react";
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
import {getByLogin} from "./UserServices";
import ReactDOM from 'react-dom';
import Home from "./Home";

class Login extends React.Component {

    state = {
        email: "",
        password: ""
    }

    handleInput = (input, inputType) =>{
        if (inputType === "email")
            this.setState({ email: input.target.value });
        else if (inputType === "password")
            this.setState({ password: input.target.value });
    }

    handleLogin = () => {
        console.log(this.state);
        getByLogin(this.state).then(
            response => {
                console.log(response);
                ReactDOM.render(
                    <Home userdata={response} />,
                    document.getElementById("root")
                );

            },
            error => console.log(error)
        )
    }

    render(){
        return(
            <Grid className="login-registration-form">
                <Row>
                    <Col xs={10} sm={8} md={6} xsOffset={1} smOffset={2} mdOffset={3}>
                        <form>
                            <FormGroup>
                                <ControlLabel>Email</ControlLabel>
                                <FormControl
                                    type="email"
                                    value={this.state.email}
                                    placeholder="Email"
                                    onChange={e => this.handleInput(e, "email")}
                                />
                            </FormGroup>
                            <FormGroup>
                                <ControlLabel>Password</ControlLabel>
                                <FormControl
                                    type="password"
                                    value={this.state.password}
                                    placeholder="Password"
                                    onChange={e => this.handleInput(e, "password")}
                                />
                            </FormGroup>
                            <ButtonToolbar className="log-button">
                                <Button bsStyle="primary" block onClick={() => this.handleLogin()}>Login</Button>
                            </ButtonToolbar>
                        </form>
                    </Col>
                </Row>
            </Grid>
        )
    }
}
export default Login;