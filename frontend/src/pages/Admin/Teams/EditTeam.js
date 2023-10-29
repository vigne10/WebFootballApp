import React, { useState, useEffect } from "react";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import { useNavigate, useParams } from "react-router-dom";
import "../../../assets/styles/Admin/Teams/EditTeam.css";

function EditTeam() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const { teamId } = useParams();
  const [teamInfo, setTeamInfo] = useState({
    name: "",
    logo: null,
  });
  const [newLogoUrl, setNewLogoUrl] = useState("");

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }
    // Call to the backend to get team details
    fetch(`https://localhost:7144/api/team/${teamId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((data) => {
        setTeamInfo({
          name: data.name,
          logo: data.logo,
        });
      })
      .catch((error) =>
        console.error(
          "Erreur lors de la récupération des détails de l'équipe : ",
          error
        )
      );
  }, [teamId]);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setTeamInfo((prevInfo) => ({ ...prevInfo, [name]: value }));
  };

  const handleImageUpload = (event) => {
    const selectedImage = event.target.files[0];
    setTeamInfo((prevInfo) => ({ ...prevInfo, logo: selectedImage }));
    setNewLogoUrl(URL.createObjectURL(selectedImage));
  };

  const handleUpdateTeam = async () => {
    try {
      if (!token) {
        navigate("/login");
        return;
      }
      const response = await fetch(
        `https://localhost:7144/api/team/${teamId}`,
        {
          method: "PUT",
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            id: teamId,
            name: teamInfo.name,
          }),
        }
      );

      if (!response.ok) {
        console.error("Erreur lors de la modification de l'équipe");
        return;
      }

      // Call to backend to upload updated logo
      if (teamInfo.logo) {
        const pictureFormData = new FormData();
        pictureFormData.append("logo", teamInfo.logo);
        await fetch(`https://localhost:7144/api/team/${teamId}/logo`, {
          method: "POST",
          headers: {
            Authorization: `Bearer ${token}`,
          },
          body: pictureFormData,
        });
      }

      console.log("Equipe modifiée avec succès !");
      navigate("/admin/teams");
    } catch (error) {
      console.error("Une erreur s'est produite : ", error);
    }
  };

  const handleCancel = () => {
    navigate("/admin/teams");
  };

  return (
    <div>
      <AdminNavbar />
      <div className="edit-team-content-container">
        <div className="edit-team-form-container">
          <h2>Modifier l'équipe</h2>
          <div className="edit-team-logo-container">
            {newLogoUrl ? (
              <img
                className="edit-team-logo"
                src={newLogoUrl}
                alt={`Logo de ${teamInfo.name}`}
              />
            ) : teamInfo.logo ? (
              <img
                className="edit-team-logo"
                src={teamInfo.logo}
                alt={`Logo de ${teamInfo.name}`}
              />
            ) : null}
          </div>
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
            <div className="edit-team-buttons-container">
              <button type="button" onClick={handleUpdateTeam}>
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

export default EditTeam;
