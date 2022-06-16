import { Alert, AlertColor, Snackbar } from "@mui/material";

export interface IMessageProps {
  message: string;
  open: boolean;
  handleClose: () => void;
  severity: AlertColor | undefined;
}

export default function Message({
  message,
  open,
  handleClose,
  severity,
}: IMessageProps) {
  return (
    <div>
      <Snackbar
        anchorOrigin={{ vertical: "top", horizontal: "right" }}
        open={open}
        autoHideDuration={6000}
        onClose={handleClose}
      >
        <Alert onClose={handleClose} severity={severity} sx={{ width: "100%" }}>
          {message}
        </Alert>
      </Snackbar>
    </div>
  );
}
