import React, { useState, useEffect } from "react";
import Navbar from "../../components/Guest/Navbar";
import "../../assets/styles/Guest/Matches.css";
import { format } from "date-fns";
import { fr } from "date-fns/locale";

function Matches() {
  const [matches, setMatches] = useState([]);

  useEffect(() => {
    fetch("https://localhost:7144/api/matches/team?TeamId=1&SeasonId=2")
      .then((response) => response.json())
      .then((data) => setMatches(data))
      .catch((error) =>
        console.error("Erreur lors de la récupération des matchs : ", error)
      );
  }, []);

  return (
    <div>
      <Navbar />
      <div className="matches-container">
        {matches.map((match) => (
          <div key={match.id} className="match-card">
            <div className="match-header">
              {match.competitionSeason.competition.name}
            </div>
            <div className="match-details-card">
              <div className="match-details">
                <div className="match-teams">
                  <div className="match-home-team">
                    <img
                      src={`https://localhost:7144/Images/Teams/${match.homeTeam.logo}`}
                      alt={match.homeTeam.name}
                      className="match-home-team-logo"
                    />
                    <div className="match-home-team-name">
                      {match.homeTeam.name}
                    </div>
                  </div>
                  <div className="match-score">
                    <div className="match-home-score">
                      {match.homeTeamScore !== null
                        ? `${match.homeTeamScore}`
                        : ""}
                    </div>
                    <div className="match-score-separator">-</div>
                    <div className="match-away-score">
                      {match.awayTeamScore !== null
                        ? `${match.awayTeamScore}`
                        : ""}
                    </div>
                  </div>
                  <div className="match-away-team">
                    <img
                      src={`https://localhost:7144/Images/Teams/${match.awayTeam.logo}`}
                      alt={match.awayTeam.name}
                      className="match-away-team-logo"
                    />
                    <div className="match-away-team-name">
                      {match.awayTeam.name}
                    </div>
                  </div>
                </div>
                <div className="match-infos">
                  {match.schedule
                    ? format(
                        new Date(match.schedule),
                        "d MMMM yyyy 'à' HH:mm",
                        {
                          locale: fr,
                        }
                      )
                    : "Date du match inconnue"}
                </div>
              </div>{" "}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Matches;
