import * as Muicon from "@mui/icons-material";
import { Edit } from "@mui/icons-material";
import { IconButton, SvgIconProps } from "@mui/material";
import Grid from "@mui/material/Grid";
import { height, maxWidth, minWidth } from "@mui/system";
import React from "react";
import { buildStyles, CircularProgressbar } from "react-circular-progressbar";
import "react-circular-progressbar/dist/styles.css";
import { useNavigate } from "react-router-dom";
import { RoomTypeIcon } from "../../../../enums/roomTypeIcon.enum";
import AddEditDialog from "../add-edit-dialog/add-edit-dialog.component";
import DeleteDialog from "../delete-dialog/delete-dialog.component";
import "./room-list-item.styles.css";

export interface IRoomListItemProps {
  roomId: string;
  roomName: string;
  roomIcon: string;
  building: string;
  coValue: number;
  currentPeople: number;
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

export default function RoomListItem(props: IRoomListItemProps) {
  const navigate = useNavigate();
  const [open, setOpen] = React.useState(false);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  //TODO: styles into css
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
          item
          xs={10}
          container
          alignContent={"center"}
          marginLeft={4}
          onClick={() => navigate("details/" + props.roomId)}
          style={{ cursor: "pointer" }}
        >
          <Grid item xs={1} container alignContent={"center"} marginLeft={4}>
            <Icon fontSize="large" name={roomType(props.roomIcon)}></Icon>
          </Grid>

          <Grid item xs container alignContent={"center"}>
            <div className="room-font-size">{props.roomName}</div>

            {props.building}
          </Grid>

          <Grid item xs={4}></Grid>

          <Grid
            item
            xs={1}
            container
            justifyContent="center"
            alignItems="center"
          >
            <div
              style={{
                display: "flex",
                width: 80,
                height: 80,
                backgroundColor: co2Color(props.coValue),
                borderRadius: "50%",
              }}
            >
              <text
                style={{
                  margin: "auto",
                  whiteSpace: "pre-line",
                  textAlign: "center",
                  fontWeight: "bold",
                }}
              >
                {props.coValue} <br /> ppm
              </text>
            </div>
            co2 value
          </Grid>

          <Grid item xs={0.5}></Grid>

          <Grid
            item
            xs={1}
            container
            justifyContent="center"
            alignItems="center"
          >
            <div className="additional-info">
              <CircularProgressbar
                strokeWidth={10}
                value={(props.currentPeople / props.maxPeople) * 100}
                minValue={0}
                maxValue={100}
                text={(props.currentPeople / props.maxPeople) * 100 + "%"}
                styles={buildStyles({
                  textColor: "black",
                  pathColor: "#66B5D6",
                })}
              />
            </div>
            <div>
              {props.currentPeople}/{props.maxPeople} People
            </div>
          </Grid>
        </Grid>

        <Grid item xs container justifyContent="center" alignItems="center">
          <IconButton aria-label="edit room" onClick={handleClickOpen}>
            <Edit fontSize="large" />
          </IconButton>

          <DeleteDialog />
        </Grid>
      </Grid>
      <AddEditDialog handleClose={handleClose} open={open}></AddEditDialog>
    </div>
  );
}
