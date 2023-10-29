import React, { useState, useEffect } from "react";
import Navbar from "../../components/Guest/Navbar";
import "../../assets/styles/Guest/Table.css";

function Table() {
  const [rankingData, setRankingData] = useState([]);

  useEffect(() => {
    // Get ranking from backend API
    fetch("https://localhost:7144/api/ranking?CompetitionSeasonId=3")
      .then((response) => response.json())
      .then((data) => setRankingData(data))
      .catch((error) =>
        console.error("Erreur lors de la récupération du classement : ", error)
      );
  }, []);

  return (
    <div>
      <Navbar />
      <table className="ranking-table">
        <thead>
          <tr>
            <th>Position</th>
            <th>Équipe</th>
            <th>Matchs joués</th>
            <th>Victoires</th>
            <th>Matchs nuls</th>
            <th>Défaites</th>
            <th>Buts marqués</th>
            <th>Buts encaissés</th>
            <th>Différence de buts</th>
            <th>Points</th>
          </tr>
        </thead>
        <tbody>
          {rankingData.map((teamData, index) => (
            <tr key={teamData.id}>
              <td className="ranking-table-td">{index + 1}</td>
              <td className="ranking-table-td-team-logo-name">
                <div className="ranking-table-team-info">
                  <img
                    src={`https://localhost:7144/Images/Teams/${teamData.team.logo}`}
                    alt={teamData.team.name}
                    className="ranking-table-team-logo"
                  />
                  <span>{teamData.team.name}</span>
                </div>
              </td>
              <td className="ranking-table-td">{teamData.played}</td>
              <td className="ranking-table-td">{teamData.wins}</td>
              <td className="ranking-table-td">{teamData.draws}</td>
              <td className="ranking-table-td">{teamData.losses}</td>
              <td className="ranking-table-td">{teamData.goalsFor}</td>
              <td className="ranking-table-td">{teamData.goalsAgainst}</td>
              <td className="ranking-table-td">{teamData.goalsDifference}</td>
              <td className="ranking-table-td">{teamData.points}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default Table;
