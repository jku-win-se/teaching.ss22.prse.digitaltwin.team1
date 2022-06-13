import { Edit } from "@mui/icons-material";
import { IconButton } from "@mui/material";
import Grid from "@mui/material/Grid";
import React from "react";
import { buildStyles, CircularProgressbar } from "react-circular-progressbar";
import "react-circular-progressbar/dist/styles.css";
import { useNavigate } from "react-router-dom";
import { Icon } from "../../../../components/icon/icon.component";
import { Building } from "../../../../enums/building.enum";
import { Measure } from "../../../../enums/measure.enum";
import { RoomType } from "../../../../enums/roomType.enum";
import { IMeasureState } from "../../../../models/IMeasureState";
import { IWSData } from "../../../../models/IWSData";
import { StateService } from "../../../../services/State.service";
import { getCO2Color } from "../../../../utils/getCO2Color";
import { getRoomTypeIcon } from "../../../../utils/getRoomTypeIcon";
import AddEditDialog from "../add-edit-dialog/add-edit-dialog.component";
import DeleteDialog from "../delete-dialog/delete-dialog.component";
import "./room-list-item.styles.css";

export interface IRoomListItemProps {
  roomId: string;
  roomName: string;
  roomIcon: string;
  building: string;
  maxPeople: number;
  triggerReload: () => void;
}

const sService = StateService.getInstance();

export default function RoomListItem(props: IRoomListItemProps) {
  const navigate = useNavigate();
  const [open, setOpen] = React.useState(false);

  const [people, setPeople] = React.useState<IWSData>();
  const [co2, setCo2] = React.useState<IWSData>();

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  React.useEffect(() => {
    async function fetchData(roomID: string) {
      await sService.getInitialMeasureById(roomID);

      const people = sService.returnWSDataForMeasure(
        Measure.PeopleInRoom,
        roomID
      );
      const co2 = sService.returnWSDataForMeasure(Measure.Co2, roomID);

      setPeople(sService.returnWSDataForMeasure(Measure.PeopleInRoom, roomID));
      setCo2(sService.returnWSDataForMeasure(Measure.Co2, roomID));
      removeWSListener(people.entityRef, people.name);
      removeWSListener(co2.entityRef, co2.name);
      getWSData(setPeople, people.entityRef, people.name);
      getWSData(setCo2, co2.entityRef, co2.name);
    }
    if (props.roomId !== undefined) {
      fetchData(props.roomId);
    }
  }, [props.roomId]);

  React.useEffect(() => {
    return () => {
      console.log("unmount");
      removeWSListener(people!.entityRef, people!.name);
      removeWSListener(co2!.entityRef, co2!.name);
    };
  }, []);

  const getWSData = (
    setData: React.Dispatch<React.SetStateAction<IWSData | undefined>>,
    entityRef: string,
    name: string
  ) => {
    console.log("Getting WS Data");
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
    <div className="list">
      <Grid
        container
        spacing={2}
        justifyContent="flex-start"
        alignContent={"center"}
        sx={{ margin: "auto 0" }}
      >
        <Grid
          className="clickable"
          item
          xs={10}
          container
          alignContent={"center"}
          marginLeft={4}
          onClick={() => navigate("details/" + props.roomId)}
        >
          <Grid
            id="roomIcon"
            item
            xs={1}
            container
            alignContent={"center"}
            marginLeft={4}
          >
            <Icon
              fontSize="large"
              name={getRoomTypeIcon(props.roomIcon as keyof typeof RoomType)}
            ></Icon>
          </Grid>

          <Grid item xs={2} sm container alignContent={"center"}>
            <div className="room-font-size">{props.roomName}</div>
            {Building[props.building as keyof typeof Building]}
          </Grid>

          <Grid item xs={4}></Grid>

          <Grid
            id="indicators"
            item
            xs={1}
            container
            justifyContent="center"
            alignItems="center"
          >
            <div
              id="co2"
              className="co2-indicator"
              style={{
                backgroundColor: getCO2Color(
                  isNaN(Number(co2?.value)) ? 0 : Number(co2?.value)
                ),
              }}
            >
              <div id="co2-text" className="co2-value">
                {isNaN(Number(co2?.value))
                  ? "-"
                  : Number(co2?.value).toLocaleString(undefined, {
                    maximumFractionDigits: 1,
                  })}{" "}
                <br /> ppm
              </div>
            </div>
            <div className="indicator-text">co2 value</div>
          </Grid>

          <Grid id="spacerMobile" item xs={2} sm={1}></Grid>

          <Grid
            id="co2Mobile"
            className="co2-indicator-mobile"
            container
            xs
            justifyContent={"right"}
          >
            <Grid
              className="co2-background"
              container
              alignContent={"center"}
              justifyContent={"center"}
              style={{
                backgroundColor: getCO2Color(
                  isNaN(Number(co2?.value)) ? 0 : Number(co2?.value)
                ),
              }}
            >
              {co2?.value} ppm
            </Grid>
          </Grid>

          <Grid id="spacerDesktop" item xs={1} sm={0.5}></Grid>

          <Grid
            id="indicators"
            item
            xs={1}
            container
            justifyContent="center"
            alignItems="center"
          >
            <div className="people-indicator">
              <CircularProgressbar
                strokeWidth={10}
                value={isNaN(Number(people?.value)) ? 0 : Number(people?.value)}
                minValue={0}
                maxValue={props.maxPeople}
                text={
                  (
                    ((isNaN(Number(people?.value))
                      ? 0
                      : Number(people?.value)) /
                      props.maxPeople) *
                    100
                  ).toLocaleString(undefined, { maximumFractionDigits: 0 }) +
                  "%"
                }
                styles={buildStyles({
                  textColor: "black",
                  pathColor: "#66B5D6",
                })}
              />
            </div>
            <div className="indicator-text">
              {isNaN(Number(people?.value))
                ? "-"
                : Number(people?.value).toLocaleString(undefined, {
                  maximumFractionDigits: 0,
                })}
              /{props.maxPeople} People
            </div>
          </Grid>
        </Grid>

        <div id="peopleMobile" className="people-indicator-mobile">
          {people?.value}/{props.maxPeople} People
        </div>

        <Grid
          id="buttonsDesktop"
          item
          xs
          container
          justifyContent="center"
          alignItems="center"
          marginRight={"20px"}
        >
          <IconButton aria-label="edit room" onClick={handleClickOpen}>
            <Edit fontSize="large" />
          </IconButton>

          <DeleteDialog
            roomId={props.roomId}
            triggerReload={props.triggerReload}
            handleClose={handleClose}
          />
        </Grid>

        <Grid
          id="buttonsMobile"
          item
          xs
          container
          justifyContent="right"
          alignItems="right"
          marginRight={"40px"}
        >
          <IconButton aria-label="edit room" onClick={handleClickOpen}>
            <Edit fontSize="large" />
          </IconButton>

          <DeleteDialog
            roomId={props.roomId}
            triggerReload={props.triggerReload}
            handleClose={handleClose}
          />
          <Grid item xs={0.5} sm={1}></Grid>
        </Grid>
      </Grid>
      {open ? (
        <AddEditDialog
          editMode={true}
          handleClose={handleClose}
          open={open}
          id={props.roomId}
          triggerReload={props.triggerReload}
        ></AddEditDialog>
      ) : null}
    </div>
  );
}
