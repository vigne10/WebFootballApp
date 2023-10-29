import React, { useState, useEffect } from "react";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import { useNavigate, useParams } from "react-router-dom";
import "../../../assets/styles/Admin/CompetitionSeasonTeams/AdminCompetitionSeasonTeams.css";

function AdminCompetitionSeasonTeams() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const { competitionSeasonId } = useParams();
  const [competitionSeasonTeams, setCompetitionSeasonTeams] = useState([]);
  const [competitionSeason, setCompetitionSeason] = useState([]);

  const handleDeleteCompetitionSeasonTeam = (competitionSeasonTeamId) => {
    if (!token) {
      navigate("/login");
      return;
    }

    const confirmDelete = window.confirm(
      "Êtes-vous sûr de vouloir supprimer cette équipe pour cette saison ? Tous les matchs de cette équipe pour cette saison seront également supprimés."
    );

    if (confirmDelete) {
      fetch(
        `https://localhost:7144/api/competition-season-team/${competitionSeasonTeamId}`,
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
            // Update list of the teams of this season
            setCompetitionSeasonTeams(
              competitionSeasonTeams.filter(
                (competitionSeasonTeam) =>
                  competitionSeasonTeam.id !== competitionSeasonTeamId
              )
            );
          } else {
            throw new Error(
              `Erreur lors de la suppression de l'équipe pour cette saison`
            );
          }
        })
        .catch((error) =>
          console.error(
            "Erreur lors de la suppression de l'équipe pour cette saison : ",
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

    // Call to backend to get CompetitionSeason details
    fetch(
      `https://localhost:7144/api/competition-season/${competitionSeasonId}`,
      {
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
        return response.json();
      })
      .then((data) => setCompetitionSeason(data))
      .catch((error) =>
        console.error(
          "Erreur lors de la récupération des informations : ",
          error
        )
      );
    console.log(competitionSeason);

    // Call to backend to get teams of this competition for this season
    fetch(
      `https://localhost:7144/api/competition-season/teams?CompetitionSeasonId=${competitionSeasonId}`,
      {
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
        return response.json();
      })
      .then((data) => setCompetitionSeasonTeams(data))
      .catch((error) =>
        console.error(
          "Erreur lors de la récupération des équipes pour cette saison : ",
          error
        )
      );
  }, []);

  return (
    <div>
      <AdminNavbar />
      <div className="admin-competition-season-teams-content-container">
        <div className="admin-competition-season-teams-top-container">
          <div id="admin-competition-season-teams-title-container">
            {competitionSeason.competition ? (
              <>
                <img
                  src={competitionSeason.competition.logo}
                  alt={`Logo de ${competitionSeason.competition.name}`}
                  className="admin-competition-season-teams-competition-logo"
                />
                <h2>
                  {competitionSeason.competition.name} (
                  {competitionSeason.season.name}) : Gestion des équipes
                </h2>
              </>
            ) : (
              <p>Chargement...</p>
            )}
          </div>
          <div id="admin-competition-season-teams-add-button-container">
            <button
              id="admin-competition-season-teams-add-button"
              onClick={() =>
                navigate(
                  `/admin/competition-season/${competitionSeasonId}/teams/add`
                )
              }
            >
              Ajouter une nouvelle équipe
            </button>
          </div>
        </div>

        <ul className="competition-season-teams-list">
          {competitionSeasonTeams.map((competitionSeasonTeam) => (
            <li
              key={competitionSeasonTeam.team.id}
              className="competition-season-team-item"
            >
              <div className="competition-season-team-info">
                {competitionSeasonTeam.team.logo ? (
                  <img
                    className="competition-season-team-logo"
                    src={competitionSeasonTeam.team.logo}
                    alt={`Logo de ${competitionSeasonTeam.team.name}`}
                  />
                ) : null}
                <div
                  className={`${
                    competitionSeasonTeam.team.logo
                      ? "competition-season-team-name"
                      : "competition-season-team-name-without-logo"
                  }`}
                >
                  {competitionSeasonTeam.team.name}
                </div>
              </div>
              <div className="competition-season-team-actions">
                <button
                  onClick={() =>
                    handleDeleteCompetitionSeasonTeam(competitionSeasonTeam.id)
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

export default AdminCompetitionSeasonTeams;
