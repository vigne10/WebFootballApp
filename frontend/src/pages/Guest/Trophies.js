import React, { useState, useEffect } from "react";
import Navbar from "../../components/Guest/Navbar";
import "../../assets/styles/Guest/Trophies.css";

function Trophies() {
  const [trophies, setTrophies] = useState([]);

  useEffect(() => {
    // Get trophies from backend API
    fetch("https://localhost:7144/api/rewards/team/1")
      .then((response) => response.json())
      .then((data) => {
        // Use reduce to aggregate trophies by competition
        const aggregatedTrophies = data.reduce((accumulator, trophy) => {
          const competitionName = trophy.competitionSeason.competition.name;
          accumulator[competitionName] = accumulator[competitionName] || {
            competition: trophy.competitionSeason.competition,
            numWins: 0,
            seasons: [],
          };
          accumulator[competitionName].numWins++;
          if (trophy.competitionSeason.season.name) {
            accumulator[competitionName].seasons.push(
              trophy.competitionSeason.season.name
            );
          }
          return accumulator;
        }, {});

        setTrophies(Object.values(aggregatedTrophies));
      })
      .catch((error) =>
        console.error("Erreur lors de la récupération des trophées : ", error)
      );
  }, []);

  return (
    <div>
      <Navbar />
      <div className="trophies-container">
        <table className="trophies-table">
          <thead>
            <tr>
              <th>Compétition</th>
              <th>Nombre de victoires</th>
              <th>Saisons de victoire</th>
            </tr>
          </thead>
          <tbody>
            {trophies.map((trophy, index) => (
              <tr key={index}>
                <td>
                  <div className="trophies-table-competition-info">
                    <img
                      src={`https://localhost:7144/Images/Competitions/${trophy.competition.logo}`}
                      alt={`Logo de ${trophy.competition.name}`}
                      className="competition-logo"
                    />{" "}
                    {trophy.competition.name}
                  </div>
                </td>
                <td>{trophy.numWins}</td>
                <td>{trophy.seasons.join(", ")}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default Trophies;
