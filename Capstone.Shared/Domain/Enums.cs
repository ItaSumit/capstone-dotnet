using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Capstone.Shared.Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MealType
{
    [Display(Name = "Vegeterian")]
    Veg = 1,

    [Display(Name = "Non Vegeterian")]
    NonVeg
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TripType
{
    OneWay = 1,
    RoundTrip
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Direction
{
    Up = 1,
    Down
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status
{
    Confirmed = 1,
    Cancelled
}