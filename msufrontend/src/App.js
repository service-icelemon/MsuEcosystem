import './styles/App.css';
import Navbar from './components/Navbar';
import Home from './pages/Home';
import { Route, Switch } from "react-router-dom";
import Post from './components/news/Post';


function App() {
  return (
    <div className="App">
      <Navbar />
      <Switch>
        <Route path="/" component={Home} exact />
        <Route path="/post/:id" component={Post} exact />
      </Switch>
    </div>
  );
}

export default App;
