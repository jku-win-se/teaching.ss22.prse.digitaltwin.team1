import { Delete } from "@mui/icons-material";
import { IconButton } from "@mui/material";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import * as React from "react";
import { IError } from "../../../../models/IError";
import { RoomService } from "../../../../services/Room.service";

export interface IDeleteDialogProp {
  roomId: string;
  triggerReload(): void;
  handleClose(): void;
}

const rService = RoomService.getInstance();

export default function DeleteDialog({
  roomId,
  triggerReload,
  handleClose,
}: IDeleteDialogProp) {
  const [open, setOpen] = React.useState(false);
  const [err, setErr] = React.useState<IError>({
    error: false,
    text: "",
    status: 200,
  });

  const handleClickOpen = () => {
    setOpen(true);
  };

  const cancel = async () => {
    setOpen(false);
  };

  const deleteAction = async () => {
    const error = await rService.delete(roomId);
    setErr(error);
    if (error.error) {
      triggerReload();
      handleClose();
    }
  };

  return (
    <div>
      <IconButton aria-label="edit room" onClick={handleClickOpen}>
        <Delete fontSize="large" />
      </IconButton>
      <Dialog
        open={open}
        onClose={cancel}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">{"Confirm delete"}</DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Are you sure you want to delete this item?
          </DialogContentText>
          {err.error ? (
            <h4 className="add-edit-err">{`${err.status}: ${err.text}`}</h4>
          ) : null}
        </DialogContent>
        <DialogActions>
          <Button onClick={cancel}>Cancel</Button>
          <Button onClick={deleteAction} autoFocus>
            Delete
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
