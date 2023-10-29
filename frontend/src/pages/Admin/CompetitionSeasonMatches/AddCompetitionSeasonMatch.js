import React, { useState, useEffect } from "react";
// import { format } from "date-fns";
// import { fr } from "date-fns/locale";
import { useNavigate, useParams } from "react-router-dom";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import "../../../assets/styles/Admin/CompetitionSeasonMatches/AddCompetitionSeasonMatch.css";

function AddCompetitionSeasonMatch() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const { competitionSeasonId } = useParams();
  const [matchInfo, setMatchInfo] = useState({
    competitionSeason: competitionSeasonId,
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
    // Call to the backend to retrieve the list of teams
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

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }
    if (competitionSeasonTeams.length > 1) {
      setMatchInfo((prevInfo) => ({
        ...prevInfo,
        homeTeam: competitionSeasonTeams[0].team.id,
        awayTeam: competitionSeasonTeams[1].team.id,
      }));
    }
  }, [competitionSeasonTeams]);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setMatchInfo((prevInfo) => ({ ...prevInfo, [name]: value }));
  };

  const handleAddMatch = async () => {
    try {
      if (!token) {
        navigate("/login");
        return;
      }
      // Call to backend to add match
      const response = await fetch("https://localhost:7144/api/match", {
        method: "POST",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          competitionSeasonId: matchInfo.competitionSeason,
          homeTeamId: matchInfo.homeTeam,
          awayTeamId: matchInfo.awayTeam,
          schedule: matchInfo.schedule,
          homeTeamScore: matchInfo.homeTeamScore,
          awayTeamScore: matchInfo.awayTeamScore,
        }),
      });

      if (!response.ok) {
        console.error("Erreur lors de l'ajout du match");
        return;
      }

      console.log("Match ajouté avec succès !");
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
      <div className="add-match-content-container">
        <div className="add-match-form-container">
          <h2>Ajouter un match</h2>
          <form>
            <select
              name="homeTeam"
              value={matchInfo.homeTeam}
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
              value={matchInfo.awayTeam}
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
            <div className="add-match-buttons-container">
              <button
                type="button"
                onClick={handleAddMatch}
                disabled={!isFormValid()}
              >
                Ajouter
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

export default AddCompetitionSeasonMatch;
