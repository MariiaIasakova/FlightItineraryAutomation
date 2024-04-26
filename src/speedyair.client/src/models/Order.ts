export default class Order {
    flightId?: number;
    orderId: string;
    departure: string;
    arrival: string;
    day?: number;
    public get id() {
        return this.orderId;
    }

    constructor(order: any) {
        this.flightId = order?.flightId;
        this.orderId = order?.orderId;
        this.arrival = order?.arrival;
        this.departure = order?.departure;
        this.day = order?.day;
    }
}