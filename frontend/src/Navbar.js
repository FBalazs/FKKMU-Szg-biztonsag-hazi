import React from 'react';
import { AppBar, Toolbar, Button, Box } from '@material-ui/core';
import UploadDialog from "./UploadDialog"


class NavBar extends React.Component {
  
    logout(){
      sessionStorage.clear();
      window.location = "/";
    }

    render() {
        return (
            <AppBar position="static">
                <Toolbar container="true" align-content="space-between">
                    
                    <Box display='flex' flexGrow={1}>
                        <Box mr={2}><Button  color="inherit" variant="outlined" href="/Album">Home </Button></Box>
                        <UploadDialog />    
                    </Box>
                    {sessionStorage.getItem("role") === "Admin" &&
                    <Box mr={2}><Button  color="inherit" variant="outlined" href="/Users">Users </Button></Box>}
                    <Button color="inherit" variant="outlined" onClick={this.logout}>Log Out </Button>
                    
                </Toolbar>
            </AppBar>
        );
    }
}

export default (NavBar);