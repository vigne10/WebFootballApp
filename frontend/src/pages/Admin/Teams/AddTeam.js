import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import "../../../assets/styles/Admin/Teams/AddTeam.css";

function AddTeam() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const [teamInfo, setTeamInfo] = useState({
    name: "",
    logo: null,
  });

  const isFormValid = () => {
    return teamInfo.name;
  };

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setTeamInfo((prevInfo) => ({ ...prevInfo, [name]: value }));
  };

  const handleImageUpload = (event) => {
    // Manage image upload
    const selectedImage = event.target.files[0];
    setTeamInfo((prevInfo) => ({ ...prevInfo, logo: selectedImage }));
  };

  const handleAddTeam = async () => {
    try {
      if (!token) {
        navigate("/login");
        return;
      }
      // Call to backend to add team and get the added team's ID
      const response = await fetch("https://localhost:7144/api/team", {
        method: "POST",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          name: teamInfo.name,
        }),
      });

      if (!response.ok) {
        console.error("Erreur lors de l'ajout de l'équipe");
        return;
      }

      const addedTeam = await response.json();
      const teamId = addedTeam.id;

      // Call to backend to upload picture
      const pictureFormData = new FormData();
      pictureFormData.append("logo", teamInfo.logo);
      await fetch(`https://localhost:7144/api/team/${teamId}/logo`, {
        method: "POST",
        headers: {
          Authorization: `Bearer ${token}`,
        },
        body: pictureFormData,
      });

      console.log("Equipe ajoutée avec succès !");
      navigate("/admin/teams");
    } catch (error) {
      console.error("Une erreur s'est produite:", error);
    }
  };

  const handleCancel = () => {
    navigate("/admin/teams");
  };

  return (
    <div>
      <AdminNavbar />
      <div className="add-team-content-container">
        <div className="add-team-form-container">
          <h2>Ajouter une équipe</h2>
          <form>
            <input
              type="text"
              name="name"
              value={teamInfo.name}
              onChange={handleInputChange}
              placeholder="Nom"
              required
            />
            <input type="file" accept="image/*" onChange={handleImageUpload} />
            <div className="add-team-buttons-container">
              <button
                type="button"
                onClick={handleAddTeam}
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

export default AddTeam;
