import React, { useState, useEffect } from "react";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import { useNavigate, useParams } from "react-router-dom";
import "../../../assets/styles/Admin/Competitions/EditCompetition.css";

function EditCompetition() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const { competitionId } = useParams();
  const [competitionInfo, setCompetitionInfo] = useState({
    name: "",
    logo: null,
  });
  const [newLogoUrl, setNewLogoUrl] = useState("");

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }
    // Call to the backend to get competition details
    fetch(`https://localhost:7144/api/competition/${competitionId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((data) => {
        setCompetitionInfo({
          name: data.name,
          logo: data.logo,
        });
      })
      .catch((error) =>
        console.error(
          "Erreur lors de la récupération des détails de la compétition : ",
          error
        )
      );
  }, [competitionId]);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setCompetitionInfo((prevInfo) => ({ ...prevInfo, [name]: value }));
  };

  const handleImageUpload = (event) => {
    const selectedImage = event.target.files[0];
    setCompetitionInfo((prevInfo) => ({ ...prevInfo, logo: selectedImage }));
    setNewLogoUrl(URL.createObjectURL(selectedImage));
  };

  const handleUpdateCompetition = async () => {
    try {
      if (!token) {
        navigate("/login");
        return;
      }
      const response = await fetch(
        `https://localhost:7144/api/competition/${competitionId}`,
        {
          method: "PUT",
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            id: competitionId,
            name: competitionInfo.name,
          }),
        }
      );

      if (!response.ok) {
        console.error("Erreur lors de la modification de la compétition");
        return;
      }

      // Call to backend to upload updated logo
      if (competitionInfo.logo) {
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
      }

      console.log("Compétition modifiée avec succès !");
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
      <div className="edit-competition-content-container">
        <div className="edit-competition-form-container">
          <h2>Modifier la compétition</h2>
          <div className="edit-competition-logo-container">
            {newLogoUrl ? (
              <img
                className="edit-competition-logo"
                src={newLogoUrl}
                alt={`Logo de ${competitionInfo.name}`}
              />
            ) : competitionInfo.logo ? (
              <img
                className="edit-competition-logo"
                src={competitionInfo.logo}
                alt={`Logo de ${competitionInfo.name}`}
              />
            ) : null}
          </div>
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
            <div className="edit-competition-buttons-container">
              <button type="button" onClick={handleUpdateCompetition}>
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

export default EditCompetition;
