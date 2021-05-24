import React from "react";
import { useDispatch } from "react-redux";
import { login } from "../redux/actions/auth";

function Login() {
  const dispatch = useDispatch();
  const [email, setEmail] = React.useState("");
  const [password, setPassword] = React.useState("");

  const onSubmit = (e) => {
    e.preventDefault();
    dispatch(login(email, password));
  };

  return (
    <div>
      <form>
        <div>
          <label>EMAIL:</label>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>
        <div>
          <label>PASSWORD:</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        <button onClick={(e) => onSubmit(e)}>Войти</button>
      </form>
    </div>
  );
}

export default Login;
