import { useState } from "react";
import Button from "react-bootstrap/Button";
import Col from "react-bootstrap/Col";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import { FlightResult } from "../../types";
import FlightView from "./FlightView";

export default function Home() {
  const [searchResult, setSearchResult] = useState<FlightResult[]>([]);
  const [selectedOutwardFlight, setOutwardFlight] = useState(-1);
  const [selectedReturnFlight, setReturnFlight] = useState(-1);
  const [outwardFlights, setOutwardFlights] = useState<FlightResult[]>([
    {
      airline: "INDIGO",
      from: "BLR",
      to: "PAT",
      departure: "15/10/2023",
      arrival: "15/10/2023",
      instrument: "",
      mealType: "",
      cost: 5000,
      flightNumber: "AB-101",
    },
    {
      airline: "SPICEJET",
      from: "BLR",
      to: "PAT",
      departure: "15/10/2023",
      arrival: "15/10/2023",
      instrument: "",
      mealType: "",
      cost: 5000,
      flightNumber: "SJ-202",
    },
  ]);
  const [returnFlights, setReturnFlights] = useState<FlightResult[]>([]);

  return (
    <>
      <div className="p-3 mb-4 bg-light rounded-3">
        <h1 className="display-5 fw-bold">Plan your travel!</h1>
        <Form autoComplete="false">
          <Row className="mb-1">
            <Form.Group as={Col} controlId="formGridEmail">
              <Form.Label>Depart from</Form.Label>
              <Form.Control type="text" placeholder="Departing airport" />
            </Form.Group>

            <Form.Group as={Col} controlId="formGridPassword">
              <Form.Label>Going to</Form.Label>
              <Form.Control type="text" placeholder="Arrival airport" />
            </Form.Group>
          </Row>
          <Row className="mb-1">
            <Form.Group as={Col} className="mb-1" id="tripType">
              <Form.Check as={Col} type="radio" label="One Way" />
            </Form.Group>
            <Form.Group as={Col} className="mb-1" id="tripType">
              <Form.Check as={Col} type="radio" label="Round Trip" />
            </Form.Group>
          </Row>
          <Row className="mb-1">
            <Form.Group as={Col} controlId="departDate">
              <Form.Label>Departure Date</Form.Label>
              <Form.Control type="date" placeholder="Departure date" />
            </Form.Group>

            <Form.Group as={Col} controlId="return Date">
              <Form.Label>Return Date</Form.Label>
              <Form.Control type="date" placeholder="Return date" />
            </Form.Group>
          </Row>
          <Row className="mb-1">
            <Form.Group as={Col} controlId="formGridState">
              <Form.Label>Meal Choice</Form.Label>
              <Form.Select defaultValue="0">
                <option id="0">Select meal option</option>
                <option id="1">Veg</option>
                <option id="2">Non-Veg</option>
              </Form.Select>
            </Form.Group>
          </Row>

          <Button variant="primary" type="submit">
            Search Flights
          </Button>
        </Form>
      </div>
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
                <Form.Select defaultValue="0">
                  <option id="0">Select meal option</option>
                  <option id="1">Veg</option>
                  <option id="2">Non-Veg</option>
                </Form.Select>
              </Form.Group>
            </div>
          </div>
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
                <Form.Select defaultValue="0">
                  <option id="0">Select meal option</option>
                  <option id="1">Veg</option>
                  <option id="2">Non-Veg</option>
                </Form.Select>
              </Form.Group>
            </div>
          </div>
        </div>
      </div>
      <div className="row bg-light p-3">
        <div className="col">Total Price: Rs. 10,000</div>
        <div className="col text-end">
        <Button variant="primary" type="button">
            Continue Booking
          </Button>
        </div>
      </div>
    </>
  );
}
