import { Measure } from "../enums/measure.enum";

export interface Istate {
  value: number;
  name: Measure;
  entityRefID: string;
  timeStamp: Date;
  id: string;
}
