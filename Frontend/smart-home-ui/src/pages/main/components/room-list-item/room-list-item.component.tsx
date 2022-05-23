import * as Muicon from "@mui/icons-material";
import { Edit } from "@mui/icons-material";
import { IconButton, SvgIconProps } from "@mui/material";
import Grid from "@mui/material/Grid";
import React from "react";
import { buildStyles, CircularProgressbar } from "react-circular-progressbar";
import "react-circular-progressbar/dist/styles.css";
import { useNavigate } from "react-router-dom";
import { Measure } from "../../../../enums/measure.enum";
import { RoomTypeIcon } from "../../../../enums/roomTypeIcon.enum";
import { StateService } from "../../../../services/State.service";
import AddEditDialog from "../add-edit-dialog/add-edit-dialog.component";
import DeleteDialog from "../delete-dialog/delete-dialog.component";
import "./room-list-item.styles.css";

export interface IRoomListItemProps {
  roomId: string;
  roomName: string;
  roomIcon: string;
  building: string;
  //coValue: number;
  //currentPeople: number;
  maxPeople: number;
}

const Icon = ({
  name,
  ...rest
}: { name: keyof typeof Muicon } & SvgIconProps) => {
  const IconComponent = Muicon[name];
  return IconComponent ? <IconComponent {...rest} /> : null;
};

function co2Color(value: number) {
  if (value < 800) {
    return "#71CCAB";
  }
  if (value > 1000) {
    return "#FF5252";
  }
  return "#FFEE4D";
}

function roomType(type: string) {
  if (type === "Lab") {
    return RoomTypeIcon.Lab;
  }
  if (type === "LectureRoom") {
    return RoomTypeIcon.LectureRoom;
  }
  return RoomTypeIcon.MeetingRoom;
}
const sService = StateService.getInstance();

export default function RoomListItem(props: IRoomListItemProps) {
  const navigate = useNavigate();
  const [open, setOpen] = React.useState(false);

  const [currPeopleInRoom, setCurrPeopleInRoom] = React.useState("-");
  const [currCO2Value, setCurrCO2Value] = React.useState("-");

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  React.useEffect(() => {
    async function fetchData(roomID: string) {
      await sService.getInitialMeasureById(roomID);

      setCurrCO2Value(
        sService
          .returnValueForMeasureAndRoomID(Measure.Co2, props.roomId)
          .toLocaleString(undefined, { maximumFractionDigits: 1 })
      );
      setCurrPeopleInRoom(
        sService
          .returnValueForMeasureAndRoomID(Measure.PeopleInRoom, props.roomId)
          .toLocaleString(undefined, { maximumFractionDigits: 0 })
      );
    }
    if (props.roomId !== undefined) {
      fetchData(props.roomId);
    }
  }, [props.roomId]);

  const renderCo2 = () => {
    return currCO2Value;
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
            <Icon fontSize="large" name={roomType(props.roomIcon)}></Icon>
          </Grid>

          <Grid item xs={2} sm container alignContent={"center"}>
            <div className="room-font-size">{props.roomName}</div>
            {props.building}
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
                backgroundColor: co2Color(
                  isNaN(Number(currCO2Value)) ? 0 : Number(currCO2Value)
                ),
              }}
            >
              <div id="co2-text" className="co2-value">
                {renderCo2()} <br /> ppm
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
                backgroundColor: co2Color(
                  isNaN(Number(currCO2Value)) ? 0 : Number(currCO2Value)
                ),
              }}
            >
              {renderCo2()} ppm
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
                value={
                  isNaN(Number(currPeopleInRoom)) ? 0 : Number(currPeopleInRoom)
                }
                minValue={0}
                maxValue={props.maxPeople}
                text={
                  (
                    ((isNaN(Number(currPeopleInRoom))
                      ? 0
                      : Number(currPeopleInRoom)) /
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
              {currPeopleInRoom}/{props.maxPeople} People
            </div>
          </Grid>
        </Grid>

        <div id="peopleMobile" className="people-indicator-mobile">
          {currPeopleInRoom}/{props.maxPeople} People
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

          <DeleteDialog />
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

          <DeleteDialog />
          <Grid item xs={0.5} sm={1}></Grid>
        </Grid>
      </Grid>

      <AddEditDialog
        editMode={true}
        handleClose={handleClose}
        open={open}
      ></AddEditDialog>
    </div>
  );
}
