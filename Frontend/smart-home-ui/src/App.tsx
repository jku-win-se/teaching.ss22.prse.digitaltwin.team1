import "@fontsource/poppins";
import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./App.css";
import { Details } from "./pages/details/details.page";
import { Main } from "./pages/main/main.page";

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
