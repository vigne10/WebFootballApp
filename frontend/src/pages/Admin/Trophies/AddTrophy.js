import React, { useState, useEffect } from "react";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import { useNavigate } from "react-router-dom";
import "../../../assets/styles/Admin/Trophies/AddTrophy.css";

function AddTrophy() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const [seasons, setSeasons] = useState([]);
  const [competitionSeasons, setCompetitionSeasons] = useState([]);
  const [selectedSeason, setSelectedSeason] = useState("");
  const [selectedCompetitionSeason, setSelectedCompetitionSeason] =
    useState("");

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }
    // Call to the backend to retrieve the list of seasons
    fetch("https://localhost:7144/api/seasons", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((data) => {
        setSeasons(data);
        if (data.length > 0) {
          setSelectedSeason(data[0].name);
        }
      })
      .catch((error) =>
        console.error("Erreur lors de la récupération des saisons:", error)
      );
  }, []);

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }
    // Call to the backend to retrieve the list of CompetitionSeasons based on the selected season
    if (selectedSeason) {
      fetch(`https://localhost:7144/api/competitions-seasons`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
        .then((response) => response.json())
        .then((data) => {
          // Filter CompetitionSeasons based on selected season
          const filteredCompetitionSeasons = data.filter(
            (competitionSeason) =>
              competitionSeason.season.name === selectedSeason
          );
          setCompetitionSeasons(filteredCompetitionSeasons);
          if (filteredCompetitionSeasons.length > 0) {
            setSelectedCompetitionSeason(filteredCompetitionSeasons[0].id);
          } else {
            setSelectedCompetitionSeason("");
          }
        })
        .catch((error) =>
          console.error(
            "Erreur lors de la récupération des CompetitionSeasons:",
            error
          )
        );
    }
  }, [selectedSeason]);

  const handleAddTrophy = () => {
    if (!token) {
      navigate("/login");
      return;
    }
    // Check if a CompetitionSeason has been selected
    if (!selectedCompetitionSeason) {
      console.error("Veuillez sélectionner une CompetitionSeason.");
      return;
    }

    // Add the trophy
    fetch(`https://localhost:7144/api/reward`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        competitionSeasonId: selectedCompetitionSeason,
        teamId: 1,
      }),
    })
      .then((response) => {
        if (!response.ok) {
          console.error("Erreur lors de l'ajout du trophée");
          return;
        }
        console.log("Trophée ajouté avec succès !");
        navigate(`/admin/trophies`);
      })
      .catch((error) =>
        console.error(
          "Une erreur s'est produite lors de l'ajout du trophée:",
          error
        )
      );
  };

  return (
    <div>
      <AdminNavbar />
      <div className="add-trophy-content-container">
        <div className="add-trophy-form-container">
          <h2>Ajouter un trophée</h2>
          <div className="add-trophy-selects-container">
            <select
              name="season"
              value={selectedSeason}
              onChange={(e) => setSelectedSeason(e.target.value)}
            >
              {seasons.map((season) => (
                <option key={season.id} value={season.name}>
                  {season.name}
                </option>
              ))}
            </select>
            <select
              name="competitionSeason"
              value={selectedCompetitionSeason}
              onChange={(e) => setSelectedCompetitionSeason(e.target.value)}
            >
              {competitionSeasons.map((competitionSeason) => (
                <option key={competitionSeason.id} value={competitionSeason.id}>
                  {competitionSeason.competition.name}
                </option>
              ))}
            </select>
          </div>
          <button type="button" onClick={handleAddTrophy}>
            Ajouter
          </button>
        </div>
      </div>
    </div>
  );
}

export default AddTrophy;
