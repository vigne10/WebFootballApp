import React, { useState, useEffect } from "react";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import { useNavigate } from "react-router-dom";
import "../../../assets/styles/Admin/Competitions/AdminCompetitions.css";

function AdminCompetitions() {
  const token = localStorage.getItem("token");
  const [competitions, setCompetitions] = useState([]);
  const navigate = useNavigate();

  const handleConsultCompetition = (competitionId) => {
    navigate(`/admin/competition/${competitionId}/seasons`);
  };

  const handleDeleteCompetition = (competitionId) => {
    if (!token) {
      navigate("/login");
      return;
    }

    const confirmDelete = window.confirm(
      "Êtes-vous sûr de vouloir supprimer cette compétition ? La suppresion de la compétition va entraîner la suppression de toutes les saisons et de toutes les statistiques associées."
    );

    if (confirmDelete) {
      fetch(`https://localhost:7144/api/competition/${competitionId}`, {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
        .then((response) => {
          if (response.status === 401) {
            localStorage.removeItem("token");
            navigate("/login");
            throw new Error("Token expiré ou invalide");
          }
          if (response.status === 204) {
            // Update list of competitions
            setCompetitions(
              competitions.filter(
                (competition) => competition.id !== competitionId
              )
            );
          } else {
            throw new Error(
              `Erreur lors de la suppression de la compétition avec l'ID ${competitionId}`
            );
          }
        })
        .catch((error) =>
          console.error(
            "Erreur lors de la suppression de la compétition : ",
            error
          )
        );
    }
  };

  const handleEditCompetition = (competitionId) => {
    navigate(`/admin/competitions/edit/${competitionId}`);
  };

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }

    // Call to backend to get competitions
    fetch(`https://localhost:7144/api/competitions`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => {
        if (response.status === 401) {
          // Token expiré ou invalide, déconnecter l'utilisateur
          localStorage.removeItem("token");
          navigate("/login"); // Rediriger vers la page de connexion
          throw new Error("Token expiré ou invalide");
        }
        return response.json();
      })
      .then((data) => setCompetitions(data))
      .catch((error) =>
        console.error(
          "Erreur lors de la récupération des compétitions : ",
          error
        )
      );
  }, []);

  return (
    <div>
      <AdminNavbar />
      <div className="admin-competitions-content-container">
        <div className="admin-competitions-top-container">
          <div id="admin-competitions-title-container">
            <h2>Gestion des compétitions</h2>
          </div>
          <div id="admin-competitions-add-button-container">
            <button
              id="admin-competitions-add-button"
              onClick={() => navigate("/admin/competitions/add")}
            >
              Ajouter une nouvelle compétition
            </button>
          </div>
        </div>

        <ul className="competitions-list">
          {competitions.map((competition) => (
            <li key={competition.id} className="competition-item">
              <div className="competition-info">
                {competition.logo ? (
                  <img
                    className="competition-logo"
                    src={competition.logo}
                    alt={`Logo de ${competition.name}`}
                  />
                ) : null}
                <div
                  className={`${
                    competition.logo
                      ? "competition-name"
                      : "competition-name-without-logo"
                  }`}
                >
                  {competition.name}
                </div>
              </div>
              <div className="competition-actions">
                <button
                  onClick={() => handleConsultCompetition(competition.id)}
                >
                  Consulter
                </button>
                <button onClick={() => handleEditCompetition(competition.id)}>
                  Modifier
                </button>
                <button onClick={() => handleDeleteCompetition(competition.id)}>
                  Supprimer
                </button>
              </div>
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
}

export default AdminCompetitions;
