import React, { useState, useEffect } from "react";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import { useNavigate, useParams } from "react-router-dom";
import "../../../assets/styles/Admin/Players/EditPlayer.css";

function EditPlayer() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const { playerId } = useParams();
  const [positions, setPositions] = useState([]);
  const [teams, setTeams] = useState([]);
  const [playerInfo, setPlayerInfo] = useState({
    name: "",
    surname: "",
    birthday: "",
    position: "",
    team: "",
    picture: null,
  });

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }
    // Call to the backend to retrieve player details
    fetch(`https://localhost:7144/api/player/${playerId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((data) => {
        setPlayerInfo({
          name: data.name,
          surname: data.surname,
          birthday: data.birthday ? data.birthday.substring(0, 10) : "",
          position: data.position.id,
          team: data.team.id,
        });
      })
      .catch((error) =>
        console.error(
          "Erreur lors de la récupération des détails du joueur:",
          error
        )
      );

    // Call to the backend to retrieve the list of positions
    fetch("https://localhost:7144/api/positions", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((data) => setPositions(data))
      .catch((error) =>
        console.error("Erreur lors de la récupération des positions:", error)
      );

    // Call to the backend to retrieve the list of teams
    fetch("https://localhost:7144/api/teams", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((data) => setTeams(data))
      .catch((error) =>
        console.error("Erreur lors de la récupération des équipes:", error)
      );
  }, [playerId]);

  const handleInputChange = (event) => {
    const { name, value } = event.target;

    setPlayerInfo((prevInfo) => ({ ...prevInfo, [name]: value }));
  };

  const handleImageUpload = (event) => {
    const selectedImage = event.target.files[0];
    setPlayerInfo((prevInfo) => ({ ...prevInfo, picture: selectedImage }));
  };

  const handleUpdatePlayer = async () => {
    try {
      if (!token) {
        navigate("/login");
        return;
      }
      const response = await fetch(
        `https://localhost:7144/api/player/${playerId}`,
        {
          method: "PUT",
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            id: playerId,
            name: playerInfo.name,
            surname: playerInfo.surname,
            birthday: playerInfo.birthday,
          }),
        }
      );

      if (!response.ok) {
        console.error("Erreur lors de la modification du joueur");
        return;
      }

      // Call to backend to upload updated picture
      if (playerInfo.picture) {
        const pictureFormData = new FormData();
        pictureFormData.append("picture", playerInfo.picture);
        await fetch(`https://localhost:7144/api/player/${playerId}/picture`, {
          method: "POST",
          headers: {
            Authorization: `Bearer ${token}`,
          },
          body: pictureFormData,
        });
      }

      // Call to backend to update position
      if (playerInfo.position) {
        await fetch(
          `https://localhost:7144/api/player/${playerId}/position?positionId=${playerInfo.position}`,
          {
            method: "PATCH",
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
      }

      // Call to backend to update team
      if (playerInfo.team) {
        await fetch(
          `https://localhost:7144/api/player/${playerId}/team?teamId=${playerInfo.team}`,
          {
            method: "PATCH",
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
      }

      console.log("Joueur modifié avec succès !");
      navigate("/admin/players");
    } catch (error) {
      console.error("Une erreur s'est produite:", error);
    }
  };

  const handleCancel = () => {
    navigate("/admin/players");
  };

  return (
    <div>
      <AdminNavbar />
      <div className="edit-player-content-container">
        <div className="edit-player-form-container">
          <h2>Modifier le joueur</h2>
          <form>
            <input
              type="text"
              name="name"
              value={playerInfo.name}
              onChange={handleInputChange}
              placeholder="Nom"
              required
            />
            <input
              type="text"
              name="surname"
              value={playerInfo.surname}
              onChange={handleInputChange}
              placeholder="Prénom"
              required
            />
            <input
              type="date"
              name="birthday"
              value={playerInfo.birthday}
              onChange={handleInputChange}
            />
            <select
              name="position"
              value={playerInfo.position}
              onChange={handleInputChange}
            >
              {positions.map((position) => (
                <option key={position.id} value={position.id}>
                  {position.name}
                </option>
              ))}
            </select>
            <select
              name="team"
              value={playerInfo.team}
              onChange={handleInputChange}
            >
              {teams.map((team) => (
                <option key={team.id} value={team.id}>
                  {team.name}
                </option>
              ))}
            </select>
            <input type="file" accept="image/*" onChange={handleImageUpload} />
            <div className="edit-player-buttons-container">
              <button type="button" onClick={handleUpdatePlayer}>
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

export default EditPlayer;
