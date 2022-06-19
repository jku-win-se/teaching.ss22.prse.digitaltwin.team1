import { IRoomEquipment } from "./IRoomEquipment";

export interface IRoom {
  size: number;
  name: string;
  roomType: string;
  building: string;
  id: string;
  peopleCount: number
  roomEquipment: IRoomEquipment[];
}
