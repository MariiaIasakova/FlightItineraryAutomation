export default class Flight {
    flightId: number;
    departure: string;
    arrival: string;
    day: number;
    public get id() {
        return this.flightId;
    }

    constructor(flight: any) {
        this.flightId = flight?.flightId;
        this.arrival = flight?.arrival;
        this.departure = flight?.departure;
        this.day = flight?.day;
    }
}