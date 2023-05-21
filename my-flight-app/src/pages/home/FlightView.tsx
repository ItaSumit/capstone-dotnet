import { FlightResult } from "../../types";

interface Props {
    flight: FlightResult;
    selected: boolean;
    onSelect: (flight: FlightResult) => void;
}
export default function FlightView({ flight, selected, onSelect }: Props) {
    const { airline, flightNumber, cost } = flight;
    return (
        <div className={`row p-3 mb-4 ${selected ? 'bg-secondary': 'bg-light'}`} style={{ cursor: 'pointer'}} onClick={() => onSelect(flight)}>
            <div className="col">{airline}</div>
            <div className="col">{flightNumber}</div>
            <div className="col text-end">Rs. {cost}</div>
        </div>
    )
}