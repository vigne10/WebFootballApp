import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter as Router } from "react-router-dom";
import App from "./App";
import "./assets/styles/Common/Fonts.css";
import "./assets/styles/Common/Body.css";
import "./assets/styles/Common/RoundedTables.css";
import "./assets/styles/Common/Admin/AdminErrorStyle.css";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <Router>
    <React.StrictMode>
      <App />
    </React.StrictMode>
  </Router>
);
