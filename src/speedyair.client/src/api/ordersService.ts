import Order from "../models/Order";
import HttpService from "./httpService";

export default class OrdersService extends HttpService {
    constructor(baseURL: string) {
        super(baseURL);
    }

    public async getOrdersAsync(): Promise<Order[]> {
        const response = await this.get("/api/flightItinerary");
        if (response.status == 200)
            return response.data.map((x: any) => new Order(x));

        return [];
    }

    public async getOrdersByFlightAsync(flightId: string): Promise<Order[]> {
        const response = await this.get(`/api/flightItinerary/${flightId}`);
        if (response.status == 200)
            return response.data.map((x: any) => new Order(x));

        return [];
    }
}
