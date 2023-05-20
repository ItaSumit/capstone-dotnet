export interface FlightSearch {
  from: string;
  to: string;
  fromTravelDate: Date;
  returnTravelDate?: Date;
  tripType: number;
  mealType: number;
}

const defaultSearch: Partial<FlightSearch> = {
  tripType: 1,
}
