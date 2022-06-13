import "@fontsource/poppins";
import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./App.css";
import Message from "./components/message/message.component";
import { IMeasureState } from "./models/IMeasureState";
import { Details } from "./pages/details/details.page";
import { Main } from "./pages/main/main.page";
import { RoomService } from "./services/Room.service";
import { StateService } from "./services/State.service";

function App() {
  const [open, setOpen] = React.useState(false);
  const [message, setMessage] = React.useState("");

  React.useEffect(() => {
    const sService = StateService.getInstance();
    const rService = RoomService.getInstance();
    sService.establishWsConnection();
    sService.hubConnection.on("Alarm", async (data: IMeasureState) => {
      const r = await rService.getById(data.entityRefID, false);
      setMessage(
        `Temperature in Room ${r.name} is ${data.value.toLocaleString(
          undefined,
          { maximumFractionDigits: 1 }
        )} °C!`
      );
      setOpen(true);
    });
    return () => {
      sService.hubConnection.off("Alarm");
      sService.closeWsConnection();
    };
  }, []);

  const handleClose = () => {
    setOpen(false);
  };
  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Main />} />
          <Route path="details/:roomid" element={<Details />} />
        </Routes>
      </BrowserRouter>
      <Message
        message={message}
        handleClose={handleClose}
        open={open}
        severity="warning"
      ></Message>
    </div>
  );
}

export default App;
