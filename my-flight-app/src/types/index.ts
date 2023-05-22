export interface FlightResult {
  id: number;
  flightNumber: string;
  airline: string;
  from: string;
  to: string;
  departure: string;
  arrival: string;
  instrument: string;
  cost: number;
  mealType: string;
}

export interface PassengerInfo {
  firstName: string;
  lastName: string;
  age: number;
  mealType: string;
  seatNumber: number;
}

export interface FlightBooking {
  fromFlightId: number;
  returnFlightId?: number;
  userEmail: string;
  passengers: PassengerInfo[];
  fromTravelDate: string;
  tripType: string;
  returnTravelDate?: string;
}

export interface BookingDetail {
  bookingId: 5;
  airline: string;
  flightNumer: string;
  pnr: string;
  passengers: PassengerInfo[];
  from: string;
  to: string;
  departureDate: string;
  departureTime: string;
  arraivalDate: string;
  arrivalTime: string;
  emailId: string;
  direction: string;
  status: string;
}

export interface AdminLogin {
  userName: string;
  password: string;
}
