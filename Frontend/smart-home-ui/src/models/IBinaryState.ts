import { Istate } from "./IState";

export interface IBinaryState extends Istate {
  name: string;
  value: boolean;
}
