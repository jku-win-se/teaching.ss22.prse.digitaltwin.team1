import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Main } from "./pages/main/main.page";
import { Details } from "./pages/details/details.page";

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
