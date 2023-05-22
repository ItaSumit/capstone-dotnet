import Button from "react-bootstrap/Button";
import Col from "react-bootstrap/Col";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import { FlightInput, defaultAddFlight } from "./helper";
import { useState } from "react";
import { addAirline } from "../../../service/adminService";

const DAYS = [
  "Sunday",
  "Monday",
  "Tuesday",
  "Wednesday",
  "Thursday",
  "Friday",
  "Saturday",
];

export default function AddAirline() {
  const [airline, setAirline] = useState<FlightInput>(defaultAddFlight);

  function handleSubmit(e: any) {
    e.preventDefault();
    console.log({ airline });
    addAirline(airline).then((data) => {
      alert(`Airline added ${data}`);
    });
  }

  return (
    <div className="p-3 mb-4 bg-light rounded-3">
      <h1 className="display-5 fw-bold">Add Airline</h1>
      <Form autoComplete="false" onSubmit={handleSubmit}>
        <Row className="mb-1">
          <Form.Group as={Col} controlId="formGridEmail">
            <Form.Label>Flight Number</Form.Label>
            <Form.Control
              type="text"
              required
              placeholder="Flight number"
              value={airline.flightNumber}
              onChange={(e) =>
                setAirline({ ...airline, flightNumber: e.target.value })
              }
            />
          </Form.Group>

          <Form.Group as={Col} controlId="formGridPassword">
            <Form.Label>Airline</Form.Label>
            <Form.Control
              type="text"
              required
              placeholder="Airline"
              value={airline.airline}
              onChange={(e) =>
                setAirline({ ...airline, airline: e.target.value })
              }
            />
          </Form.Group>
        </Row>
        <Row className="mb-1">
          <Form.Group as={Col} controlId="formGridEmail">
            <Form.Label>From</Form.Label>
            <Form.Control
              type="text"
              required
              placeholder="From"
              value={airline.from}
              onChange={(e) => setAirline({ ...airline, from: e.target.value })}
            />
          </Form.Group>

          <Form.Group as={Col} controlId="formGridPassword">
            <Form.Label>To</Form.Label>
            <Form.Control
              type="text"
              required
              placeholder="To"
              value={airline.to}
              onChange={(e) => setAirline({ ...airline, to: e.target.value })}
            />
          </Form.Group>
        </Row>
        <Row className="mb-1">
          <Form.Group as={Col} controlId="formGridEmail">
            <Form.Label>Start At</Form.Label>
            <Form.Control
              type="text"
              required
              placeholder="Start at time"
              value={airline.startAt}
              onChange={(e) =>
                setAirline({ ...airline, startAt: e.target.value })
              }
            />
          </Form.Group>

          <Form.Group as={Col} controlId="formGridPassword">
            <Form.Label>End At</Form.Label>
            <Form.Control
              type="text"
              required
              placeholder="end at time"
              value={airline.endAt}
              onChange={(e) =>
                setAirline({ ...airline, endAt: e.target.value })
              }
            />
          </Form.Group>
        </Row>
        <Row className="mb-1">
          <Form.Group as={Col} controlId="departDate">
            <Form.Label>Cost</Form.Label>
            <Form.Control
              type="number"
              placeholder="Cost"
              value={airline.cost}
              onChange={(e) =>
                setAirline({ ...airline, cost: parseFloat(e.target.value) })
              }
            />
          </Form.Group>
          <Form.Group as={Col} className="mb-1" id="mealType">
            <Form.Label> </Form.Label>
            <Form.Check>
              <Form.Check.Input
                checked={airline.isVeg}
                onChange={(e) => {
                  setAirline({ ...airline, isVeg: e.target.checked });
                }}
              />
              <Form.Check.Label>Veg</Form.Check.Label>
            </Form.Check>
            <Form.Check>
              <Form.Check.Input
                checked={airline.isNonVeg}
                onChange={(e) => {
                  setAirline({ ...airline, isNonVeg: e.target.checked });
                }}
              />
              <Form.Check.Label>Non Veg</Form.Check.Label>
            </Form.Check>
          </Form.Group>
          <Form.Group as={Col} controlId="businessClassSeats">
            <Form.Label>Business Class Seats</Form.Label>
            <Form.Control
              type="number"
              required
              placeholder="Business Class Seats"
              value={airline.businessClassSeats}
              onChange={(e) =>
                setAirline({
                  ...airline,
                  businessClassSeats: parseFloat(e.target.value),
                })
              }
            />
          </Form.Group>
          <Form.Group as={Col} controlId="nonBusinessClassSeats">
            <Form.Label>Economy Seats</Form.Label>
            <Form.Control
              type="number"
              required
              placeholder="Economy Seats"
              value={airline.nonBusinessClassSeats}
              onChange={(e) =>
                setAirline({
                  ...airline,
                  nonBusinessClassSeats: parseFloat(e.target.value),
                })
              }
            />
          </Form.Group>
          <Form.Group as={Col} controlId="rows">
            <Form.Label>Rows</Form.Label>
            <Form.Control
              type="number"
              required
              placeholder="Rows"
              value={airline.rows}
              onChange={(e) =>
                setAirline({ ...airline, rows: parseFloat(e.target.value) })
              }
            />
          </Form.Group>
        </Row>
        <Row className="mb-1">
          <Form.Group as={Col} className="mb-1" id="tripType">
            <Form.Label>Running days</Form.Label>
            {DAYS.map((d) => (
              <div>
                <input
                  type="checkbox"
                  className="form-check-input"
                  checked={airline.days.some((ad) => ad === d)}
                  onChange={(e) => {
                    if (e.target.checked) {
                      setAirline({ ...airline, days: [...airline.days, d] });
                    } else {
                      setAirline({
                        ...airline,
                        days: [...airline.days.filter((ad) => ad !== d)],
                      });
                    }
                  }}
                />{" "}
                {d}
              </div>
            ))}
          </Form.Group>
        </Row>
        <div className="row">
          <div className="col text-end">
            <Button variant="primary" type="submit">
              Add Flight
            </Button>
          </div>
        </div>
      </Form>
    </div>
  );
}
