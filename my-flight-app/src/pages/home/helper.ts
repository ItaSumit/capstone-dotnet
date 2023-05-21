export interface FlightSearch {
  from: string;
  to: string;
  fromTravelDate: string;
  returnTravelDate?: string;
  tripType: number;
  mealType: number;
}


const MealStringMap: Record<string, number> = {};
MealStringMap['Select meal option'] = 0;
MealStringMap['Veg'] = 1;
MealStringMap['Non-Veg'] = 2;

const MealNumberMap: Record<number, string> = {};
MealNumberMap[9] = 'Select meal option';
MealNumberMap[1] = 'Veg';
MealNumberMap[2] = 'Non-Veg';

const defaultSearch: FlightSearch = {
  tripType: 1,
  from: "BLR",
  to: "PAT",
  fromTravelDate: new Date().toISOString().split('T')[0],
  mealType: 0,
};

export { defaultSearch, MealNumberMap, MealStringMap };
