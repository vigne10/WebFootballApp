import React, { useState, useEffect } from "react";
import Navbar from "../../components/Guest/Navbar";
import PlayerCard from "../../components/Guest/PlayerCard";
import "../../assets/styles/Guest/Players.css";

function Players() {
  const [players, setPlayers] = useState([]);

  useEffect(() => {
    fetch("https://localhost:7144/api/players?TeamId=1")
      .then((response) => response.json())
      .then((data) => setPlayers(data))
      .catch((error) =>
        console.error("Erreur lors de la récupération des joueurs : ", error)
      );
  }, []);

  const groupedPlayers = {
    Gardien: [],
    Défenseur: [],
    Milieu: [],
    Attaquant: [],
  };

  players.forEach((player) => {
    groupedPlayers[player.position.name].push(player);
  });

  return (
    <div>
      <Navbar />
      <div className="players-page-container">
        {Object.entries(groupedPlayers).map(([position, positionPlayers]) => (
          <div key={position} className="players-position-section">
            <h2 className="players-position-title">
              {position === "Milieu" ? `${position}x` : `${position}s`}
            </h2>
            <div className="players-position-cards">
              {positionPlayers.map((player, index) => (
                <PlayerCard key={index} player={player} />
              ))}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Players;
