import React from 'react';
import { AppBar, Toolbar, Button, Box } from '@material-ui/core';



class NavBar extends React.Component {
  
    render() {
        return (
            <AppBar position="static">
                <Toolbar container="true" align-content="space-between">
                    
                    <Box display='flex' flexGrow={1}>
                        <Button color="inherit" href="/">Home </Button>
                    </Box>
                    <Button color="inherit" href="/">Log Out </Button>
                </Toolbar>
            </AppBar>
        );
    }
}

export default (NavBar);