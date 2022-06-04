import { Measure } from "../enums/measure.enum";
import { IChartData } from "../models/IChartData";
import { IMeasureState } from "../models/IMeasureState";
import * as signalR from "@microsoft/signalr";
import { IWSData } from "../models/IWSData";
import { IBinaryState } from "../models/IBinaryState";

export class StateService {
  private static instance: StateService;
  BASE_URL: string = "https://transdataservice.azurewebsites.net/api/";
  hubConnection!: signalR.HubConnection;
  states: IMeasureState[] = [];
  private constructor() {}
  public static getInstance(): StateService {
    if (!StateService.instance) {
      StateService.instance = new StateService();
    }

    return StateService.instance;
  }

  establishWsConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("https://transdataservice.azurewebsites.net/hub", {
        headers: { ApiKey: process.env.REACT_APP_API_KEY! },
      })
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log("Connection started"))
      .catch((err) =>
        console.log("Error while establishing connection: ", err)
      );
  }

  closeWsConnection() {
    this.hubConnection.stop();
  }

  private addHeaders() {
    const requestHeaders: HeadersInit = new Headers();
    requestHeaders.append("Content-Type", "application/json");
    requestHeaders.append("Accept", "/");
    requestHeaders.append("ApiKey", process.env.REACT_APP_API_KEY as string);
    return requestHeaders;
  }

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
    this.states = [];
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
  }
  async getMeasureChartData(roomID: string, name: string) {
    const response = await fetch(
      this.BASE_URL + "/ReadMeasure/GetChartData/" + roomID + "&" + name,
      {
        headers: new Headers(this.addHeaders()),
        method: "GET",
      }
    );

    return (await response.json()) as IChartData[];
  }

  async getBinaryChartData(rqIds: string[], stateName: string) {
    const response = await fetch(
      this.BASE_URL + "ReadBinary/GetChartData/" + stateName,
      {
        headers: new Headers(this.addHeaders()),
        method: "Post",
        body: JSON.stringify(rqIds),
      }
    );

    return (await response.json()) as IChartData[];
  }

  async getValueForStateNameAndEquipment(
    roomEquipmentID: string,
    name: string
  ) {
    const response = await fetch(
      this.BASE_URL + "ReadBinary/GetRecentBy/" + roomEquipmentID + "&" + name,
      {
        headers: new Headers(this.addHeaders()),
        method: "GET",
      }
    );
    return (await response.json()) as IBinaryState;
  }

  async getStateNamesForEquipment(roomEquipmentID: string) {
    const response = await fetch(
      this.BASE_URL + "ReadBinary/GetTypesBy/" + roomEquipmentID,
      {
        headers: new Headers(this.addHeaders()),
        method: "GET",
      }
    );
    return (await response.json()) as string[];
  }

  returnWSDataForMeasure(
    measure: Measure,
    roomID: string,
    fractionalDigits: number
  ) {
    console.log(this.states);
    const measureState = this.states.filter(
      (s) => s.name === measure && s.entityRefID === roomID
    )[0];
    if (measureState) {
      return {
        name: measureState.name,
        value: measureState.value.toLocaleString(undefined, {
          maximumFractionDigits: fractionalDigits,
        }),
        entityRef: measureState.entityRefID,
      } as IWSData;
    } else {
      return { name: "", value: "-", entityRef: "" } as IWSData;
    }
  }
}
