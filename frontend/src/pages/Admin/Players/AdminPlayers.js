import React, { useState, useEffect } from "react";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import { useNavigate } from "react-router-dom";
import "../../../assets/styles/Admin/Players/AdminPlayers.css";

function AdminPlayers() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const [players, setPlayers] = useState([]);

  const handleDeletePlayer = (playerId) => {
    if (!token) {
      navigate("/login");
      return;
    }

    const confirmDelete = window.confirm(
      "Êtes-vous sûr de vouloir supprimer ce joueur ?"
    );

    if (confirmDelete) {
      fetch(`https://localhost:7144/api/player/${playerId}`, {
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
            // Update list of players
            setPlayers(players.filter((player) => player.id !== playerId));
          } else {
            throw new Error(
              `Erreur lors de la suppression du joueur avec l'ID ${playerId}`
            );
          }
        })
        .catch((error) =>
          console.error("Erreur lors de la suppression du joueur:", error)
        );
    }
  };

  const handleEditPlayer = (playerId) => {
    navigate(`/admin/players/edit/${playerId}`);
  };

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }

    // Call to backend to get players
    const teamId = 1;
    fetch(`https://localhost:7144/api/players?TeamId=${teamId}`, {
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
      .then((data) => setPlayers(data))
      .catch((error) =>
        console.error("Erreur lors de la récupération des joueurs:", error)
      );
  }, []);

  return (
    <div>
      <AdminNavbar />
      <div className="admin-players-content-container">
        <div className="admin-players-top-container">
          <div id="admin-players-title-container">
            <h2>Gestion des joueurs</h2>
          </div>
          <div id="admin-players-add-button-container">
            <button
              id="admin-players-add-button"
              onClick={() => navigate("/admin/players/add")}
            >
              Ajouter un nouveau joueur
            </button>
          </div>
        </div>

        <ul className="player-list">
          {players.map((player) => (
            <li key={player.id} className="player-item">
              <div className="player-info">
                <div className="player-name">
                  {player.surname} {player.name}
                </div>
                <div className="player-position">{player.position.name}</div>
              </div>
              <div className="player-actions">
                <button onClick={() => handleEditPlayer(player.id)}>
                  Modifier
                </button>
                <button onClick={() => handleDeletePlayer(player.id)}>
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

export default AdminPlayers;
