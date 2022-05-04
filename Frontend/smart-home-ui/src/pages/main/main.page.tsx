import AddIcon from '@mui/icons-material/Add';
import LayersOutlinedIcon from '@mui/icons-material/LayersOutlined';
import { Box, Button, Grid } from "@mui/material";
import * as React from "react";
import { Building } from '../../enums/building.enum';
import { IRoom } from '../../models/IRoom';
import FilterBar from "./components/filter-bar/filter-bar";
import RoomList from "./components/room-list/room-list.component";

export interface IMainProps { }

var roomCopy: IRoom[] = [];

export function Main(props: IMainProps) {
  const [rooms, setRooms] = React.useState<IRoom[]>(JSON.parse(JSON.stringify([{
    "size": 60,
    "name": "Lobby1",
    "roomType": "Unknown",
    "building": "Unknown",
    "roomEquipment": [
      {
        "roomID": "19f4ac27-db85-460f-ad6d-f3c5bcb26444",
        "equipmentRef": "5",
        "name": "Window",
        "id": "257c6cb4-6fa1-4e7f-ad45-162145159cbb"
      }
    ],
    "id": "19f4ac27-db85-460f-ad6d-f3c5bcb26444"
  },
  {
    "size": 50,
    "name": "room102",
    "roomType": "Unknown",
    "building": "Unknown",
    "roomEquipment": [
      {
        "roomID": "1c513df8-cb14-4394-b74f-233ff9eac038",
        "equipmentRef": "3",
        "name": "Window",
        "id": "202ad4d1-ece7-46ad-a886-1faebb675bd7"
      },
      {
        "roomID": "1c513df8-cb14-4394-b74f-233ff9eac038",
        "equipmentRef": "4",
        "name": "Window",
        "id": "b230a414-630a-412c-a513-3b95c8626c05"
      },
      {
        "roomID": "1c513df8-cb14-4394-b74f-233ff9eac038",
        "equipmentRef": "2",
        "name": "Ventilator",
        "id": "ab9aaf45-a813-451a-ad47-7fc173ca2bdf"
      }
    ],
    "id": "1c513df8-cb14-4394-b74f-233ff9eac038"
  },
  {
    "size": 100,
    "name": "room101",
    "roomType": "Unknown",
    "building": "Unknown",
    "roomEquipment": [
      {
        "roomID": "4de0cb8a-71ef-4139-92cd-41684762a733",
        "equipmentRef": "1",
        "name": "Window",
        "id": "fc6183c0-a989-4f5a-a8a1-20080ba965f6"
      },
      {
        "roomID": "4de0cb8a-71ef-4139-92cd-41684762a733",
        "equipmentRef": "2",
        "name": "Window",
        "id": "427564b1-e1e6-4de2-845b-be2e4f9cb359"
      },
      {
        "roomID": "4de0cb8a-71ef-4139-92cd-41684762a733",
        "equipmentRef": "1",
        "name": "Ventilator",
        "id": "5045ae47-02d1-4ea6-b7a8-6e93d953ce27"
      }
    ],
    "id": "4de0cb8a-71ef-4139-92cd-41684762a733"
  }])))

  React.useEffect(() => { roomCopy = rooms }, [])

  const changeFilterValue = (newValue: string) => {
    if (newValue === Building[1]) {
      setRooms(roomCopy)
    } else {
      setRooms(roomCopy.filter((val, idx, arr) => val.building === newValue))
    }
  }

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
            <FilterBar changeFilterValue={changeFilterValue} />
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
          <RoomList rooms={rooms} />
        </Grid>

      </Grid>
    </Box>
  );
}
