import { Box, Grid } from "@mui/material";
import * as React from "react";
import { IRoom } from "../../models/IRoom";
import { RoomService } from "../../services/Room.service";
import InformationPanel from "./components/information-panel/information-panel.component";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import "./details.style.css";
import { useNavigate, useParams } from "react-router-dom";
import TempAndCo2Chart from "./components/temp-and-co2-chart/temp-and-co2-chart.component";
import PeopleChart from "./components/people-chart/people-chart.component";
import SensorChart from "./components/sensor-chart/sensor-chart.component";
import ControlArea from "./components/control-area/control-area.component";

export interface IDetailsProps {}

export function Details(props: IDetailsProps) {
  const [room, setRoom] = React.useState<IRoom>();
  const navigate = useNavigate();
  const { roomid } = useParams();
  React.useEffect(() => {
    const rService = RoomService.getInstance();
    async function fetchData() {
      const r = await rService.getById(roomid!);
      await rService.addStatesForRoomEquipment();
      setRoom(r);
    }
    fetchData();
  }, []);
  return (
    <Box sx={{ flexGrow: 1, height: "100vh" }}>
      <Grid container spacing={2}>
        <Grid className="grid-bg" item sm={12} md={6}>
          <div className="header-container">
            <ArrowBackIcon
              onClick={() => navigate("/")}
              fontSize="large"
              className="header-icon-size"
            ></ArrowBackIcon>
            <h1 className="header-font-size">Dashboard</h1>
          </div>

          <Grid
            className="infoboard-height"
            item
            xs={10}
            sx={{ margin: "auto" }}
          >
            <div>
              <InformationPanel room={room}></InformationPanel>
            </div>
          </Grid>
          <Grid item xs={10} sx={{ margin: "auto" }}>
            <ControlArea room={room} />
          </Grid>
        </Grid>
        <Grid className="grid-bg" item sm={12} md={6}>
          <Grid item className="chart-height" xs={10}>
            <TempAndCo2Chart room={room}></TempAndCo2Chart>
          </Grid>
          <Grid item className="chart-height" xs={10}>
            <PeopleChart room={room}></PeopleChart>
          </Grid>
          <Grid item className="chart-height" xs={10}>
            <SensorChart room={room} />
          </Grid>
        </Grid>
      </Grid>
    </Box>
  );
}
