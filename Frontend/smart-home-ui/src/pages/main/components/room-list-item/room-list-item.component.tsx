import * as Muicon from "@mui/icons-material";
import { Edit } from "@mui/icons-material";
import { IconButton, SvgIconProps } from "@mui/material";
import Grid from "@mui/material/Grid";
import React from 'react';
import { buildStyles, CircularProgressbar } from "react-circular-progressbar";
import 'react-circular-progressbar/dist/styles.css';
import { useNavigate } from 'react-router-dom';
import DeleteDialog from "../delete-dialog/delete-dialog.component";
import "./room-list-item.styles.css";

export interface IRoomListItemProps {
    roomName: string;
    roomIcon: keyof typeof Muicon;
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

function co2Color(props: number) {
    if (props < 800) {
        return "#71CCAB";
    }
    if (props > 1000) {
        return "#FF5252"
    }
    return "#FFEE4D";
}

export default function RoomListItem(props: IRoomListItemProps) {
    const navigate = useNavigate();

    return (
        <div
            className="list"
            onClick={() => navigate('details/:roomid')}
            style={{ cursor: 'pointer' }}
        >

            <Grid
                container
                spacing={2}
                justifyContent="flex-start"
                alignContent={"center"}
                sx={{ margin: "auto 0" }}
            >
                <Grid
                    item
                    xs={1}
                    container
                    alignContent={"center"}
                    marginLeft={4}
                >
                    <Icon fontSize="large" name={props.roomIcon}></Icon>
                </Grid>

                <Grid
                    item
                    xs
                    container
                    alignContent={"center"}
                >
                    <div className="room-font-size">
                        {props.roomName}
                    </div>

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
                    <div style={{
                        display: "flex",
                        width: "80px",
                        height: "80px",
                        backgroundColor: co2Color(props.coValue),
                        borderRadius: "50%"
                    }}>
                        <text style={{
                            margin: "auto",
                            whiteSpace: "pre-line",
                            textAlign: "center"
                        }}>
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
                    <div style={{
                        display: "flex",
                        width: "80px",
                        height: "80px"
                    }}>
                        <CircularProgressbar
                            strokeWidth={10}
                            value={(props.currentPeople / props.maxPeople) * 100}
                            minValue={0}
                            maxValue={100}
                            text={((props.currentPeople / props.maxPeople) * 100) + '%'}
                            styles={buildStyles({
                                textColor: "black",
                                pathColor: "#66B5D6"
                            })}
                        />
                    </div>
                    <div>{props.currentPeople}/{props.maxPeople} People</div>
                </Grid>

                <Grid
                    item
                    xs
                    container
                    justifyContent="center"
                    alignItems="center"
                >
                    <IconButton aria-label="edit room"
                        onClick={() => {
                            alert('edit clicked');
                        }}
                    >
                        <Edit fontSize="large" />
                    </IconButton>

                    <DeleteDialog />

                </Grid>
            </Grid>
        </div>
    );
}
