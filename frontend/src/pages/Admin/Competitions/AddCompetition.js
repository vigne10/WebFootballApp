import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import "../../../assets/styles/Admin/Competitions/AddCompetition.css";

function AddCompetition() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const [competitionInfo, setCompetitionInfo] = useState({
    name: "",
    logo: null,
  });

  const isFormValid = () => {
    return competitionInfo.name;
  };

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setCompetitionInfo((prevInfo) => ({ ...prevInfo, [name]: value }));
  };

  const handleImageUpload = (event) => {
    // Manage image upload
    const selectedImage = event.target.files[0];
    setCompetitionInfo((prevInfo) => ({ ...prevInfo, logo: selectedImage }));
  };

  const handleAddCompetition = async () => {
    try {
      if (!token) {
        navigate("/login");
        return;
      }
      // Call to backend to add competition and get the added competition's ID
      const response = await fetch("https://localhost:7144/api/competition", {
        method: "POST",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          name: competitionInfo.name,
        }),
      });

      if (!response.ok) {
        console.error("Erreur lors de l'ajout de la compétition");
        return;
      }

      const addedCompetition = await response.json();
      const competitionId = addedCompetition.id;

      // Call to backend to upload picture
      const pictureFormData = new FormData();
      pictureFormData.append("logo", competitionInfo.logo);
      await fetch(
        `https://localhost:7144/api/competition/${competitionId}/logo`,
        {
          method: "POST",
          headers: {
            Authorization: `Bearer ${token}`,
          },
          body: pictureFormData,
        }
      );

      console.log("Compétition ajoutée avec succès !");
      navigate("/admin/competitions");
    } catch (error) {
      console.error("Une erreur s'est produite : ", error);
    }
  };

  const handleCancel = () => {
    navigate("/admin/competitions");
  };

  return (
    <div>
      <AdminNavbar />
      <div className="add-competition-content-container">
        <div className="add-competition-form-container">
          <h2>Ajouter une compétition</h2>
          <form>
            <input
              type="text"
              name="name"
              value={competitionInfo.name}
              onChange={handleInputChange}
              placeholder="Nom"
              required
            />

            <input type="file" accept="image/*" onChange={handleImageUpload} />
            <div className="add-competition-buttons-container">
              <button
                type="button"
                onClick={handleAddCompetition}
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

export default AddCompetition;
