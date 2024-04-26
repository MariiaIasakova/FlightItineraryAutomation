import axios, { AxiosResponse } from "axios";
export default class HttpService {
    private baseURL: string;

    constructor(baseURL: string) {
        this.baseURL = baseURL;
    }

    protected async get(
        endpoint?: string,
        params?: { [key: string]: any },
        headers?: { [key: string]: any }
    ) : Promise<AxiosResponse> {
        const url = endpoint ? this.baseURL.concat(endpoint) : this.baseURL;
        const options = { params, headers };
        return axios.get(url, options);
    }
}