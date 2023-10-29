import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import "../../../assets/styles/Admin/CompetitionSeasons/AddCompetitionSeason.css";

function AddCompetitionSeason() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const { competitionId } = useParams();
  const [competitionSeasonInfo, setCompetitionSeasonInfo] = useState({
    competitionId: competitionId,
    seasonId: "",
  });
  const [seasonsNotInCompetition, setSeasonsNotInCompetition] = useState([]);

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }
    // Function to fetch seasons AND seasons already in the competition season
    const fetchSeasonsAndCompetitionSeasons = async () => {
      try {
        // Call to the backend to get the list of seasons
        const seasonsResponse = await fetch(
          "https://localhost:7144/api/seasons",
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        if (!seasonsResponse.ok) {
          console.error("Erreur lors de la récupération des saisons");
          return;
        }

        const seasonsData = await seasonsResponse.json();
        setSeasonsNotInCompetition(seasonsData);

        // Call to backend to get season IDs already added to the competition
        const competitionSeasonsResponse = await fetch(
          `https://localhost:7144/api/competition/${competitionId}/seasons`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        if (!competitionSeasonsResponse.ok) {
          console.error(
            "Erreur lors de la récupération des saisons de la compétition"
          );
          return;
        }

        const competitionSeasonsData = await competitionSeasonsResponse.json();

        // Extract season IDs already added to the competition
        const seasonIdsInCompetition = competitionSeasonsData.map(
          (competitionSeason) => competitionSeason.season.id
        );

        // Filter seasons not already added to the competition
        const seasonsNotInCompetitionFiltered = seasonsData.filter(
          (season) => !seasonIdsInCompetition.includes(season.id)
        );

        setSeasonsNotInCompetition(seasonsNotInCompetitionFiltered);

        if (seasonsNotInCompetitionFiltered.length > 0) {
          setCompetitionSeasonInfo((prevInfo) => ({
            ...prevInfo,
            seasonId: seasonsNotInCompetitionFiltered[0].id,
          }));
        }
      } catch (error) {
        console.error("Une erreur s'est produite:", error);
      }
    };

    fetchSeasonsAndCompetitionSeasons();
  }, [competitionId, token]);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setCompetitionSeasonInfo((prevInfo) => ({
      ...prevInfo,
      [name]: value,
    }));
  };

  const handleAddCompetitionSeason = async () => {
    if (!token) {
      navigate("/login");
      return;
    }
    try {
      const response = await fetch(
        "https://localhost:7144/api/competition-season",
        {
          method: "POST",
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
          body: JSON.stringify(competitionSeasonInfo),
        }
      );

      if (!response.ok) {
        console.error("Erreur lors de l'ajout de la saison");
        return;
      }

      console.log("Saison ajoutée avec succès !");
      navigate(`/admin/competition/${competitionId}/seasons`);
    } catch (error) {
      console.error("Une erreur s'est produite:", error);
    }
  };

  const handleCancel = () => {
    navigate(`/admin/competition/${competitionId}/seasons`);
  };

  return (
    <div>
      <AdminNavbar />
      <div className="competition-season-add-content-container">
        <div className="competition-season-add-form-container">
          <h2>Ajouter une saison</h2>
          <form>
            <select
              name="seasonId"
              value={competitionSeasonInfo.seasonId}
              onChange={handleInputChange}
              required
            >
              {seasonsNotInCompetition.map((season) => (
                <option key={season.id} value={season.id}>
                  {season.name}
                </option>
              ))}
            </select>
            <div className="competition-season-add-buttons-container">
              <button
                type="button"
                onClick={handleAddCompetitionSeason}
                disabled={!competitionSeasonInfo.seasonId}
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

export default AddCompetitionSeason;
