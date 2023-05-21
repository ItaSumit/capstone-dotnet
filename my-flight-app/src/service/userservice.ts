import axios from "axios";
import { FlightSearch } from "../pages/home/helper";
import { BookingDetail, FlightBooking, FlightResult } from "../types";

export async function flightSearch(search: FlightSearch): Promise<FlightResult[]> {
    var response = await axios.post<FlightResult[]>('http://localhost:8000/api/v1.0/flight/search', search);
    return response.data;
}

export async function bookflight(request: FlightBooking) {
    var response = await axios.post<string>('http://localhost:8000/api/v1.0/flight/booking', request);
    return response.data;
}

export async function bookingHistory(email: string): Promise<BookingDetail[]> {
    var response = await axios.get<BookingDetail[]>(`http://localhost:8000/api/v1.0/flight/booking/history/${email}`);
    return response.data;
}

export async function bookingPnr(pnr: string): Promise<BookingDetail[]> {
    var response = await axios.get<BookingDetail[]>(`http://localhost:8000/api/v1.0/flight/ticket/${pnr}`);
    return response.data;
}