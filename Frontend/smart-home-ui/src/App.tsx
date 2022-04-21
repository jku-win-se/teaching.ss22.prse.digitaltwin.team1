import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Main } from "./pages/main/main.page";
import { Details } from "./pages/details/details.page";
import CssBaseline from "@mui/material/CssBaseline";
import "@fontsource/poppins";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Main />} />
        <Route path="details/:roomid" element={<Details />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
