import React from 'react';
import {createNewUser} from './UserServices';
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


class Register extends React.Component {
    state = {
        firstName: "",
        lastName: "",
        password: "",
        cPassword: "",
        email: "",
        username: ""
    }
    firstName = "firstName";
    lastName = "lastName";
    handleInput = (input, inputType) => {

        if (inputType === "firstName")
            this.setState({ firstName: input.target.value });
        else if (inputType === "lastName")
            this.setState({ lastName: input.target.value });
        else if (inputType === "email")
            this.setState({ email: input.target.value });
        else if (inputType === "password")
            this.setState({ password: input.target.value });
        else if (inputType === "cPassword")
            this.setState({ cPassword: input.target.value });
        else if (inputType === "username")
            this.setState({ username: input.target.value });
    }

    handleSubmit = info => {
        if(this.state.password === this.state.cPassword){
            const userInfo = {
                firstName: this.state.firstName,
                lastName: this.state.lastName,
                password: this.state.password,
                email: this.state.email,
                username: this.state.username
            }
            createNewUser(userInfo).then(
                response => {
                    console.log(response);
                    this.handleClear();
                },
                error => console.log(error)
            )
        }

    }
    handleClear = () => {
        this.setState({
            firstName: "",
            lastName: "",
            password: "",
            email: "",
            username: ""
        });
    }
    checkPassword = () => {
        if(this.state.cPassword !== this.state.password){
            return "error"
        } else if(this.state.cPassword === this.state.password && this.state.cPassword !== "") {
            return "success";
        } else 
            return null;
    }

    render() {
        return (
            <Grid className="login-registration-form">
                <Row>
                    <Col xs={10} sm={8} md={6} xsOffset={1} smOffset={2} mdOffset={3}>
                        <form>
                            <FormGroup>
                                <ControlLabel>First Name</ControlLabel>
                                <FormControl
                                    type="text"
                                    value={this.state.firstName}
                                    placeholder="First Name"
                                    onChange={e => this.handleInput(e, "firstName")}
                                />
                            </FormGroup>
                            <FormGroup>
                                <ControlLabel>Last Name</ControlLabel>
                                <FormControl
                                    type="text"
                                    value={this.state.lastName}
                                    placeholder="Last Name"
                                    onChange={e => this.handleInput(e, "lastName")}
                                />
                            </FormGroup>
                            <FormGroup>
                                <ControlLabel>Username</ControlLabel>
                                <FormControl
                                    type="text"
                                    value={this.state.username}
                                    placeholder="Username"
                                    onChange={e => this.handleInput(e, "username")}
                                />
                            </FormGroup>
                            <FormGroup>
                                <ControlLabel>Email</ControlLabel>
                                <FormControl
                                    type="email"
                                    value={this.state.email}
                                    placeholder="Email"
                                    onChange={e => this.handleInput(e, "email")}
                                />
                            </FormGroup>
                            <FormGroup
                                controlId="formPassword"
                                validationState={this.checkPassword()}
                            >
                                <ControlLabel>Password</ControlLabel>
                                <FormControl
                                    type="password"
                                    value={this.state.password}
                                    placeholder="Password"
                                    onChange={e => this.handleInput(e, "password")}
                                />
                            </FormGroup>
                            <FormGroup
                                controlId="formCPassword"
                                validationState={this.checkPassword()}
                            >
                                <ControlLabel>Confirm Password</ControlLabel>
                                <FormControl
                                    type="password"
                                    value={this.state.cPassword}
                                    placeholder="Confirm Password"
                                    onChange={e => this.handleInput(e, "cPassword")}
                                />
                            </FormGroup>
                            <ButtonToolbar className="reg-button">
                                <Button bsStyle="primary" onClick={() => this.handleSubmit()}>Create Account</Button>
                                <Button bsStyle="danger" onClick={() => this.handleClear()}>Reset Form</Button>
                            </ButtonToolbar>

                        </form>
                    </Col>
                </Row>
            </Grid>

        )
    }
}
export default Register;