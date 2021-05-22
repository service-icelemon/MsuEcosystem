import './styles/App.css';
import Navbar from './components/Navbar';
import Home from './pages/Home';
import Faculties from './pages/Faculties';
import Faculty from './components/info/faculty/Faculty';
import { Route, Switch } from "react-router-dom";
import Post from './components/news/Post';


function App() {
  return (
    <div className="App">
      <Navbar />
      <Switch>
        <Route path="/" component={Home} exact />
        <Route path="/post/:id" component={Post} exact />
        <Route path="/faculties" component={Faculties} exact />
        <Route path="/faculty/:id" component={Faculty} exact />
      </Switch>
    </div>
  );
}

export default App;
