import React from 'react';
import { FormGroup, FormControl, ControlLabel, Button, ButtonToolbar, Grid, Row, Col } from 'react-bootstrap';

class Register extends React.Component {
    state = {
        firstName: "",
        lastName: "",
        password: "",
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
        else if (inputType === "username")
            this.setState({ username: input.target.value });
    }

    handleSubmit = info => {
        console.log(this.state);
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

    render() {
        return (
            <Grid className="registration-form">
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
                                    type="text"
                                    value={this.state.email}
                                    placeholder="Email"
                                    onChange={e => this.handleInput(e, "email")}
                                />
                            </FormGroup>
                            <FormGroup>
                                <ControlLabel>Password</ControlLabel>
                                <FormControl
                                    type="text"
                                    value={this.state.password}
                                    placeholder="Password"
                                    onChange={e => this.handleInput(e, "password")}
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