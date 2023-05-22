import "./App.css";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import Header from "./component/Header";
import Home from "./pages/home/Home";
import Layout from "./component/Layout";
import AddAirline from "./pages/admin/airline/AddAirline";
import History from "./pages/booking/History";
import Login from "./pages/admin/Login";
import { useEffect, useState } from "react";
import { getToken } from "./service/adminService";

function App() {
  
  return (
    <BrowserRouter>
      <Header />
      <Layout>
        <Switch>
          <Route exact={true} path="/">
            <Home />
          </Route>
          <Route exact={true} path="/adminLogin">
            <Login />
          </Route>
          <Route exact={true} path="/admin/add-airline">
            <AddAirline />
          </Route>
          <Route path="/booking/history/:id?">
            <History />
          </Route>
        </Switch>
      </Layout>
    </BrowserRouter>
  );
}

export default App;
