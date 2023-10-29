import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Navbar from "../../components/Guest/Navbar";
import "../../assets/styles/Guest/History.css";

function History() {
  const [history, setHistory] = useState({});
  const navigate = useNavigate();

  useEffect(() => {
    // Get history from backend API
    fetch("https://localhost:7144/api/history/2")
      .then((response) => response.json())
      .then((data) => setHistory(data))
      .catch((error) =>
        console.error("Erreur lors de la récupération de l'histoire : ", error)
      );
  }, []);

  return (
    <div>
      <Navbar />
      <div className="history-page-container">
        <h1 className="history-page-article-title">Histoire du club</h1>
        <div className="history-page-article-content">{history.content}</div>
        <div className="history-page-back-button-container">
          <button
            className="history-page-back-button"
            onClick={() => navigate(-1)}
          >
            Retour
          </button>
        </div>
      </div>
    </div>
  );
}

export default History;
