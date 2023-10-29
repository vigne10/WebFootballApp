import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import "../../../assets/styles/Admin/Players/AddPlayer.css";

function AddPlayer() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
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

  const isFormValid = () => {
    return (
      playerInfo.name &&
      playerInfo.surname &&
      playerInfo.position &&
      playerInfo.team
    );
  };

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }
    // Call to the backend to retrieve the list of positions
    fetch("https://localhost:7144/api/positions", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => {
        if (response.status === 401) {
          // Token expiré ou invalide, déconnecter l'utilisateur
          localStorage.removeItem("token");
          navigate("/login"); // Rediriger vers la page de connexion
          throw new Error("Token expiré ou invalide");
        }
        return response.json();
      })
      .then((data) => {
        setPositions(data);
        if (data.length > 0) {
          setPlayerInfo((prevInfo) => ({ ...prevInfo, position: data[0].id }));
        }
      })
      .catch((error) =>
        console.error("Erreur lors de la récupération des positions:", error)
      );

    // Call to the backend to retrieve the list of teams
    fetch("https://localhost:7144/api/teams", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => {
        if (response.status === 401) {
          localStorage.removeItem("token");
          navigate("/login");
          throw new Error("Token expiré ou invalide");
        }
        return response.json();
      })
      .then((data) => {
        setTeams(data);
        if (data.length > 0) {
          setPlayerInfo((prevInfo) => ({
            ...prevInfo,
            team: data[0].id,
          }));
        }
      })
      .catch((error) =>
        console.error("Erreur lors de la récupération des équipes:", error)
      );
  }, []);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setPlayerInfo((prevInfo) => ({ ...prevInfo, [name]: value }));
  };

  const handleImageUpload = (event) => {
    // Manage image upload
    const selectedImage = event.target.files[0];
    setPlayerInfo((prevInfo) => ({ ...prevInfo, picture: selectedImage }));
  };

  const handleAddPlayer = async () => {
    try {
      if (!token) {
        navigate("/login");
        return;
      }
      // Call to backend to add player and get the added player's ID
      const response = await fetch("https://localhost:7144/api/player", {
        method: "POST",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          name: playerInfo.name,
          surname: playerInfo.surname,
          birthday: playerInfo.birthday,
        }),
      });

      if (!response.ok) {
        console.error("Erreur lors de l'ajout du joueur");
        return;
      }

      const addedPlayer = await response.json();
      const playerId = addedPlayer.id;

      // Call to backend to upload picture
      const pictureFormData = new FormData();
      pictureFormData.append("picture", playerInfo.picture);
      await fetch(`https://localhost:7144/api/player/${playerId}/picture`, {
        method: "POST",
        headers: {
          Authorization: `Bearer ${token}`,
        },
        body: pictureFormData,
      });

      // Call to backend to set position
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

      // Call to backend to set team
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

      console.log("Joueur ajouté avec succès !");
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
      <div className="add-player-content-container">
        <div className="add-player-form-container">
          <h2>Ajouter un joueur</h2>
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
            <div className="add-player-buttons-container">
              <button
                type="button"
                onClick={handleAddPlayer}
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

export default AddPlayer;
