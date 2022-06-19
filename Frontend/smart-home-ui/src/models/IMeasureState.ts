import { Measure } from "../enums/measure.enum";
import { Istate } from "./IState";

export interface IMeasureState extends Istate {
  name: Measure;
  value: number;
}
