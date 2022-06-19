import Grid from "@mui/material/Grid";
import * as React from "react";
import { Building } from "../../../../enums/building.enum";
import { Equipment } from "../../../../enums/equipment.enum";
import { Measure } from "../../../../enums/measure.enum";
import { IMeasureState } from "../../../../models/IMeasureState";
import { IRoom } from "../../../../models/IRoom";
import { IWSData } from "../../../../models/IWSData";
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
  const [temp, setTemp] = React.useState<IWSData>();
  const [people, setPeople] = React.useState<IWSData>();
  const [co2, setCo2] = React.useState<IWSData>();
  React.useEffect(() => {
    async function fetchData(roomID: string) {
      try {
        await sService.getInitialMeasureById(roomID);
        const localTemp = sService.returnWSDataForMeasure(
          Measure.Temperature,
          roomID
        );
        const localPeople = sService.returnWSDataForMeasure(
          Measure.PeopleInRoom,
          roomID
        );
        const localCo2 = sService.returnWSDataForMeasure(Measure.Co2, roomID);
        setTemp(localTemp);
        setPeople(localPeople);
        setCo2(localCo2);
        removeWSListener(localTemp.entityRef, localTemp.name);
        removeWSListener(localPeople.entityRef, localPeople.name);
        removeWSListener(localCo2.entityRef, localCo2.name);
        getWSData(setTemp, localTemp.entityRef, localTemp.name);
        getWSData(setPeople, localPeople.entityRef, localPeople.name);
        getWSData(setCo2, localCo2.entityRef, localCo2.name);
      } catch (err) {
        console.log(err);
      }

      setIsLoading(false);
    }
    if (props.room !== undefined) {
      fetchData(props.room.id);
    }
  }, [props.room]);

  React.useEffect(() => {
    return () => {
      removeWSListener(temp!.entityRef, temp!.name);
      removeWSListener(people!.entityRef, people!.name);
      removeWSListener(co2!.entityRef, co2!.name);
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const getWSData = (
    setData: React.Dispatch<React.SetStateAction<IWSData | undefined>>,
    entityRef: string,
    name: string
  ) => {
    sService.hubConnection.on(
      "Sensor/" + entityRef + "/" + name,
      (data: IMeasureState) => {
        setData({
          name: name,
          value: data.value.toString(),
          entityRef: entityRef,
        });
      }
    );
  };

  const removeWSListener = (entityRef: string, name: string) => {
    sService.hubConnection.off("Sensor/" + entityRef + "/" + name);
  };

  return (
    <div>
      <h3 className="information-panel-header">{props.room?.name}</h3>
      <Grid container spacing={0}>
        <Grid item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value={
              (isNaN(Number(people?.value))
                ? "-"
                : Number(people?.value).toLocaleString(undefined, {
                    maximumFractionDigits: 0,
                  })) +
              "/" +
              props.room?.peopleCount
            }
            icon="GroupOutlined"
          ></InformationPanelItem>
        </Grid>
        <Grid item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value={
              isNaN(Number(temp?.value))
                ? "-"
                : Number(temp?.value).toLocaleString(undefined, {
                    maximumFractionDigits: 1,
                  })
            }
            unit="°C"
            icon="ThermostatOutlined"
          ></InformationPanelItem>
        </Grid>
        <Grid id="SensorWindowOutlined" item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value={rService.getEquipmentNumber(Equipment.Window)}
            icon="SensorWindowOutlined"
            color="#AE619D"
          ></InformationPanelItem>
        </Grid>
        <Grid id="SensorWindowOutlined" item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value={rService.getEquipmentNumber(Equipment.Ventilator)}
            icon="AcUnitOutlined"
            color="#BFCE52"
          ></InformationPanelItem>
        </Grid>
        <Grid item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value={props.room?.size}
            unit="&#13217;"
            icon="StraightenOutlined"
          ></InformationPanelItem>
        </Grid>
        <Grid item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value={
              isNaN(Number(co2?.value))
                ? "-"
                : Number(co2?.value).toLocaleString(undefined, {
                    maximumFractionDigits: 1,
                  })
            }
            unit="ppm"
            icon="Co2Outlined"
            numericValue={Number(co2?.value)}
            dynamic={true}
          ></InformationPanelItem>
        </Grid>
        <Grid id="SensorDoorOutlined" item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value={rService.getEquipmentNumber(Equipment.Door)}
            icon="SensorDoorOutlined"
            color="#0084BB"
          ></InformationPanelItem>
        </Grid>
        <Grid id="LightbulbOutlined" item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value={rService.getEquipmentNumber(Equipment.Light)}
            icon="LightbulbOutlined"
            color="#F1BC3F"
          ></InformationPanelItem>
        </Grid>
        <Grid item xs={4} sm={4} md={3}>
          <InformationPanelItem
            isLoading={isLoading}
            value={Building[props.room?.building as keyof typeof Building]}
            icon="InfoOutlined"
          ></InformationPanelItem>
        </Grid>
      </Grid>
    </div>
  );
}
