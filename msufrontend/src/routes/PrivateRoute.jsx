import React from "react";
import { useSelector } from "react-redux";
import { Route, Redirect } from "react-router-dom";

function PrivateRoute({ children, ...rest }) {
  const IsAuthenticated = useSelector(({ auth }) => auth.isAuthenticated);

  return (
    <Route
      {...rest}
      render={({ location }) => {
        return IsAuthenticated ? (
          children
        ) : (
          <Redirect to={{ pathname: "/login", state: { from: location } }} />
        );
      }}
    />
  );
}

export default PrivateRoute;