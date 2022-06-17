import { Building } from "../enums/building.enum";
import { Equipment } from "../enums/equipment.enum";
import { IError } from "../models/IError";
import { IRoom } from "../models/IRoom";
import { IRoomEquipment } from "../models/IRoomEquipment";
import { asyncForEach } from "../utils/asyncForEach";
import { StateService } from "./State.service";

export class RoomService {
  private static instance: RoomService;
  private constructor() {}
  public static getInstance(): RoomService {
    if (!RoomService.instance) {
      RoomService.instance = new RoomService();
    }

    return RoomService.instance;
  }
  private BASE_URL: string =
    "https://basedataservice.azurewebsites.net/api/Room";

  selectedRoom: IRoom | undefined = undefined;

  allRooms: IRoom[] = [];

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
    this.allRooms = (await response.json()) as IRoom[];
    console.log(this.allRooms);
    return this.allRooms;
  }

  async addStatesForRoomEquipment() {
    const sService = StateService.getInstance();
    await asyncForEach<IRoomEquipment>(
      this.selectedRoom!.roomEquipment,
      async (rq) => {
        const states = await sService.getStateNamesForEquipment(rq.id);
        await asyncForEach<string>(states, async (state) => {
          const stateValue = await sService.getValueForStateNameAndEquipment(
            rq.id,
            state
          );
          rq.state.push(stateValue);
        });
      }
    );
    console.log(this.selectedRoom);
  }
  async getById(id: string, persistant: boolean = true): Promise<IRoom> {
    const response = await fetch(this.BASE_URL + "/" + id, {
      headers: new Headers(this.addHeaders()),
      method: "GET",
    });
    if (persistant) {
      this.selectedRoom = (await response.json()) as IRoom;
      this.selectedRoom.roomEquipment.forEach((rq) => (rq.state = []));
      return this.selectedRoom;
    } else {
      return (await response.json()) as IRoom;
    }
  }

  getEquipmentNumber(type: Equipment) {
    return this.selectedRoom?.roomEquipment.filter((re) => re.name === type)
      .length;
  }
  getEquipmentNumberByTypeAndID(roomID: string, type: Equipment) {
    return this.allRooms
      .filter((room) => room.id === roomID)[0]
      .roomEquipment.filter((re) => re.name === type).length;
  }

  filterByBuilding(building: keyof typeof Building) {
    return building === "AR"
      ? this.allRooms
      : this.allRooms.filter((val) => val.building === building);
  }

  getEquipmentByName(equipment: Equipment) {
    return this.selectedRoom?.roomEquipment.filter(
      (rq) => rq.name === equipment
    )!;
  }

  async delete(roomID: string) {
    const response = await fetch(this.BASE_URL + "/" + roomID, {
      headers: new Headers(this.addHeaders()),
      method: "DELETE",
    });
    const err: IError = {
      error: response.ok,
      text: response.statusText,
      status: response.status,
    };
    return err;
  }

  async addOrChange(
    id: string | undefined,
    peopleCount: number,
    name: string,
    size: number,
    roomType: string,
    building: string,
    roomEquipmentDict: { [key: string]: number },
    change: boolean = false
  ) {
    const response = await fetch(this.BASE_URL + "/Models", {
      headers: new Headers(this.addHeaders()),
      method: change ? "PUT" : "POST",
      body: JSON.stringify({
        id,
        peopleCount,
        name,
        size,
        roomType,
        building,
        roomEquipmentDict,
      }),
    });
    const err: IError = {
      error: !response.ok,
      text: response.statusText,
      status: response.status,
    };
    return err;
  }
}
