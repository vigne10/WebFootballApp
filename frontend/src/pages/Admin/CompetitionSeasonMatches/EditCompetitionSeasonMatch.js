import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import "../../../assets/styles/Admin/CompetitionSeasonMatches/EditCompetitionSeasonMatch.css";

function EditCompetitionSeasonMatch() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const { competitionSeasonId, matchId } = useParams();
  const [matchInfo, setMatchInfo] = useState({
    homeTeam: "",
    awayTeam: "",
    schedule: "",
    homeTeamScore: null,
    awayTeamScore: null,
  });
  const [competitionSeasonTeams, setCompetitionSeasonTeams] = useState([]);

  const isFormValid = () => {
    return matchInfo.homeTeam && matchInfo.awayTeam;
  };

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }
    // Call to the backend to get match details
    fetch(`https://localhost:7144/api/match/${matchId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((data) => {
        setMatchInfo({
          id: data.id,
          competitionSeason: data.competitionSeason,
          homeTeam: data.homeTeam,
          awayTeam: data.awayTeam,
          schedule: data.schedule.substring(0, 16),
          homeTeamScore: data.homeTeamScore,
          awayTeamScore: data.awayTeamScore,
        });
      })
      .catch((error) =>
        console.error(
          "Erreur lors de la récupération des détails du match : ",
          error
        )
      );

    // Call to the backend to get the list of teams
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
      .then((data) => {
        setCompetitionSeasonTeams(data);
        if (data.length > 0) {
          setMatchInfo((prevInfo) => ({
            ...prevInfo,
            team: data[0].id,
          }));
        }
      })
      .catch((error) =>
        console.error("Erreur lors de la récupération des équipes:", error)
      );
  }, []);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setMatchInfo((prevInfo) => ({ ...prevInfo, [name]: value }));
  };

  const handleEditMatch = async () => {
    try {
      if (!token) {
        navigate("/login");
        return;
      }
      // Call to backend to edit match
      const response = await fetch(
        `https://localhost:7144/api/match/${matchId}`,
        {
          method: "PUT",
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            id: matchId,
            competitionSeasonId: competitionSeasonId,
            homeTeamId: matchInfo.homeTeam.id,
            awayTeamId: matchInfo.awayTeam.id,
            schedule: matchInfo.schedule,
            homeTeamScore: matchInfo.homeTeamScore,
            awayTeamScore: matchInfo.awayTeamScore,
          }),
        }
      );

      if (!response.ok) {
        console.error("Erreur lors de la modification du match");
        return;
      }

      console.log("Match modifié avec succès !");
      navigate(`/admin/competition-season/${competitionSeasonId}/matches`);
    } catch (error) {
      console.error("Une erreur s'est produite:", error);
    }
  };

  const handleCancel = () => {
    navigate(`/admin/competition-season/${competitionSeasonId}/matches`);
  };

  return (
    <div>
      <AdminNavbar />
      <div className="edit-match-content-container">
        <div className="edit-match-form-container">
          <h2>Modifier un match</h2>
          <form>
            <select
              name="homeTeam"
              value={matchInfo.homeTeam.id}
              onChange={handleInputChange}
            >
              {competitionSeasonTeams.map((competitionSeasonTeam) => (
                <option
                  key={competitionSeasonTeam.team.id}
                  value={competitionSeasonTeam.team.id}
                >
                  {competitionSeasonTeam.team.name}
                </option>
              ))}
            </select>
            <select
              name="awayTeam"
              value={matchInfo.awayTeam.id}
              onChange={handleInputChange}
            >
              {competitionSeasonTeams.map((competitionSeasonTeam) => (
                <option
                  key={competitionSeasonTeam.team.id}
                  value={competitionSeasonTeam.team.id}
                >
                  {competitionSeasonTeam.team.name}
                </option>
              ))}
            </select>
            <input
              type="datetime-local"
              name="schedule"
              value={matchInfo.schedule}
              onChange={handleInputChange}
            />
            <input
              type="number"
              name="homeTeamScore"
              value={matchInfo.homeTeamScore}
              onChange={handleInputChange}
              placeholder="Score à domicile"
              required
            />
            <input
              type="number"
              name="awayTeamScore"
              value={matchInfo.awayTeamScore}
              onChange={handleInputChange}
              placeholder="Score à l'extérieur"
              required
            />
            <div className="edit-match-buttons-container">
              <button
                type="button"
                onClick={handleEditMatch}
                disabled={!isFormValid()}
              >
                Modifier
              </button>
              <button type="button" onClick={handleCancel}>
                Annuler
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}

export default EditCompetitionSeasonMatch;
