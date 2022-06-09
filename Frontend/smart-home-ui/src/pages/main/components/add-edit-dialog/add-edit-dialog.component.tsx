import * as React from "react";
import useMediaQuery from "@mui/material/useMediaQuery";
import ClearIcon from "@mui/icons-material/Clear";
import { useTheme } from "@mui/material/styles";
import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  FormControl,
  FormControlLabel,
  Grid,
  MenuItem,
  Radio,
  RadioGroup,
  Select,
  SelectChangeEvent,
  TextField,
} from "@mui/material";
import "./add-edit-dialog.styles.css";
import { RoomType } from "../../../../enums/roomType.enum";
import { Building } from "../../../../enums/building.enum";
import { RoomService } from "../../../../services/Room.service";
import { Equipment } from "../../../../enums/equipment.enum";
import { IError } from "../../../../models/IError";

export interface IAddEditDialogProps {
  editMode: boolean;
  id?: string;
  name?: string;
  roomType?: string;
  building?: string;
  noOfVents?: number;
  size?: number;
  noOfPeople?: number;
  noOfDoors?: number;
  noOfWindows?: number;
  noOfLights?: number;
  open: boolean;
  handleClose(): void;
  triggerReload(): void;
}
const rService = RoomService.getInstance();

export default function AddEditDialog({
  handleClose,
  open,
  id,
  triggerReload,
  editMode,
}: IAddEditDialogProps) {
  const theme = useTheme();
  const fullScreen = useMediaQuery(theme.breakpoints.down("md"));
  const [building, setBuilding] = React.useState("");
  const [roomType, setRoomType] = React.useState(Object.keys(RoomType)[0]);
  const [name, setName] = React.useState("");
  const [noOfVents, setNoOfVents] = React.useState(0);
  const [noOfPeople, setNoOfPeople] = React.useState(0);
  const [noOfDoors, setNoOfDoors] = React.useState(0);
  const [noOfWindows, setNoOfWindows] = React.useState(0);
  const [noOfLights, setNoOfLights] = React.useState(0);
  const [size, setSize] = React.useState(0);
  const [err, setErr] = React.useState<IError>({
    error: false,
    text: "",
    status: 200,
  });

  React.useEffect(() => {
    async function fetchData(id: string) {
      const room = await rService.getById(id);
      console.log(room);
      setBuilding(room.building);
      setRoomType(room.roomType);
      setName(room.name);
      setNoOfVents(rService.getEquipmentNumber(Equipment.Ventilator) || 0);
      setNoOfPeople(room.peopleCount);
      setNoOfDoors(rService.getEquipmentNumber(Equipment.Door) || 0);
      setNoOfWindows(rService.getEquipmentNumber(Equipment.Window) || 0);
      setNoOfLights(rService.getEquipmentNumber(Equipment.Light) || 0);
      setSize(room.size);
    }
    if (id) {
      fetchData(id);
    }

    return () => {
      console.log("Destruction");
      setBuilding("");
      setRoomType(RoomType.Lab);
      setName("");
      setNoOfVents(0);
      setNoOfPeople(0);
      setNoOfDoors(0);
      setNoOfWindows(0);
      setNoOfLights(0);
      setSize(0);
    };
  }, []);

  const save = async () => {
    if (name === "") {
      setErr({
        error: true,
        text: "Name is required",
        status: 400,
      });
    } else if (size === 0) {
      setErr({
        error: true,
        text: "Size is required",
        status: 400,
      });
    } else if (noOfPeople === 0) {
      setErr({
        error: true,
        text: "Number of people is required",
        status: 400,
      });
    } else {
      const err = await rService.addOrChange(
        id,
        noOfPeople,
        name,
        size,
        roomType,
        building,
        {
          [Equipment.Ventilator]: noOfVents,
          [Equipment.Door]: noOfDoors,
          [Equipment.Window]: noOfWindows,
          [Equipment.Light]: noOfLights,
        },
        editMode
      );
      console.log(err);
      setErr(err);
    }

    if (!err.error) {
      triggerReload();
      handleClose();
    }
  };
  return (
    <div>
      <Dialog
        fullScreen={fullScreen}
        open={open}
        onClose={handleClose}
        aria-labelledby="responsive-dialog-title"
      >
        <DialogTitle
          className="add-edit-dialog-header"
          id="responsive-dialog-title"
        >
          <ClearIcon onClick={handleClose}></ClearIcon>{" "}
          {editMode ? "Edit" : "Add"} a Smart Room
        </DialogTitle>
        <DialogContent>
          <Grid container spacing={2}>
            <Grid item sm={8} xs={12}>
              <h5 className="add-edit-dialog-sub-header"> Room details *</h5>
              <Grid container>
                <Grid className="add-edit-dialog-padding" item xs={12}>
                  <TextField
                    className="add-edit-dialog-name"
                    id="standard-basic"
                    value={name}
                    onChange={(event) => {
                      setName(event.target.value);
                    }}
                    label="Name"
                    variant="standard"
                    margin="dense"
                  />
                </Grid>

                <Grid className="add-edit-dialog-padding" item sm={12} xs={6}>
                  <TextField
                    InputProps={{ inputProps: { min: 0 } }}
                    value={size}
                    onChange={(event) => {
                      setSize(+event.target.value);
                    }}
                    id="standard-basic"
                    label="Size [m^2]"
                    variant="standard"
                    type="number"
                    margin="dense"
                  />
                </Grid>
                <Grid className="add-edit-dialog-padding" item sm={12} xs={6}>
                  <TextField
                    InputProps={{ inputProps: { min: 0 } }}
                    value={noOfPeople}
                    onChange={(event) => {
                      setNoOfPeople(+event.target.value);
                    }}
                    id="standard-basic"
                    label="Max People"
                    variant="standard"
                    type="number"
                    margin="dense"
                  />
                </Grid>
                <Grid className="add-edit-dialog-padding" item sm={12} xs={6}>
                  <TextField
                    InputProps={{ inputProps: { min: 0 } }}
                    value={noOfWindows}
                    onChange={(event) => {
                      setNoOfWindows(+event.target.value);
                    }}
                    id="standard-basic"
                    label="Windows"
                    variant="standard"
                    type="number"
                    margin="dense"
                  />
                </Grid>
                <Grid className="add-edit-dialog-padding" item sm={12} xs={6}>
                  <TextField
                    InputProps={{ inputProps: { min: 0 } }}
                    value={noOfDoors}
                    onChange={(event) => {
                      setNoOfDoors(+event.target.value);
                    }}
                    id="standard-basic"
                    label="Doors"
                    variant="standard"
                    type="number"
                    margin="dense"
                  />
                </Grid>
                <Grid className="add-edit-dialog-padding" item sm={12} xs={6}>
                  <TextField
                    InputProps={{ inputProps: { min: 0 } }}
                    value={noOfVents}
                    onChange={(event) => {
                      setNoOfVents(+event.target.value);
                    }}
                    id="standard-basic"
                    label="Ventilators"
                    variant="standard"
                    type="number"
                    margin="dense"
                  />
                </Grid>
                <Grid className="add-edit-dialog-padding" item sm={12} xs={6}>
                  <TextField
                    InputProps={{ inputProps: { min: 0 } }}
                    value={noOfLights}
                    onChange={(event) => {
                      setNoOfLights(+event.target.value);
                    }}
                    id="standard-basic"
                    label="Lights"
                    variant="standard"
                    type="number"
                    margin="dense"
                  />
                </Grid>
              </Grid>
            </Grid>
            <Grid item sm={4} xs={12}>
              <h5 className="add-edit-dialog-sub-header">Room type *</h5>
              <RadioGroup
                value={roomType}
                onChange={(event, value) => {
                  console.log(Object.entries(RoomType));
                  console.log(value);
                  console.log(RoomType[value as keyof typeof RoomType]);
                  setRoomType(value);
                }}
              >
                {Object.entries(RoomType).map((val) => (
                  <FormControlLabel
                    key={val[0]}
                    value={val[0]}
                    control={<Radio />}
                    label={val[1] as string}
                  />
                ))}
              </RadioGroup>
              <h5 className="add-edit-dialog-sub-header">Building *</h5>
              <FormControl fullWidth variant="standard" sx={{ minWidth: 120 }}>
                <Select
                  labelId="demo-simple-select-standard-label"
                  id="demo-simple-select-standard"
                  value={building}
                  onChange={(event: SelectChangeEvent) =>
                    setBuilding(event.target.value)
                  }
                >
                  {Object.entries(Building)
                    .slice(1)
                    .map((val) => (
                      <MenuItem key={val[0]} value={val[0]}>
                        {val[1] as string}
                      </MenuItem>
                    ))}
                </Select>
              </FormControl>

              {err.error ? (
                <h4 className="add-edit-err">{`${err.status}: ${err.text}`}</h4>
              ) : null}
            </Grid>
          </Grid>
        </DialogContent>
        <DialogActions>
          <Button autoFocus onClick={handleClose}>
            Cancel
          </Button>
          <Button onClick={save} autoFocus>
            Save
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
