import { Measure } from "../enums/measure.enum";

export interface Istate {
  measureValue: number;
  name: Measure;
  entityRefID: string;
  timeStamp: Date;
  id: string;
}
