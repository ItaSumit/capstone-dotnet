import "./App.css";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import Header from "./component/Header";
import Home from "./pages/home/Home";
import Layout from "./component/Layout";
import AddAirline from "./pages/admin/airline/AddAirline";
function App() {
  return (
    <BrowserRouter>
      <Header />
      <Layout>
        <Switch>
          <Route exact={true} path="/">
            <Home />
          </Route>
          <Route exact={true} path="/hello">
            <div>hello</div>
          </Route>
          <Route exact={true} path="/admin/add-airline">
            <AddAirline />
          </Route>
        </Switch>
      </Layout>
    </BrowserRouter>
  );
}

export default App;
