import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import "../../../assets/styles/Admin/History/AdminHistory.css";

function AdminHistory() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const [historyInfo, setHistoryInfo] = useState({
    id: 1,
    content: "",
  });

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }
    // Call to the backend to get history details
    fetch(`https://localhost:7144/api/history/2`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((data) => {
        setHistoryInfo({
          id: data.id,
          content: data.content,
        });
      })
      .catch((error) =>
        console.error(
          "Erreur lors de la récupération des détails de l'histoire : ",
          error
        )
      );
  }, []);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setHistoryInfo((prevInfo) => ({ ...prevInfo, [name]: value }));
  };

  const handleEdit = async () => {
    if (!token) {
      navigate("/login");
      return;
    }
    try {
      // Call to backend to edit history
      const response = await fetch(
        `https://localhost:7144/api/history/${historyInfo.id}`,
        {
          method: "PUT",
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            id: historyInfo.id,
            content: historyInfo.content,
          }),
        }
      );

      console.log("Histoire modifiée avec succès !");
      navigate("/admin");

      if (!response.ok) {
        console.error("Erreur lors de la modification de l'histoire");
        return;
      }
    } catch (error) {
      console.error("Une erreur s'est produite : ", error);
    }
  };

  const handleDelete = async () => {
    if (!token) {
      navigate("/login");
      return;
    }
    const confirmDelete = window.confirm(
      "Êtes-vous sûr de vouloir supprimer ce match ?"
    );

    if (confirmDelete) {
      try {
        // Call to backend to delete history
        const response = await fetch(
          `https://localhost:7144/api/history/${historyInfo.id}`,
          {
            method: "PUT",
            headers: {
              Authorization: `Bearer ${token}`,
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              id: historyInfo.id,
              content: null,
            }),
          }
        );

        console.log("Histoire supprimée avec succès !");
        console.log(response);
        navigate("/admin");

        if (!response.ok) {
          console.error("Erreur lors de la suppression de l'histoire");
          return;
        }
      } catch (error) {
        console.error("Une erreur s'est produite : ", error);
      }
    }
  };

  const handleCancel = () => {
    navigate("/admin");
  };

  return (
    <div>
      <AdminNavbar />
      <div className="admin-history-content-container">
        <div className="admin-history-top-container">
          <div id="admin-history-title-container">
            <h2>Gestion de l'histoire du club</h2>
          </div>
          <div className="admin-history-buttons-container">
            <div className="admin-history-edit-button-container">
              <button id="admin-history-edit-button" onClick={handleEdit}>
                Modifier
              </button>
            </div>
            <div className="admin-history-delete-button-container">
              <button id="admin-history-delete-button" onClick={handleDelete}>
                Supprimer
              </button>
            </div>
            <div className="admin-history-cancel-button-container">
              <button id="admin-history-cancel-button" onClick={handleCancel}>
                Annuler
              </button>
            </div>
          </div>
        </div>
        <div className="admin-history-form-container">
          <form>
            <textarea
              type="text"
              name="content"
              id="admin-history-content-input"
              value={historyInfo.content}
              onChange={handleInputChange}
              placeholder="Contenu"
              required
            />
          </form>
        </div>
      </div>
    </div>
  );
}

export default AdminHistory;
