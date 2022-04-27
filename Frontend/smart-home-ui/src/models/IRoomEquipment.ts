import { Equipment } from "../enums/equipment.enum";

export interface IRoomEquipment {
  equipmentRef: string;
  name: Equipment;
  roomID: string;
  id: string;
}
