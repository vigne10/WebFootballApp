import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import "../../../assets/styles/Admin/CompetitionSeasonTeams/AddCompetitionSeasonTeam.css";

function AddCompetitionSeasonTeam() {
  const { competitionSeasonId } = useParams();
  const [competitionSeasonTeamInfo, setCompetitionSeasonTeamInfo] = useState({
    team: "",
  });
  const [teamsNotInCompetition, setTeamsNotInCompetition] = useState([]);

  const token = localStorage.getItem("token");
  const navigate = useNavigate();

  useEffect(() => {
    // Function to fetch teams and teams already in the competition season
    const fetchTeamsAndCompetitionTeams = async () => {
      try {
        // Call to the backend to get the list of teams
        const teamsResponse = await fetch("https://localhost:7144/api/teams", {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        if (!teamsResponse.ok) {
          console.error("Erreur lors de la récupération des équipes");
          return;
        }

        const teamsData = await teamsResponse.json();
        setTeamsNotInCompetition(teamsData);

        // Call to backend to get teams already in the competition season
        const competitionTeamsResponse = await fetch(
          `https://localhost:7144/api/competition-season/teams?CompetitionSeasonId=${competitionSeasonId}`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        if (!competitionTeamsResponse.ok) {
          console.error(
            "Erreur lors de la récupération des équipes de la saison de compétition"
          );
          return;
        }

        const competitionTeamsData = await competitionTeamsResponse.json();

        // Extract teams IDs already in this season of the competition
        const teamIdsInCompetition = competitionTeamsData.map(
          (team) => team.team.id
        );

        // Filter teams not in this season of the competition
        const teamsNotInCompetitionFiltered = teamsData.filter(
          (team) => !teamIdsInCompetition.includes(team.id)
        );

        setTeamsNotInCompetition(teamsNotInCompetitionFiltered);

        if (teamsNotInCompetitionFiltered.length > 0) {
          setCompetitionSeasonTeamInfo((prevInfo) => ({
            ...prevInfo,
            team: teamsNotInCompetitionFiltered[0].id,
          }));
        }
      } catch (error) {
        console.error("Une erreur s'est produite:", error);
      }
    };

    fetchTeamsAndCompetitionTeams();
  }, [competitionSeasonId, token]);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setCompetitionSeasonTeamInfo((prevInfo) => ({
      ...prevInfo,
      [name]: value,
    }));
  };

  const handleAddCompetitionSeasonTeam = async () => {
    try {
      const response = await fetch(
        `https://localhost:7144/api/competition-season-team?TeamId=${competitionSeasonTeamInfo.team}&CompetitionSeasonId=${competitionSeasonId}`,
        {
          method: "POST",
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      if (!response.ok) {
        console.error("Erreur lors de l'ajout de l'équipe");
        return;
      }

      console.log("Equipe ajoutée avec succès !");
      navigate(`/admin/competition-season/${competitionSeasonId}/teams`);
    } catch (error) {
      console.error("Une erreur s'est produite:", error);
    }
  };

  const handleCancel = () => {
    navigate(`/admin/competition-season/${competitionSeasonId}/teams`);
  };

  return (
    <div>
      <AdminNavbar />
      <div className="competition-season-team-add-content-container">
        <div className="competition-season-team-add-form-container">
          <h2>Ajouter une équipe</h2>
          <form>
            <select
              name="team"
              value={competitionSeasonTeamInfo.team}
              onChange={handleInputChange}
              required
            >
              {teamsNotInCompetition.map((team) => (
                <option key={team.id} value={team.id}>
                  {team.name}
                </option>
              ))}
            </select>
            <div className="competition-season-team-add-buttons-container">
              <button
                type="button"
                onClick={handleAddCompetitionSeasonTeam}
                disabled={!competitionSeasonTeamInfo.team}
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

export default AddCompetitionSeasonTeam;
