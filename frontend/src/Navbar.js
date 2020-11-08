import React from 'react';
import { AppBar, Toolbar, Button } from '@material-ui/core';



class NavBar extends React.Component {
  
    render() {
        return (
            <AppBar position="static">
                <Toolbar container="true" align-content="space-between">
                    <Button color="inherit" href="/">Home </Button>
                    <Button color="inherit" href="/">Log Out </Button>
                </Toolbar>
            </AppBar>
        );
    }
}

export default (NavBar);