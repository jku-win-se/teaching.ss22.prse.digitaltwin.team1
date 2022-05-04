import AddIcon from '@mui/icons-material/Add';
import LayersOutlinedIcon from '@mui/icons-material/LayersOutlined';
import { Box, Button, Grid } from "@mui/material";
import * as React from "react";
import FilterBar from "./components/filter-bar/filter-bar";
import RoomList from "./components/room-list/room-list.component";

export interface IMainProps { }

export function Main(props: IMainProps) {
  return (
    <Box
      sx={{
        flexGrow: 1,
        height: "100vh"
      }}
    >
      <Grid
        container
        spacing={2}
      >
        <Grid
          className="grid-bg"
          item
          xs={10}
          sx={{ margin: "auto" }}
        >
          <h1 className="header-font-size">Smartrooms</h1>
        </Grid>

        <Grid
          className="filter-height"
          item
          xs={10}
          sx={{ margin: "auto" }}
          container justifyContent="flex-start"
          alignContent={"center"}
        >
          <Grid
            item
            xs={10}
            sx={{ margin: "auto 0" }}
          >
            <FilterBar />
          </Grid>

          <Grid
            item
            xs={2}
            container justifyContent="flex-end"
            alignContent={"center"}
          >
            <Button variant="text"
              startIcon={<AddIcon></AddIcon>}
              endIcon={<LayersOutlinedIcon></LayersOutlinedIcon>}
              size="large"
              onClick={() => {
                alert('add clicked');
              }}
            >
              add
            </Button>
          </Grid>
        </Grid>

        <Grid
          className="roomlist-height"
          item
          xs={10}
          sx={{ margin: "auto" }}
        >
          <RoomList />
        </Grid>

      </Grid>
    </Box>
  );
}
