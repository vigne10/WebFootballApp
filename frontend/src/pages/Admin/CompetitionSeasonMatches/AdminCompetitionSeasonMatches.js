import React, { useState, useEffect } from "react";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import { useNavigate, useParams } from "react-router-dom";
import "../../../assets/styles/Admin/CompetitionSeasonMatches/AdminCompetitionSeasonMatches.css";
import { format } from "date-fns";
import { fr } from "date-fns/locale";

function AdminCompetitionSeasonMatches() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const { competitionSeasonId } = useParams();
  const [matches, setMatches] = useState([]);
  const [competitionSeason, setCompetitionSeason] = useState([]);

  const handleDeleteMatch = (matchId) => {
    if (!token) {
      navigate("/login");
      return;
    }

    const confirmDelete = window.confirm(
      "Êtes-vous sûr de vouloir supprimer ce match ? Le classement sera également mis à jour."
    );

    if (confirmDelete) {
      fetch(`https://localhost:7144/api/match/${matchId}`, {
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
            // Update list of matches
            setMatches(matches.filter((match) => match.id !== matchId));
          } else {
            throw new Error(`Erreur lors de la suppression du match`);
          }
        })
        .catch((error) =>
          console.error("Erreur lors de la suppression  du match : ", error)
        );
    }
  };

  const handleEditMatch = (matchId) => {
    navigate(`/admin/competition-season/3/match/${matchId}/edit`);
  };

  const handleResetScores = (matchId) => {
    if (!token) {
      navigate("/login");
      return;
    }

    const confirmReset = window.confirm(
      "Êtes-vous sûr de vouloir réinitialiser les scores de ce match ?"
    );

    if (confirmReset) {
      fetch(`https://localhost:7144/api/match/${matchId}/reset-scores`, {
        method: "POST",
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
            // Update list of matches
            setMatches((prevMatches) =>
              prevMatches.map((match) =>
                match.id === matchId
                  ? { ...match, homeTeamScore: null, awayTeamScore: null }
                  : match
              )
            );
          } else {
            throw new Error("Erreur lors de la réinitialisation des scores");
          }
        })
        .catch((error) =>
          console.error(
            "Erreur lors de la réinitialisation des scores : ",
            error
          )
        );
    }
  };

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/login");
      return;
    }

    // Call to backend to get CompetitionSeason details
    fetch(
      `https://localhost:7144/api/competition-season/${competitionSeasonId}`,
      {
        method: "GET",
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

    // Call to backend to get matches for this competition season
    fetch(
      `https://localhost:7144/api/matches/competition-season/${competitionSeasonId}`,
      {
        method: "GET",
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
      .then((data) => setMatches(data))
      .catch((error) =>
        console.error("Erreur lors de la récupération des matchs : ", error)
      );
  }, []);

  return (
    <div>
      <AdminNavbar />
      <div className="admin-competition-season-matches-content-container">
        <div className="admin-competition-season-matches-top-container">
          <div id="admin-competition-season-matches-title-container">
            {competitionSeason.competition ? (
              <>
                <img
                  src={competitionSeason.competition.logo}
                  alt={`Logo de ${competitionSeason.competition.name}`}
                  className="admin-competition-season-matches-competition-logo"
                />
                <h2>
                  {competitionSeason.competition.name} (
                  {competitionSeason.season.name}) : Gestion des matchs
                </h2>
              </>
            ) : (
              <p>Chargement...</p>
            )}
          </div>
          <div id="admin-competition-season-matches-add-button-container">
            <button
              id="admin-competition-season-matches-add-button"
              onClick={() =>
                navigate(
                  `/admin/competition-season/${competitionSeasonId}/matches/add`
                )
              }
            >
              Ajouter un nouveau match
            </button>
          </div>
        </div>

        <ul className="competition-season-matches-list">
          {matches.map((match) => (
            <li key={match.id} className="competition-season-match-item">
              <div className="competition-season-match-block">
                <div className="competition-season-match-teams-info">
                  <div className="competition-season-home-team-info">
                    {match.homeTeam.logo ? (
                      <img
                        className="competition-season-home-team-logo"
                        src={`https://localhost:7144/Images/Teams/${match.homeTeam.logo}`}
                        alt={`Logo de ${match.homeTeam.name}`}
                      />
                    ) : null}
                    <div
                      className={`${
                        match.homeTeam.logo
                          ? "competition-season-home-team-name"
                          : "competition-season-home-team-name-without-logo"
                      }`}
                    >
                      {match.homeTeam.name}
                    </div>
                  </div>

                  {match.homeTeamScore !== null && (
                    <div className="competition-season-match-home-score">
                      {match.homeTeamScore}
                    </div>
                  )}

                  <div
                    className={`${
                      match.homeTeamScore !== null &&
                      match.awayTeamScore !== null
                        ? "competition-season-match-teams-separator"
                        : "competition-season-match-teams-separator-without-scores"
                    }`}
                  >
                    -
                  </div>

                  {match.awayTeamScore !== null && (
                    <div className="competition-season-match-away-score">
                      {match.awayTeamScore}
                    </div>
                  )}

                  <div className="competition-season-away-team-info">
                    <div
                      className={`${
                        match.awayTeam.logo
                          ? "competition-season-away-team-name"
                          : "competition-season-away-team-name-without-logo"
                      }`}
                    >
                      {match.awayTeam.name}
                    </div>
                    {match.awayTeam.logo ? (
                      <img
                        className="competition-season-away-team-logo"
                        src={`https://localhost:7144/Images/Teams/${match.awayTeam.logo}`}
                        alt={`Logo de ${match.awayTeam.name}`}
                      />
                    ) : null}
                  </div>
                </div>
                <div className="competition-season-match-infos">
                  <div className="competition-season-match-schedule">
                    {format(new Date(match.schedule), "d MMMM yyyy 'à' HH:mm", {
                      locale: fr,
                    })}
                  </div>
                </div>
              </div>

              <div className="competition-season-match-actions">
                <button onClick={() => handleResetScores(match.id)}>
                  Réinitialiser les scores
                </button>
                <button onClick={() => handleEditMatch(match.id)}>
                  Modifier
                </button>
                <button onClick={() => handleDeleteMatch(match.id)}>
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

export default AdminCompetitionSeasonMatches;
