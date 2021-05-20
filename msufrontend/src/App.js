import './styles/App.css';
import Navbar from './components/Navbar';
import Home from './pages/Home';
import { Route, Switch } from "react-router-dom";
import Article from './components/news/Article';


function App() {
  return (
    <div className="App">
      <Navbar />
      <Switch>
        <Route path="/" component={Home} exact />
        <Route path="/article/:id" component={Article} exact />
      </Switch>
    </div>
  );
}

export default App;
