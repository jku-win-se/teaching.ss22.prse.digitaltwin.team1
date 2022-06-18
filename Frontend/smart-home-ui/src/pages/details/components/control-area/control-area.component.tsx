import { Grid } from "@mui/material";
import * as React from "react";
import { Equipment } from "../../../../enums/equipment.enum";
import { IRoom } from "../../../../models/IRoom";
import { RoomService } from "../../../../services/Room.service";
import ControlUnit from "../control-unit/control-unit.component";
import "./control-area.styles.css";

export interface IControlAreaProps {
  room: IRoom | undefined;
}
const rService = RoomService.getInstance();
export default function ControlArea(props: IControlAreaProps) {
  const [isLoading, setIsLoading] = React.useState(true);

  React.useEffect(() => {
    if (props.room) {
      setIsLoading(false);
    }
  }, [props.room]);

  if (!isLoading) {
    return (
      <div>
        <Grid container spacing={2}>
          <Grid
            className={`${
              rService.getEquipmentByName(Equipment.Window).length === 0
                ? "control-area-hide-unit"
                : ""
            } `}
            item
            xs={12}
            sm={12}
            md={3}
          >
            <ControlUnit
              iconName="SensorWindowOutlined"
              header="Windows open"
              sensors={rService.getEquipmentByName(Equipment.Window)}
              activeColor="#AE619D"
            ></ControlUnit>
          </Grid>
          <Grid
            className={`${
              rService.getEquipmentByName(Equipment.Door).length === 0
                ? "control-area-hide-unit"
                : ""
            } `}
            item
            xs={12}
            sm={12}
            md={3}
          >
            <ControlUnit
              header="Doors open"
              iconName="SensorDoorOutlined"
              sensors={rService.getEquipmentByName(Equipment.Door)}
              activeColor="#0084BB"
            ></ControlUnit>
          </Grid>
          <Grid
            className={`${
              rService.getEquipmentByName(Equipment.Ventilator).length === 0
                ? "control-area-hide-unit"
                : ""
            } `}
            item
            xs={12}
            sm={12}
            md={3}
          >
            <ControlUnit
              iconName="AcUnitOutlined"
              header="Fans on"
              sensors={rService.getEquipmentByName(Equipment.Ventilator)}
              activeColor="#BFCE52"
            ></ControlUnit>
          </Grid>
          <Grid
            className={`${
              rService.getEquipmentByName(Equipment.Light).length === 0
                ? "control-area-hide-unit"
                : ""
            } `}
            item
            xs={12}
            sm={12}
            md={3}
          >
            <ControlUnit
              iconName="LightbulbOutlined"
              header="Lights on"
              sensors={rService.getEquipmentByName(Equipment.Light)}
              activeColor="#F1BC3F"
            ></ControlUnit>
          </Grid>
        </Grid>
      </div>
    );
  } else {
    return <h1>Control Area Loading...</h1>;
  }
}
