import { Measure } from "../enums/measure.enum";
import { Istate } from "../models/IState";

export class StateService {
  private static instance: StateService;
  BASE_URL: string = "https://transdataservice.azurewebsites.net/api/";
  private constructor() {}
  public static getInstance(): StateService {
    if (!StateService.instance) {
      StateService.instance = new StateService();
    }

    return StateService.instance;
  }

  private addHeaders() {
    const requestHeaders: HeadersInit = new Headers();
    requestHeaders.append("Content-Type", "application/json");
    requestHeaders.append("Accept", "/");
    requestHeaders.append("ApiKey", process.env.REACT_APP_API_KEY as string);
    return requestHeaders;
  }

  states: Istate[] = [];

  async getMeasureTypesById(roomID: string) {
    const response = await fetch(
      this.BASE_URL + "/ReadMeasure/GetTypesBy/" + roomID,
      {
        headers: new Headers(this.addHeaders()),
        method: "GET",
      }
    );

    return (await response.json()) as string[];
  }
  async getInitialMeasureById(roomID: string) {
    const stateNames = await this.getMeasureTypesById(roomID);
    await Promise.all(
      stateNames.map(async (stateName) => {
        const response = await fetch(
          this.BASE_URL +
            "/ReadMeasure/GetRecentBy/" +
            roomID +
            "&" +
            stateName,
          {
            headers: new Headers(this.addHeaders()),
            method: "GET",
          }
        );
        this.states.push(await response.json());
      })
    );
    console.log(this.states);
  }

  returnValueForMeasure(measure: Measure) {
    console.log(
      this.states.filter((s) => s.name === measure)[0]
        ? this.states.filter((s) => s.name === measure)[0].value
        : "-"
    );
    return this.states.filter((s) => s.name === measure)[0]
      ? this.states.filter((s) => s.name === measure)[0].value
      : "-";
  }
}