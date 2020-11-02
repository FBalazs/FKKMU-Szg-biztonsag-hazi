import logo from './logo.svg';
import './App.css';
import SignIn from './signIn';
import SignUp from './signup';
import { Link, Route, Switch } from "react-router-dom";
import Album from './album';
import { AppBar, Toolbar, Button } from '@material-ui/core';


function App() {
  return (
    <div className="App">
      
    
      <Switch>
        <Route exact path="/"><SignIn /></Route>
        <Route path="/SignUp"><SignUp /></Route>
        <Route path="/Album">
        <AppBar position="static">
          <Toolbar>    
          <Button color="inherit" href="/">Log Out </Button>
          </Toolbar>
        </AppBar>
        <Album /></Route>
        
      </Switch>
      
    </div>
  );
}

export default App;
