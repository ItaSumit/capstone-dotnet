import { useState } from "react";
import { useHistory } from "react-router";
import { adminLogin } from "../../service/adminService";

const Login = () => {
  const history = useHistory();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const onChangeUsername = (e: any) => {
    const username = e.target.value;
    setUsername(username);
  };
  const onChangePassword = (e: any) => {
    const password = e.target.value;
    setPassword(password);
  };
  const handleLogin = (e: any) => {
    e.preventDefault();

    adminLogin({ userName: username, password: password }).then(
      () => {
        history.push("/admin/add-airline");
      },
      (error) => {
        console.log(error);
      }
    );
  };
  return (
    <div className="p-5">
      <form onSubmit={handleLogin}>
        <h3>Login</h3>
        <div className="my-3">
          <label className="form-label">Email address</label>
          <input
            type="text"
            className="form-control"
            name="username"
            value={username}
            id="exampleInputEmail1"
            aria-describedby="emailHelp"
            onChange={onChangeUsername}
          />
        </div>
        <div className="mb-3">
          <label className="form-label">Password</label>
          <input
            type="password"
            name="password"
            value={password}
            className="form-control"
            onChange={onChangePassword}
          />
        </div>
        <button type="submit" className="btn btn-dark">
          Login
        </button>
      </form>

      <div className="mt-3">
        <strong>Existing Authors</strong>
        <div>flightadmin:123456</div>
      </div>
    </div>
  );
};
export default Login;
