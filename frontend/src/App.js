import './App.css';
import SignIn from './SignIn';
import SignUp from './SignUp';
import { Route, Switch } from "react-router-dom";
import Album from './Album';
import Item from './Item';
import PrivateRoute from "./PrivateRoute"


function App() {
 

  return (
    <div className="App">
      <Switch>
        <Route exact path="/" component={SignIn}></Route>
        <Route path="/SignUp" component={SignUp}></Route>
        <PrivateRoute path="/Album/:itemid" component={Item}/>
        <PrivateRoute path="/Album" component={Album} />
        <Route>404 Page not found</Route>
      </Switch>
    </div>
  );
}

export default App;
