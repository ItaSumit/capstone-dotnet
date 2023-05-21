export interface FlightInput {
  flightNumber: string;
  airline: string;
  from: string;
  to: string;
  startAt: string;
  endAt: string;
  days: string[];
  instrument: string;
  businessClassSeats: number;
  nonBusinessClassSeats: number;
  rows: number;
  cost: number;
  isVeg: boolean;
  isNonVeg: boolean;
}

const defaultAddFlight: FlightInput = {
  flightNumber: "",
  airline: "",
  from: "",
  to: "",
  startAt: "",
  endAt: "",
  days: [
    "Sunday",
    "Monday",
    "Tuesday",
    "Wednusday",
    "Thursday",
    "Friday",
    "Saturday",
  ],
  instrument: "",
  businessClassSeats: 30,
  nonBusinessClassSeats: 120,
  rows: 30,
  cost: 5000,
  isVeg: false,
  isNonVeg: true,
};

export { defaultAddFlight };
