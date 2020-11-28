import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';

import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import { requestOptions } from './config';

const useStyles = makeStyles((theme) => ({
  formControl: {
    margin: theme.spacing(1),
    minWidth: 120,
  },
  selectEmpty: {
    marginTop: theme.spacing(2),
  },
}));

export default function CustomButton(props) {
    const classes = useStyles();
    const [role, setRole] = React.useState(props.user_role);
    const [user_roles, ] = React.useState(["Admin","Customer"]);

    const handleChange = (event) => {

        setRole(event.target.value);

         requestOptions['method'] = "PUT"
         requestOptions['body'] = JSON.stringify({Role: event.target.value})  
    
        fetch('https://localhost:8080/api/users/' + props.user_id, requestOptions)
          .then(response => {
            if (response.status===204){
                alert("Modified")
              } else {
                console.log(response)
              }
          })
          event.preventDefault();
    };

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
              <MenuItem key={role2} value={role2}>{role2}</MenuItem>
            ))}
            </Select>
        </FormControl>
        </div>
        );
    }

