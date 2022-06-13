import { Chip, Grid } from "@mui/material";
import * as React from "react";
import { buildStyles, CircularProgressbar } from "react-circular-progressbar";
import * as Muicon from "@mui/icons-material";
import { SvgIconProps } from "@mui/material/SvgIcon";
import "react-circular-progressbar/dist/styles.css";
import "./control-unit.styles.css";
import { IRoomEquipment } from "../../../../models/IRoomEquipment";
import { IBinaryState } from "../../../../models/IBinaryState";
import { StateService } from "../../../../services/State.service";

export interface IControlUnitProps {
  sensors: IRoomEquipment[];
  header: string;
  activeColor: string;
  iconName: keyof typeof Muicon;
}

const sService = StateService.getInstance();

const Icon = ({
  name,
  ...rest
}: { name: keyof typeof Muicon } & SvgIconProps) => {
  const IconComponent = Muicon[name];
  return IconComponent ? <IconComponent {...rest} /> : null;
};

export default function ControlUnit(props: IControlUnitProps) {
  const [sensors, setSensors] = React.useState<IRoomEquipment[]>([]);

  React.useEffect(() => {
    if (props.sensors !== []) {
      setSensors(props.sensors);
      props.sensors.forEach((sensor) => {
        if (sensor.state.length !== 0) {
          removeWSListener(sensor.id, sensor.state[0].name);
          getWSData(setSensors, sensor.id, sensor.state[0].name);
        }
      });
    }
  }, [props.sensors]);

  const removeWSListener = (entityRef: string, name: string) => {
    sService.hubConnection.off("Sensor/" + entityRef + "/" + name);
  };

  const getWSData = (
    setData: React.Dispatch<React.SetStateAction<IRoomEquipment[]>>,
    entityRef: string,
    name: string
  ) => {
    sService.hubConnection.on(
      "Sensor/" + entityRef + "/" + name,
      (data: IBinaryState) => {
        console.log(data);
        setData((prev) => {
          return prev.map((s) =>
            s.id === data.entityRefID ? { ...s, state: [data] } : s
          );
        });
      }
    );
  };

  const generateChips = () => {
    return sensors.map((i, index) => {
      if (i.state.length !== 0) {
        return (
          <Grid key={index} item xs={12}>
            <Chip
              onClick={async () => {
                await sService.changeSensorBinaryState(
                  i.id,
                  !i.state[0].value,
                  i.state[0].name,
                  i.state[0].id
                );
              }}
              style={{
                backgroundColor: i.state[0].value
                  ? props.activeColor
                  : "#BCBCBC",
              }}
              className="chip"
              label={i.name + " " + index}
            ></Chip>
          </Grid>
        );
      } else {
        return null;
      }
    });
  };

  const renderText = () => {
    return sensors.length === 0
      ? `0%`
      : `${
          (sensors.filter((i) => i.state[0]).filter((s) => s.state[0].value)
            .length /
            sensors.length) *
          100
        }%`;
  };

  const generateIcons = () => {
    return sensors.map((i, index) => {
      if (i.state.length !== 0) {
        return (
          <Icon
            key={index}
            onClick={() => {
              sService.changeSensorBinaryState(
                i.id,
                !i.state[0].value,
                i.state[0].name,
                i.state[0].id
              );
            }}
            style={{
              color: i.state[0].value ? props.activeColor : "#BCBCBC",
              marginLeft: "1.5rem",
              fontSize: "clamp(2.2rem, 3vw, 2.7rem)",
            }}
            name={props.iconName}
          ></Icon>
        );
      } else {
        return null;
      }
    });
  };
  return (
    <div>
      <div className="control-unit-big-screen">
        <div className="control-unit-header">
          <h5>{props.header}</h5>
        </div>

        <div
          style={{
            maxWidth: "125px",
            maxHeight: "125px",
            minWidth: "100px",
            minHeight: "100px",
            margin: "auto",
          }}
        >
          <CircularProgressbar
            strokeWidth={10}
            value={
              sensors.filter((i) => i.state[0]).filter((s) => s.state[0].value)
                .length
            }
            minValue={0}
            maxValue={sensors.filter((i) => i.state[0]).length}
            styles={buildStyles({
              textColor: "black",
              pathColor: props.activeColor,
              textSize: "1rem",
            })}
            text={renderText()}
          />
        </div>
        <div className="control-unit-container">
          <Grid container spacing={1}>
            {generateChips()}
          </Grid>
        </div>
      </div>
      <Grid
        container
        spacing={2}
        className="control-unit-mobile-container control-unit-small-screen"
      >
        <Grid item xs={3} className="control-unit-header">
          <h5>{props.header}</h5>
        </Grid>
        <Grid item xs={9} className="control-unit-mobile-icon">
          {generateIcons()}
        </Grid>
      </Grid>
    </div>
  );
}
