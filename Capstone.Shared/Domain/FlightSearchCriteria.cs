﻿namespace Capstone.Shared.Domain;

public class FlightSearchCriteria
{
    public string From { get; set; }

    public string To { get; set; }

    public DateOnly FromTravelDate { get; set; }

    public DateOnly? ReturnTravelDate { get; set; }

    public TripType TripType { get; set; }

    public MealType MealType { get; set; }
}