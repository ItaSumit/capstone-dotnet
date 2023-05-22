import axios from "axios";
import { FlightInput } from "../pages/admin/airline/helper";
import { FlightSearch } from "../pages/home/helper";
import {
  AdminLogin,
  BookingDetail,
  FlightBooking,
  FlightResult,
} from "../types";

export async function adminLogin(login: AdminLogin): Promise<void> {
  let response: any = await axios.post<AdminLogin>(
    "http://localhost:8000/api/v1.0/flight/admin/login",
    login
  );

  if (response.data.token) {
    localStorage.setItem("token", response.data.token);
  }

  return response.data;
}

export const getToken = () => {
  return localStorage.getItem("token");
};
export const logout = () => {
  localStorage.removeItem("token");
};
export const Islogged = () => {
  return localStorage.getItem("token") !== "" ? true : false;
};

export async function addAirline(request: FlightInput) {
  request.instrument = "Boeing";
  var response = await axios.post<number>(
    "http://localhost:8000/api/v1.0/flight/airline/register",
    request,
    {
      headers: {
        Authorization: `Bearer ${getToken()}`,
        "Content-Type": "application/json",
      },
    }
  );
  return response.data;
}
