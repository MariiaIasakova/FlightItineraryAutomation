import Flight from "../models/Flight";
import HttpService from "./httpService";

export default class ScheduleService extends HttpService {
    constructor(baseURL: string) {
        super(baseURL);
    }

    public async getScheduleAsync(): Promise<Flight[]> {
        const response = await this.get("/api/schedule");
        if (response.status == 200)
            return response.data.map((x: any) => new Flight(x));

        return [];
    }
}
