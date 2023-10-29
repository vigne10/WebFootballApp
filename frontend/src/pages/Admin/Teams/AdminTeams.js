import React, { useState, useEffect } from "react";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import { useNavigate } from "react-router-dom";
import "../../../assets/styles/Admin/Teams/AdminTeams.css";

function AdminTeams() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const [teams, setTeams] = useState([]);
  const [errorMessage, setErrorMessage] = useState("");

  const handleDeleteTeam = (teamId) => {
    if (!token) {
      navigate("/login");
      return;
    }

    const confirmDelete = window.confirm(
      "Êtes-vous sûr de vouloir supprimer cette équipe ? Toutes les informations liées à cette équipe seront également supprimées."
    );

    if (confirmDelete) {
      fetch(`https://localhost:7144/api/team/${teamId}`, {
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
            // Update list of teams
            setTeams(teams.filter((team) => team.id !== teamId));
          } else {
            const errorData = response.json();
            setErrorMessage(
              `Erreur lors de la suppresion de l'équipe : ${errorData.message}`
            );
          }
        })
        .catch((error) =>
          console.error("Erreur lors de la suppression de l'équipe : ", error)
        );
    }
  };

  const handleEditTeam = (teamId) => {
    navigate(`/admin/teams/edit/${teamId}`);
  };

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }

    // Call to backend to get teams
    fetch(`https://localhost:7144/api/teams`, {
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
      .then((data) => setTeams(data))
      .catch((error) =>
        console.error("Erreur lors de la récupération des équipes : ", error)
      );
  }, []);

  return (
    <div>
      <AdminNavbar />
      <div className="admin-teams-content-container">
        <div className="error-message-container">
          {errorMessage && <div className="error-message">{errorMessage}</div>}
        </div>

        <div className="admin-teams-top-container">
          <div id="admin-teams-title-container">
            <h2>Gestion des équipes</h2>
          </div>
          <div id="admin-teams-add-button-container">
            <button
              id="admin-teams-add-button"
              onClick={() => navigate("/admin/teams/add")}
            >
              Ajouter une nouvelle équipe
            </button>
          </div>
        </div>

        <ul className="teams-list">
          {teams.map((team) => (
            <li key={team.id} className="team-item">
              <div className="team-info">
                {team.logo ? (
                  <img
                    className="team-logo"
                    src={team.logo}
                    alt={`Logo de ${team.name}`}
                  />
                ) : null}
                <div
                  className={`${
                    team.logo ? "team-name" : "team-name-without-logo"
                  }`}
                >
                  {team.name}
                </div>
              </div>
              <div className="team-actions">
                <button onClick={() => handleEditTeam(team.id)}>
                  Modifier
                </button>
                <button onClick={() => handleDeleteTeam(team.id)}>
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

export default AdminTeams;
