import { PassengerInfo } from "../../types";

export interface FlightSearch {
  from: string;
  to: string;
  fromTravelDate: string;
  returnTravelDate?: string;
  tripType: string;
  mealType: string;
}


const MealStringMap: Record<string, number> = {};
MealStringMap['Select meal option'] = 0;
MealStringMap['Veg'] = 1;
MealStringMap['NonVeg'] = 2;

const MealNumberMap: Record<number, string> = {};
MealNumberMap[9] = 'Select meal option';
MealNumberMap[1] = 'Veg';
MealNumberMap[2] = 'NonVeg';

const defaultSearch: FlightSearch = {
  tripType: "OneWay",
  from: "BLR",
  to: "DEL",
  fromTravelDate: new Date().toISOString().split('T')[0],
  mealType: "Select meal option",
};

const defaultPassenger: PassengerInfo = {
  firstName: '',
  lastName: '',
  age: 25,
  mealType: 'Veg',
  seatNumber: 1
}

export { defaultSearch, MealNumberMap, MealStringMap, defaultPassenger };
