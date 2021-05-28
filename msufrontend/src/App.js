import "./styles/App.css";
import NavigationBar from "./components/NavigationBar";
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
import { Container } from "react-bootstrap";
import { Footer } from "antd/lib/layout/layout";
import PrivateRoute from "./routes/PrivateRoute";
import Profile from "./pages/Profile";
import EmailConfirm from "./pages/EmailConfirm";
import ChangePassword from "./pages/ChangePassword";
import Schedule from "./components/info/schedule/Schedule";
import Register from "./pages/Register";
import PasswordForgot from "./pages/PasswordForgot/PasswordForgot";

function App() {
  const dispatch = useDispatch();
  React.useEffect(() => {
    dispatch(loadUserdata());
  }, []);

  return (
    <>
      <NavigationBar />
      <Container>
          <Switch>
            <Route path="/" component={Home} exact />
            <Route path="/post/:id" component={Post} exact />
            <Route path="/faculties" component={Faculties} exact />
            <Route path="/faculty/:id" component={Faculty} exact />
            <Route
              path="/faculty/department/:id"
              component={Department}
              exact
            />
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
            <Route path="/register" component={Register} exact />
            <Route path="/passwordforgot" component={PasswordForgot} exact />
            <Route path="/emailconfirmation/:token" component={EmailConfirm} exact />
            <Route path="/passwordreset/:token" component={ChangePassword} exact />
            <PrivateRoute exact path="/profile">
              <Profile />
            </PrivateRoute>
            <PrivateRoute exact path="/schedule">
              <Schedule />
            </PrivateRoute>
            <Route component={NotFound} />
          </Switch>
      </Container>
      {/* <Footer /> */}
    </>
  );
}

export default App;
