import { useEffect, useState } from "react";
import { useHistory } from "react-router";
import { Link } from "react-router-dom";
import { getToken } from "../service/adminService";

export default function Header() {
  const [token, setToken] = useState<string | null>(null);
  const history = useHistory();

  useEffect(() => {
    const token = getToken();
    setToken(token);
  }, []);

  return (
    <div>
      <nav className="navbar navbar-expand navbar-dark bg-dark px-3">
        <Link to="/" className="navbar-brand">
          Capstone Flight
        </Link>
        <div className="navbar-nav mr-auto">
          <li className="nav-item">
            <Link to={"/"} className="nav-link">
              Plan Travel
            </Link>
          </li>
        </div>
        {token ? (
          <>
            <div className="navbar-nav mr-auto">
              <li className="nav-item">
                <Link to={"/booking/history"} className="nav-link">
                  Booking History
                </Link>
              </li>
            </div>
            <div className="navbar-nav mr-auto">
              <li className="nav-item">
                <Link to={"/admin/add-airline"} className="nav-link">
                  Add airline
                </Link>
              </li>
            </div>
            <div className="navbar-nav ml-auto" style={{ marginLeft: "auto" }}>
              <li className="nav-item">
                <Link to={"/"} className="nav-link">
                  Welcome Admin
                </Link>
              </li>

              <li className="nav-item">
                <button
                  type="button"
                  className="btn btn-primary"
                  onClick={() => {
                    localStorage.removeItem("token");
                    history.push("/");
                    window.location.reload();
                  }}
                >
                  Log out
                </button>
              </li>
            </div>
          </>
        ) : (
          <>
            <div className="navbar-nav mr-auto">
              <li className="nav-item">
                <Link to={"/adminLogin"} className="nav-link">
                  Admin Login
                </Link>
              </li>
            </div>
          </>
        )}
      </nav>
    </div>
  );
}
