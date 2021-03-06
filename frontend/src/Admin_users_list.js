import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import Navbar from './Navbar';
import Typography from '@material-ui/core/Typography';
import { Box } from '@material-ui/core';
import { withStyles } from '@material-ui/core/styles';
import Custombutton from './CustomButton';
import { requestOptions } from './config';

const useStyles = makeStyles({
  table: {
    minWidth: 650,
  },
});

class Users extends React.Component {
  
    constructor(props) {
        super(props);
        
        this.state = {
          role : null,
          users: [],
        }; 
        this.getUsers(); 
    }
    getUsers(){
        console.log("download")
        requestOptions['method'] = "GET"
        fetch("https://localhost:8080/api/users/", requestOptions)
        .then(response => {
                response.json().then(data =>{this.setState({users:data}); console.log(data)}) 
        });
    }
        

   
    render(){
        const { classes } = this.props;
        return (
            <React.Fragment>
            
                <Navbar />
                <Typography component="h1" variant="h2" align="center" color="textPrimary" gutterBottom style={{marginTop:80}}>User list</Typography>
                <div style={{width: '50%', margin:'auto',marginTop:100}}>
                    <TableContainer component={Paper} >
                        <Table className={classes.table} aria-label="simple table" >
                            <TableHead>
                            <TableRow>
                                <TableCell>User_id</TableCell>
                                <TableCell>User_name</TableCell>
                                <TableCell align="center">Role</TableCell>
                                {/* <TableCell align="center">Change Role </TableCell>    */}
                            </TableRow>
                            </TableHead>
                            <TableBody>
                            {this.state.users.map((user,index) => (
                                <TableRow key={user.id}>
                                    <TableCell component="th" scope="row">
                                        {user.id}
                                    </TableCell>
                                    <TableCell >{user.email}</TableCell>
                                    {/* <TableCell align="center">{user.role}</TableCell> */}
                                    <TableCell align="center"><Box><Custombutton user_id={user.id} user_role={user.role}></Custombutton> </Box> </TableCell>
                                </TableRow>
                            ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </div>
            </React.Fragment>
        );
        }
    }
export default withStyles(useStyles)(Users);