import * as Muicon from "@mui/icons-material";
import { Edit } from "@mui/icons-material";
import { IconButton, SvgIconProps } from "@mui/material";
import Grid from "@mui/material/Grid";
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

                    <Grid
                        item
                        xs={2}
                        sm
                        container
                        alignContent={"center"}
                    >
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
                            style={{ backgroundColor: co2Color(props.coValue) }}
                        >
                            <div id="co2-text" className="co2-value">
                                {props.coValue} <br /> ppm
                            </div>
                        </div>
                        <div className="indicator-text">
                            co2 value
                        </div>
                    </Grid>

                    <Grid
                        id="spacerMobile"
                        item
                        xs={2}
                        sm={1}
                    >
                    </Grid>

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
                            style={{ backgroundColor: co2Color(props.coValue) }}
                        >
                            {props.coValue} ppm
                        </Grid>
                    </Grid>

                    <Grid
                        id="spacerDesktop"
                        item
                        xs={1}
                        sm={0.5}
                    >
                    </Grid>

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
                                value={(props.currentPeople / props.maxPeople) * 100}
                                minValue={0}
                                maxValue={100}
                                text={Math.round(((props.currentPeople / props.maxPeople) * 100) * 100) / 100 + "%"}
                                styles={buildStyles({
                                    textColor: "black",
                                    pathColor: "#66B5D6",
                                })} />
                        </div>
                        <div className="indicator-text">
                            {props.currentPeople}/{props.maxPeople} People
                        </div>
                    </Grid>
                </Grid>

                <div
                    id="peopleMobile"
                    className="people-indicator-mobile"
                >
                    {props.currentPeople}/{props.maxPeople} People
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
                    <Grid
                        item
                        xs={0.5}
                        sm={1}
                    >
                    </Grid>
                </Grid>
            </Grid>

            <AddEditDialog handleClose={handleClose} open={open}></AddEditDialog>
        </div >
    );
}

