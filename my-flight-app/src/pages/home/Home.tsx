import { useEffect, useState } from "react";
import Button from "react-bootstrap/Button";
import Col from "react-bootstrap/Col";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import { FlightResult, PassengerInfo } from "../../types";
import FlightView from "./FlightView";
import {
  FlightSearch,
  defaultSearch,
  MealNumberMap,
  MealStringMap,
  defaultPassenger,
} from "./helper";
import { bookflight, flightSearch } from "../../service/userservice";
import { useHistory } from "react-router-dom";

export default function Home() {
  const history = useHistory();
  const [searchCriteria, setSearchCriteria] =
    useState<FlightSearch>(defaultSearch);
  const [searchResult, setSearchResult] = useState<FlightResult[]>([]);
  const [selectedOutwardFlight, setOutwardFlight] = useState(-1);
  const [selectedReturnFlight, setReturnFlight] = useState(-1);
  const [totalCost, setTotalCost] = useState(0);
  const [email, setEmail] = useState("");
  const [outwardFlights, setOutwardFlights] = useState<FlightResult[]>([]);
  const [returnFlights, setReturnFlights] = useState<FlightResult[]>([]);
  const [passenger, setPassenger] = useState<PassengerInfo>(defaultPassenger);
  const [passengers, setPassengers] = useState<PassengerInfo[]>([]);

  function handleSearch(e: any) {
    e.preventDefault();
    console.log({ searchCriteria });
    flightSearch(searchCriteria)
      .then((flights) => {
        setOutwardFlights(
          flights.filter((f) => f.from === searchCriteria.from)
        );
        setReturnFlights(flights.filter((f) => f.from === searchCriteria.to));

        if (flights.length === 0) {
          alert("No flights found");
        }
      })
      .catch((error) => alert("Errored: \r\n\r\n" + JSON.stringify(error)));
  }

  function handlePassengerSubmit(e: any) {
    e.preventDefault();
    setPassengers((prev) => [
      ...prev,
      { ...passenger, seatNumber: prev.length + 1 },
    ]);
    setPassenger({ ...defaultPassenger });
  }

  function handleFlightBooking() {
    bookflight({
      fromFlightId: outwardFlights[selectedOutwardFlight].id,
      fromTravelDate: searchCriteria.fromTravelDate,
      passengers: passengers,
      tripType: searchCriteria.tripType,
      userEmail: email,
      returnFlightId:
        searchCriteria.tripType === "RoundTrip"
          ? returnFlights[selectedReturnFlight].id
          : undefined,
      returnTravelDate:
        searchCriteria.tripType === "RoundTrip"
          ? searchCriteria.returnTravelDate
          : undefined,
    }).then((pnr) => {
      history.push(`/booking/history/${pnr}`);
    });
  }

  useEffect(() => {
    let outwardCost = 0;
    let returnCost = 0;

    if (selectedOutwardFlight > -1) {
      outwardCost = outwardFlights[selectedOutwardFlight].cost;
    }

    if (selectedReturnFlight > -1) {
      returnCost = returnFlights[selectedReturnFlight].cost;
    }

    setTotalCost(outwardCost + returnCost);
  }, [selectedOutwardFlight, selectedReturnFlight]);

  return (
    <>
      <div className="p-3 mb-4 bg-light rounded-3">
        <h1 className="display-5 fw-bold">Plan your travel!</h1>
        <Form autoComplete="false" onSubmit={handleSearch}>
          <Row className="mb-1">
            <Form.Group as={Col} controlId="formGridEmail">
              <Form.Label>Depart from</Form.Label>
              <Form.Control
                type="text"
                placeholder="Departing airport"
                required
                value={searchCriteria.from}
                onChange={(e) =>
                  setSearchCriteria({ ...searchCriteria, from: e.target.value })
                }
              />
            </Form.Group>

            <Form.Group as={Col} controlId="formGridPassword">
              <Form.Label>Going to</Form.Label>
              <Form.Control
                type="text"
                placeholder="Arrival airport"
                required
                value={searchCriteria.to}
                onChange={(e) =>
                  setSearchCriteria({ ...searchCriteria, to: e.target.value })
                }
              />
            </Form.Group>
          </Row>
          <Row className="mb-1">
            <Form.Group as={Col} className="mb-1" id="tripType">
              <input
                type="radio"
                className="form-check-input"
                checked={searchCriteria.tripType === "OneWay"}
                onChange={(e) => {
                  if (e.target.checked) {
                    setSearchCriteria({
                      ...searchCriteria,
                      tripType: "OneWay",
                    });
                  }
                }}
              />{" "}
              One Way
            </Form.Group>
            <Form.Group as={Col} className="mb-1" id="tripType">
              <input
                type="radio"
                className="form-check-input"
                checked={searchCriteria.tripType === "RoundTrip"}
                onChange={(e) => {
                  if (e.target.checked) {
                    setSearchCriteria({
                      ...searchCriteria,
                      tripType: "RoundTrip",
                    });
                  }
                }}
              />{" "}
              Return Trip
            </Form.Group>
          </Row>
          <Row className="mb-1">
            <Form.Group as={Col} controlId="departDate">
              <Form.Label>Departure Date</Form.Label>
              <Form.Control
                type="date"
                placeholder="Departure date"
                required
                value={searchCriteria.fromTravelDate}
                onChange={(e) =>
                  setSearchCriteria({
                    ...searchCriteria,
                    fromTravelDate: e.target.value,
                  })
                }
              />
            </Form.Group>

            {searchCriteria.tripType === "RoundTrip" && (
              <Form.Group as={Col} controlId="return Date">
                <Form.Label>Return Date</Form.Label>
                <Form.Control
                  type="date"
                  placeholder="Return date"
                  value={searchCriteria.returnTravelDate}
                  onChange={(e) =>
                    setSearchCriteria({
                      ...searchCriteria,
                      returnTravelDate: e.target.value,
                    })
                  }
                />
              </Form.Group>
            )}
          </Row>
          <Row className="mb-1">
            <Form.Group as={Col} controlId="formGridState">
              <Form.Label>Meal Choice</Form.Label>
              <Form.Select
                value={searchCriteria.mealType}
                onChange={(e) => {
                  console.log({ val: e.target.value });
                  setSearchCriteria({
                    ...searchCriteria,
                    mealType: e.target.value,
                  });
                }}
              >
                <option>Select meal option</option>
                <option>Veg</option>
                <option>NonVeg</option>
              </Form.Select>
            </Form.Group>
          </Row>

          <Button variant="primary" type="submit">
            Search Flights
          </Button>
        </Form>
      </div>
      {outwardFlights.length > 0 && (
        <>
          <div className="p-3 mb-2 rounded-3">
            <div className="row">
              <div className="col">
                <h3 className="fw-bold">Onward Journey</h3>
                {outwardFlights.map((f, idx) => (
                  <FlightView
                    flight={f}
                    selected={idx === selectedOutwardFlight}
                    onSelect={(_) => setOutwardFlight(idx)}
                  />
                ))}
                <div>
                  <Form.Group as={Col} controlId="formGridState">
                    <Form.Label>Meal Choice</Form.Label>
                    <Form.Select value={searchCriteria.mealType}>
                      <option>Select meal option</option>
                      <option>Veg</option>
                      <option>NonVeg</option>
                    </Form.Select>
                  </Form.Group>
                </div>
              </div>
              {searchCriteria.tripType === "RoundTrip" && (
                <div className="col">
                  <h3 className="fw-bold">Return Journey</h3>
                  {returnFlights.map((f, idx) => (
                    <FlightView
                      flight={f}
                      selected={idx === selectedReturnFlight}
                      onSelect={(_) => setReturnFlight(idx)}
                    />
                  ))}
                  <div>
                    <Form.Group as={Col} controlId="formGridState">
                      <Form.Label>Meal Choice</Form.Label>
                      <Form.Select value={searchCriteria.mealType}>
                        <option id="0">Select meal option</option>
                        <option id="1">Veg</option>
                        <option id="2">NonVeg</option>
                      </Form.Select>
                    </Form.Group>
                  </div>
                </div>
              )}
            </div>
          </div>

          <div className="bg-light p-3 mb-3">
            <h3>Contact Information</h3>
            <div className="row">
              <Form.Group as={Col} controlId="formGridEmail">
                <Form.Label>Email</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="email"
                  required
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                />
              </Form.Group>
            </div>

            <h3>Passenger Information</h3>
            <div className="row">
              <div className="col">
                <strong>Firstname</strong>
              </div>
              <div className="col">
                <strong>Lastname</strong>
              </div>
              <div className="col">
                <strong>Age</strong>
              </div>
              <div className="col">
                <strong>Meal</strong>
              </div>
            </div>

            {passengers.map((p) => (
              <div className="row">
                <div className="col">{p.firstName}</div>
                <div className="col">{p.lastName}</div>
                <div className="col">{p.age}</div>
                <div className="col">{p.mealType}</div>
              </div>
            ))}

            <h4 className="mt-4">Add new passenger</h4>
            <Form autoComplete="off" onSubmit={handlePassengerSubmit}>
              <Row className="mb-1">
                <Form.Group as={Col} controlId="formGridEmail">
                  <Form.Label>First name</Form.Label>
                  <Form.Control
                    type="text"
                    placeholder="Firstname"
                    required
                    value={passenger.firstName}
                    onChange={(e) =>
                      setPassenger({
                        ...passenger,
                        firstName: e.target.value,
                      })
                    }
                  />
                </Form.Group>

                <Form.Group as={Col} controlId="formGridEmail">
                  <Form.Label>Last name</Form.Label>
                  <Form.Control
                    type="text"
                    placeholder="Lastname"
                    required
                    value={passenger.lastName}
                    onChange={(e) =>
                      setPassenger({
                        ...passenger,
                        lastName: e.target.value,
                      })
                    }
                  />
                </Form.Group>
                <Form.Group as={Col} controlId="formGridEmail">
                  <Form.Label>Age</Form.Label>
                  <Form.Control
                    type="text"
                    placeholder="Age"
                    required
                    value={passenger.age}
                    onChange={(e) =>
                      setPassenger({
                        ...passenger,
                        age: parseFloat(e.target.value),
                      })
                    }
                  />
                </Form.Group>
                <Form.Group as={Col} controlId="formGridState">
                  <Form.Label>Meal Choice</Form.Label>
                  <Form.Select
                    value={passenger.mealType}
                    onChange={(e) => {
                      setPassenger({
                        ...passenger,
                        mealType: e.target.value,
                      });
                    }}
                  >
                    <option>Select meal option</option>
                    <option>Veg</option>
                    <option>NonVeg</option>
                  </Form.Select>
                </Form.Group>
                <Form.Group as={Col} controlId="formGridState">
                  <Button
                    variant="primary"
                    type="submit"
                    style={{ marginTop: "30px" }}
                  >
                    +
                  </Button>
                </Form.Group>
              </Row>
            </Form>
          </div>
          <div className="row bg-light p-3">
            <div className="col">Total Price: Rs. {totalCost}</div>
            <div className="col text-end">
              <Button
                variant="primary"
                type="button"
                onClick={handleFlightBooking}
              >
                Confirm Booking
              </Button>
            </div>
          </div>
        </>
      )}
    </>
  );
}
