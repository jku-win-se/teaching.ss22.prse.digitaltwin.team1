import Grid from "@mui/material/Grid";
import * as React from "react";
import { Equipment } from "../../../../enums/equipment.enum";
import { Measure } from "../../../../enums/measure.enum";
import { IRoom } from "../../../../models/IRoom";
import { RoomService } from "../../../../services/Room.service";
import { StateService } from "../../../../services/State.service";
import InformationPanelItem from "../information-panel-item/information-panel-item.component";
import "./information-panel.style.css";

export interface IInformationPanelProps {
  room: IRoom | undefined;
}

const sService = StateService.getInstance();
const rService = RoomService.getInstance();

export default function InformationPanel(props: IInformationPanelProps) {
  const [isLoading, setIsLoading] = React.useState(true);

  React.useEffect(() => {
    async function fetchData(roomID: string) {
      await sService.getInitialMeasureById(roomID);
      setIsLoading(false);
    }
    if (props.room !== undefined) {
      fetchData(props.room.id);
    }
  }, [props.room]);

  return (
    <div>
      <h3 className="information-panel-header">
        {props.room?.building} {props.room?.name}
      </h3>
      <Grid container spacing={0}>
        <Grid item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value={
              sService.returnValueForMeasure(Measure.People) +
              "/" +
              props.room?.size
            }
            icon="GroupOutlined"
          ></InformationPanelItem>
        </Grid>
        <Grid item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value={sService
              .returnValueForMeasure(Measure.Temperature)
              .toLocaleString(undefined, { maximumFractionDigits: 0 })}
            unit="°C"
            icon="ThermostatOutlined"
          ></InformationPanelItem>
        </Grid>
        <Grid id="SensorWindowOutlined" item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value={rService.getEquipmentNumber(Equipment.Window)}
            icon="SensorWindowOutlined"
          ></InformationPanelItem>
        </Grid>
        <Grid id="SensorWindowOutlined" item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value={rService.getEquipmentNumber(Equipment.Ventilator)}
            icon="AcUnitOutlined"
          ></InformationPanelItem>
        </Grid>
        <Grid item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value="62"
            unit="&#13217;"
            icon="StraightenOutlined"
          ></InformationPanelItem>
        </Grid>
        <Grid item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value={sService
              .returnValueForMeasure(Measure.Co2)
              .toLocaleString(undefined, { maximumFractionDigits: 1 })}
            unit="ppm"
            icon="Co2Outlined"
          ></InformationPanelItem>
        </Grid>
        <Grid id="SensorDoorOutlined" item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value={rService.getEquipmentNumber(Equipment.Window)}
            icon="SensorDoorOutlined"
          ></InformationPanelItem>
        </Grid>
        <Grid id="LightbulbOutlined" item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value={rService.getEquipmentNumber(Equipment.Window)}
            icon="LightbulbOutlined"
          ></InformationPanelItem>
        </Grid>
        <Grid item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value={props.room?.name}
            icon="InfoOutlined"
          ></InformationPanelItem>
        </Grid>
      </Grid>
    </div>
  );
}
