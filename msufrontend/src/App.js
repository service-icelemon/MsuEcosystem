import "./styles/App.css";
import Navbar from "./components/Navbar";
import Home from "./pages/Home";
import Faculties from "./pages/Faculties";
import Faculty from "./components/info/faculty/Faculty";
import Department from "./components/info/department/Department";
import Teacher from "./components/info/teacher/Teacher";
import { Route, Switch } from "react-router-dom";
import Post from "./components/news/Post";
import Speciality from "./components/info/speciality/Speciality";
import NotFound from "./pages/NotFound";
import Login from "./pages/Login";
import React from "react";
import { useDispatch } from "react-redux";
import { loadUserdata } from "./redux/actions/auth";

function App() {
  const dispatch = useDispatch();
  React.useEffect(() => {
    dispatch(loadUserdata());
  }, []);

  return (
    <div className="App">
      <Navbar />
      <Switch>
        <Route path="/" component={Home} exact />
        <Route path="/post/:id" component={Post} exact />
        <Route path="/faculties" component={Faculties} exact />
        <Route path="/faculty/:id" component={Faculty} exact />
        <Route path="/faculty/department/:id" component={Department} exact />
        <Route
          path="/faculty/department/teacher/:id"
          component={Teacher}
          exact
        />
        <Route
          path="/faculty/department/speciality/:id"
          component={Speciality}
          exact
        />
        <Route path="/login" component={Login} exact />
        <Route component={NotFound} />
      </Switch>
    </div>
  );
}

export default App;
