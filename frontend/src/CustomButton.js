import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';

import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';

const useStyles = makeStyles((theme) => ({
  formControl: {
    margin: theme.spacing(1),
    minWidth: 120,
  },
  selectEmpty: {
    marginTop: theme.spacing(2),
  },
}));

const requestOptions = {
    headers: {
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + sessionStorage.getItem("token"),
    },
  };

export default function CustomButton() {
    const classes = useStyles();
    const [role, setRole] = React.useState('');
    const [user_roles, setUserRoles] = React.useState(["GOD","PEASANT"]);


    const getRoles = () => {
        
        requestOptions['method'] = "GET"
        fetch("https://localhost:8080/api/roles/", requestOptions)
        .then(response => {
                response.json().then(data =>{setUserRoles(data); console.log(data)}) 
        });
        }

    const handleChange = (event) => {

        setRole(event.target.value);

        const requestOptions = {
            method: 'PUT',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify({role: this.state.role})       
          };
    
          fetch('https://localhost:8080/api/user/role', requestOptions)
            .then(response => {
                if (response.status===200){
                  response.json().then(data =>{ console.log(data) })
                } else {
                    console.log(response)
                  }
            })
                    //.then(data => {console.log(data)});
          event.preventDefault();
    };

   // requestOptions['method'] = "GET"
   // fetch("https://localhost:8080/api/roles/", requestOptions)
   // .then(response => {
   //         response.json().then(data =>{setUserRoles(data); console.log(data)}) 
   // });
    

    return (
        <div>
        <FormControl className={classes.formControl}>
            <InputLabel id="select-label">Roles</InputLabel>
            <Select

            labelId="select-label"
            id="select"
            value={role}
            onChange={handleChange}
            >
            {user_roles.map((role2)=>
            (
            <MenuItem value={role2}>{role2}</MenuItem>
            ))}
            </Select>
        </FormControl>
        </div>
        );
    }

