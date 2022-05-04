import { Equipment } from "../enums/equipment.enum";
import { IRoom } from "../models/IRoom";

export class RoomService {
  private static instance: RoomService;
  private constructor() {}
  public static getInstance(): RoomService {
    if (!RoomService.instance) {
      RoomService.instance = new RoomService();
    }

    return RoomService.instance;
  }
  BASE_URL: string = "https://basedataservice.azurewebsites.net/api/Room";

  selectedRoom: IRoom | undefined = undefined;

  private addHeaders() {
    const requestHeaders: HeadersInit = new Headers();
    requestHeaders.append("Content-Type", "application/json");
    requestHeaders.append("Accept", "/");
    requestHeaders.append("ApiKey", process.env.REACT_APP_API_KEY as string);
    requestHeaders.forEach((val) => console.log(val));
    return requestHeaders;
  }
  async getAll(): Promise<IRoom[]> {
    const response = await fetch(this.BASE_URL, {
      headers: new Headers(this.addHeaders()),
      method: "GET",
    });

    return (await response.json()) as IRoom[];
  }

  async getById(id: String): Promise<IRoom> {
    const response = await fetch(this.BASE_URL + "/" + id, {
      headers: new Headers(this.addHeaders()),
      method: "GET",
    });
    this.selectedRoom = (await response.json()) as IRoom;
    return this.selectedRoom;
  }

  getEquipmentNumber(type: Equipment) {
    return this.selectedRoom?.roomEquipment.filter((re) => re.name !== type)
      .length;
  }
}