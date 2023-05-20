import Button from "react-bootstrap/Button";
import Col from "react-bootstrap/Col";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";

export default function Home() {
  return (
    <div className="p-5 mb-4 bg-light rounded-3">
      <div className="container-fluid py-5">
        <h1 className="display-5 fw-bold">Plan your travel!</h1>
        <Form autoComplete="false">
          <Row className="mb-3">
            <Form.Group as={Col} controlId="formGridEmail">
              <Form.Label>Depart from</Form.Label>
              <Form.Control type="text" placeholder="Departing airport" />
            </Form.Group>

            <Form.Group as={Col} controlId="formGridPassword">
              <Form.Label>Going to</Form.Label>
              <Form.Control type="text" placeholder="Arrival airport" />
            </Form.Group>
          </Row>
          <Row className="mb-3">
            <Form.Group as={Col} className="mb-3" id="tripType">
              <Form.Label> </Form.Label>
              <Form.Check as={Col} type="radio" label="One Way" />
            </Form.Group>
            <Form.Group as={Col} className="mb-3" id="tripType">
              <Form.Label> </Form.Label>
              <Form.Check as={Col} type="radio" label="Round Trip" />
            </Form.Group>
          </Row>
          <Row className="mb-3">
            <Form.Group as={Col} controlId="departDate">
              <Form.Label>Departure Date</Form.Label>
              <Form.Control type="date" placeholder="Departure date" />
            </Form.Group>

            <Form.Group as={Col} controlId="return Date">
              <Form.Label>Return Date</Form.Label>
              <Form.Control type="date" placeholder="Return date" />
            </Form.Group>
          </Row>
          <Row className="mb-3">
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
            Submit
          </Button>
        </Form>
      </div>
    </div>
  );
}
