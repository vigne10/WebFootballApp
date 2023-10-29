import React from "react";
import { format, parseISO } from "date-fns";
import { fr } from "date-fns/locale";
import "../../assets/styles/Common/Guest/PlayerCard.css";

function PlayerCard({ player }) {
  if (player.birthday) {
    const birthdayDate = parseISO(player.birthday);

    const formattedBirthday = format(birthdayDate, "d MMMM yyyy", {
      locale: fr,
    });

    if (player.picture !== null) {
      return (
        <div className="player-card">
          <img
            src={`https://localhost:7144/Images/Players/${player.picture}`}
            alt={`Photo de ${player.name}`}
            className="player-card-image"
          />
          <h3 className="player-card-name">
            {player.surname} {player.name}
          </h3>
          <p className="player-card-birthday">{formattedBirthday}</p>
        </div>
      );
    } else {
      return (
        <div className="player-card">
          <h3 className="player-card-name">
            {player.surname} {player.name}
          </h3>
          <p className="player-card-birthday">{formattedBirthday}</p>
        </div>
      );
    }
  } else {
    if (player.picture !== null) {
      return (
        <div className="player-card">
          <img
            src={`https://localhost:7144/Images/Players/${player.picture}`}
            alt={`Photo de ${player.name}`}
            className="player-card-image"
          />
          <h3 className="player-card-name">
            {player.surname} {player.name}
          </h3>
          <p className="player-card-birthday">Date de naissance inconnue</p>
        </div>
      );
    } else {
      return (
        <div className="player-card">
          <h3 className="player-card-name">
            {player.surname} {player.name}
          </h3>
          <p className="player-card-birthday">Date de naissance inconnue</p>
        </div>
      );
    }
  }
}

export default PlayerCard;
