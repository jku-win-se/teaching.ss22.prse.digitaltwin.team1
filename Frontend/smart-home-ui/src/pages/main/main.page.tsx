import AddIcon from "@mui/icons-material/Add";
import LayersOutlinedIcon from "@mui/icons-material/LayersOutlined";
import { Box, Button, CircularProgress, Fab, Grid } from "@mui/material";
import * as React from "react";
import { Building } from "../../enums/building.enum";
import { IRoom } from "../../models/IRoom";
import { RoomService } from "../../services/Room.service";
import AddEditDialog from "./components/add-edit-dialog/add-edit-dialog.component";
import FilterBar from "./components/filter-bar/filter-bar";
import RoomList from "./components/room-list/room-list.component";
import "./main.style.css";

export interface IMainProps {}

const rService = RoomService.getInstance();

export function Main(props: IMainProps) {
  const [rooms, setRooms] = React.useState<IRoom[]>([]);
  const [isLoading, setIsLoading] = React.useState(true);
  const [open, setOpen] = React.useState(false);

  const fetchData = async () => {
    const r = await rService.getAll();
    setRooms(r);
    setIsLoading(false);
  };

  React.useEffect(() => {
    fetchData();
  }, []);

  const changeFilterValue = (newValue: keyof typeof Building) => {
    console.log(newValue);
    setRooms(rService.filterByBuilding(newValue));
  };

  const triggerReload = async () => {
    setIsLoading(true);
    await fetchData();
  };

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <Box
      sx={{
        flexGrow: 1,
        height: "100vh",
      }}
    >
      <Grid container spacing={2}>
        <Grid className="grid-bg" item xs={10} sx={{ margin: "auto" }}>
          <h1 className="header-font-size">Smartrooms</h1>
        </Grid>

        <Grid
          className="filter-height"
          item
          xs={10}
          sx={{ margin: "auto" }}
          container
          justifyContent="flex-start"
          alignContent={"center"}
        >
          <Grid item xs={10} sx={{ margin: "auto 0" }}>
            <FilterBar changeFilterValue={changeFilterValue} />
          </Grid>

          <Grid
            id="addRoomButton"
            item
            xs={2}
            container
            justifyContent="flex-end"
            alignContent={"center"}
          >
            <Button
              variant="text"
              startIcon={<AddIcon></AddIcon>}
              endIcon={<LayersOutlinedIcon></LayersOutlinedIcon>}
              size="large"
              onClick={handleClickOpen}
            >
              add
            </Button>
          </Grid>
        </Grid>

        {isLoading ? (
          <Grid
            className="progress-bar"
            item
            container
            xs={10}
            sx={{ margin: "auto" }}
          >
            <CircularProgress />
          </Grid>
        ) : (
          <Grid
            className="roomlist-height"
            item
            xs={10}
            sx={{ margin: "auto" }}
          >
            <RoomList triggerReload={triggerReload} rooms={rooms} />
          </Grid>
        )}
      </Grid>

      <Fab
        className="fab"
        id="addFab"
        color="primary"
        onClick={handleClickOpen}
      >
        <AddIcon />
      </Fab>
      {open ? (
        <AddEditDialog
          editMode={false}
          handleClose={handleClose}
          open={open}
          triggerReload={triggerReload}
        ></AddEditDialog>
      ) : null}
    </Box>
  );
}
