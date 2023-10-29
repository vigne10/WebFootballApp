import React, { useState, useEffect } from "react";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import { useNavigate, useParams } from "react-router-dom";
import "../../../assets/styles/Admin/CompetitionSeasons/AdminCompetitionSeasons.css";

function AdminCompetitionSeasons() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const { competitionId } = useParams();
  const [competitionSeasons, setCompetitionSeasons] = useState([]);
  const [competition, setCompetition] = useState([]);

  const handleDeleteCompetitionSeason = (competitionSeasonId) => {
    if (!token) {
      navigate("/login");
      return;
    }

    const confirmDelete = window.confirm(
      "Êtes-vous sûr de vouloir supprimer cette saison pour cette compétition ? La suppression de la saison entrainera la suppression de tous les matchs et du classement associé."
    );

    if (confirmDelete) {
      fetch(
        `https://localhost:7144/api/competition-season/${competitionSeasonId}`,
        {
          method: "DELETE",
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      )
        .then((response) => {
          if (response.status === 401) {
            localStorage.removeItem("token");
            navigate("/login");
            throw new Error("Token expiré ou invalide");
          }
          if (response.status === 204) {
            // Update list of competition seasons
            setCompetitionSeasons(
              competitionSeasons.filter(
                (competitionSeason) =>
                  competitionSeason.id !== competitionSeasonId
              )
            );
          } else {
            throw new Error(
              `Erreur lors de la suppression de la saison pour la compétition`
            );
          }
        })
        .catch((error) =>
          console.error(
            "Erreur lors de la suppression de la saison pour la compétition : ",
            error
          )
        );
    }
  };

  const handleCompetitionSeasonMatches = (competitionSeason) => {
    navigate(`/admin/competition-season/${competitionSeason.id}/matches`);
  };

  const handleCompetitionSeasonTeams = (competitionSeason) => {
    navigate(`/admin/competition-season/${competitionSeason.id}/teams`);
  };

  const handleCompetitionSeasonGenerateTable = (competitionSeasonId) => {
    if (!token) {
      navigate("/login");
      return;
    }

    const confirmGenerate = window.confirm(
      "Êtes-vous sûr de vouloir regénérer le classement pour cette saison ?"
    );

    if (confirmGenerate) {
      fetch(
        `https://localhost:7144/api/ranking/competition-season/${competitionSeasonId}`,
        {
          method: "POST",
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      )
        .then((response) => {
          if (response.status === 401) {
            localStorage.removeItem("token");
            navigate("/login");
            throw new Error("Token expiré ou invalide");
          }
          if (response.status === 204) {
            // Update list of competition seasons
            setCompetitionSeasons((prevSeasons) =>
              prevSeasons.map((season) =>
                season.id === competitionSeasonId
                  ? { ...season } // You can update any relevant properties here
                  : season
              )
            );
          } else {
            throw new Error("Erreur lors de la régénération du classement");
          }
        })
        .catch((error) =>
          console.error(
            "Erreur lors de la régénération du classement : ",
            error
          )
        );
    }
  };

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }

    // Call to backend to get competition details
    fetch(`https://localhost:7144/api/competition/${competitionId}`, {
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
      .then((data) => setCompetition(data))
      .catch((error) =>
        console.error(
          "Erreur lors de la récupération de la compétition : ",
          error
        )
      );

    // Call to backend to get seasons of this competition
    fetch(`https://localhost:7144/api/competition/${competitionId}/seasons`, {
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
      .then((data) => setCompetitionSeasons(data))
      .catch((error) =>
        console.error(
          "Erreur lors de la récupération des saisons pour cette compétition : ",
          error
        )
      );
  }, []);

  return (
    <div>
      <AdminNavbar />
      <div className="admin-competition-seasons-content-container">
        <div className="admin-competition-seasons-top-container">
          <div id="admin-competition-seasons-title-container">
            <img
              src={competition.logo}
              alt={`Logo de ${competition.name}`}
              className="admin-competition-seasons-competition-logo"
            />
            <h2>{competition.name}</h2>
          </div>
          <div id="admin-competition-seasons-add-button-container">
            <button
              id="admin-competition-seasons-add-button"
              onClick={() =>
                navigate(`/admin/competition/${competitionId}/seasons/add`)
              }
            >
              Ajouter une nouvelle saison
            </button>
          </div>
        </div>

        <ul className="competition-seasons-list">
          {competitionSeasons.map((competitionSeason) => (
            <li key={competitionSeason.id} className="competition-season-item">
              <div className="competition-season-info">
                <div className="competition-season-name">
                  {competitionSeason.season.name}
                </div>
              </div>
              <div className="competition-season-actions">
                <button
                  onClick={() =>
                    handleCompetitionSeasonGenerateTable(competitionSeason.id)
                  }
                >
                  Regénérer le classement
                </button>
                <button
                  onClick={() =>
                    handleCompetitionSeasonTeams(competitionSeason)
                  }
                >
                  Equipes
                </button>
                <button
                  onClick={() =>
                    handleCompetitionSeasonMatches(competitionSeason)
                  }
                >
                  Matchs
                </button>
                <button
                  onClick={() =>
                    handleDeleteCompetitionSeason(competitionSeason.id)
                  }
                >
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

export default AdminCompetitionSeasons;
