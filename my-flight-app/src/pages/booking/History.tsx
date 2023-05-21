import { useEffect, useState } from "react";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import { BookingDetail } from "../../types";
import { bookingHistory, bookingPnr } from "../../service/userservice";
import Table from "react-bootstrap/Table";
import { useParams } from "react-router-dom";

export default function History() {
  const { id } = useParams<{ id: string }>();
  const [email, setEmail] = useState("");
  const [pnr, setPnr] = useState("");
  const [bookings, setBookings] = useState<BookingDetail[]>([]);
  function handleHistory() {
    if (pnr.length) {
      bookingPnr(pnr).then((bookingHistory) => {
        setBookings(bookingHistory);
        if (bookingHistory.length === 0) {
          alert("No bookings found");
        }
      });
    } else if (email.length) {
      bookingHistory(email).then((bookingHistory) => {
        setBookings(bookingHistory);
        if (bookingHistory.length === 0) {
          alert("No bookings found");
        }
      });
    } else {
      alert("Fill PNR or Email");
    }
  }

  useEffect(() => {
    if (id) {
        setPnr(id);
        bookingPnr(id).then((bookingHistory) => {
            setBookings(bookingHistory);
            if (bookingHistory.length === 0) {
              alert("No bookings found");
            }
          });
    }
  }, [id]);

  return (
    <div className="p-3 mb-4 bg-light">
      <h3>Get your booking history</h3>
      <div>
        <Form.Label htmlFor="email">Email</Form.Label>
        <Form.Control
          type="email"
          id="email"
          placeholder="email address"
          aria-describedby="helpBlock"
          value={email}
          onChange={(e) => {
            setEmail(e.target.value);
            setPnr("");
          }}
        />
        <Form.Text id="helpBlock" muted>
          Provide your email adress to fetch list of booking.
        </Form.Text>
      </div>
      <div>Or</div>
      <div>
        <Form.Label htmlFor="pnr">PNR</Form.Label>
        <Form.Control
          type="text"
          id="pnr"
          placeholder="pnr"
          aria-describedby="helpBlockPnr"
          value={pnr}
          onChange={(e) => {
            setPnr(e.target.value);
            setEmail("");
          }}
        />
        <Form.Text id="helpBlockPnr" muted>
          Provide your PNR to fetch booking detail
        </Form.Text>
      </div>
      <div className="mb-4">
        <Button variant="primary" onClick={handleHistory}>
          Get bookings
        </Button>
      </div>
      {bookings.length > 0 && (
        <>
          <h2>Bookings</h2>
          <div>
            <Table striped bordered hover>
              <thead>
                <tr>
                  <th>#</th>
                  <th>PNR</th>
                  <th>Status</th>
                  <th>Flight No</th>
                  <th>Departure Date</th>
                  <th>Departure Time</th>
                  <th>Arrival Date</th>
                  <th>Arrival Time</th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                {bookings.map((b) => {
                  const {
                    bookingId,
                    pnr,
                    status,
                    flightNumer,
                    departureDate,
                    departureTime,
                    arraivalDate,
                    arrivalTime,
                  } = b;
                  return (
                    <>
                      <tr>
                        <td rowSpan={b.passengers.length + 3}>{bookingId}</td>
                        <td>{pnr}</td>
                        <td>{status}</td>
                        <td>{flightNumer}</td>
                        <td>{departureDate}</td>
                        <td>{departureTime}</td>
                        <td>{arraivalDate}</td>
                        <td>{arrivalTime}</td>
                        <td>
                          <button className="btn btn-danger btn-sm">
                            Cancel
                          </button>
                        </td>
                      </tr>
                      <tr>
                        <td colSpan={8}>Passengers</td>
                      </tr>
                      <tr style={{ fontWeight: "bold" }}>
                        <td>Firstname</td>
                        <td>Lastname</td>
                        <td>Age</td>
                        <td>Meal Type</td>
                        <td colSpan={4}>Seat #</td>
                      </tr>
                      {b.passengers.map((p) => (
                        <tr>
                          <td>{p.firstName}</td>
                          <td>{p.lastName}</td>
                          <td>{p.age}</td>
                          <td>{p.mealType}</td>
                          <td>{p.seatNumber}</td>
                        </tr>
                      ))}
                    </>
                  );
                })}
              </tbody>
            </Table>
          </div>
        </>
      )}
    </div>
  );
}
