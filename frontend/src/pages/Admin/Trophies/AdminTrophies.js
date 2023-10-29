import React, { useState, useEffect } from "react";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import { useNavigate } from "react-router-dom";
import "../../../assets/styles/Admin/Trophies/AdminTrophies.css";

function AdminTrophies() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const [trophies, setTrophies] = useState([]);

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }

    // Call to backend to get list of trophies
    fetch("https://localhost:7144/api/rewards/team/1", {
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
        return response.json();
      })
      .then((data) => setTrophies(data))
      .catch((error) =>
        console.error("Erreur lors de la récupération des trophées : ", error)
      );
  }, []);

  const handleDeleteTrophy = (trophyId) => {
    if (!token) {
      navigate("/login");
      return;
    }

    const confirmDelete = window.confirm(
      "Êtes-vous sûr de vouloir supprimer ce trophée ?"
    );

    if (confirmDelete) {
      fetch(`https://localhost:7144/api/reward/${trophyId}`, {
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
            // Update list of trophies
            setTrophies(trophies.filter((trophy) => trophy.id !== trophyId));
          } else {
            throw new Error(`Erreur lors de la suppression du trophée`);
          }
        })
        .catch((error) =>
          console.error("Erreur lors de la suppression du trophée : ", error)
        );
    }
  };

  return (
    <div>
      <AdminNavbar />
      <div className="admin-trophies-content-container">
        <div className="admin-trophies-top-container">
          <div id="admin-trophies-title-container">
            <h2>Gestion du palmarès</h2>
          </div>
          <div id="admin-trophies-add-button-container">
            <button
              id="admin-add-trophy-button"
              onClick={() => navigate("/admin/trophy/add")}
            >
              Ajouter un nouveau trophée
            </button>
          </div>
        </div>

        <ul className="admin-trophies-list">
          {trophies.map((trophy) => (
            <li key={trophy.id} className="admin-trophy-item">
              <div className="admin-trophy-info">
                <img
                  src={`https://localhost:7144/Images/Competitions/${trophy.competitionSeason.competition.logo}`}
                  alt={`Logo de ${trophy.competitionSeason.competition.name}`}
                  className="admin-trophy-competition-logo"
                />
                <div className="admin-trophy-competition-name">
                  {trophy.competitionSeason.competition.name} (
                  {trophy.competitionSeason.season.name})
                </div>
              </div>
              <div className="admin-trophy-actions">
                <button onClick={() => handleDeleteTrophy(trophy.id)}>
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

export default AdminTrophies;
