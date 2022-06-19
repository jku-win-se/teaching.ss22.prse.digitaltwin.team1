import { Equipment } from "../enums/equipment.enum";
import { IBinaryState } from "./IBinaryState";

export interface IRoomEquipment {
  equipmentRef: string;
  name: Equipment;
  roomID: string;
  id: string;
  state: IBinaryState[];
}
