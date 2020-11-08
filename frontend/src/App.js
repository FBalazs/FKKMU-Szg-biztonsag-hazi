import './App.css';
import SignIn from './SignIn';
import SignUp from './SignUp';
import { Route, Switch } from "react-router-dom";
import Album from './Album';
import Item from './Item';


function App() {
  return (
    <div className="App">
      <Switch>
        <Route exact path="/"><SignIn /></Route>
        <Route path="/SignUp"><SignUp /></Route>
          <Route path="/Album/:itemid" component={Item} />
          <Route path="/Album" component={Album} />
      </Switch>
      
    </div>
  );
}

export default App;
