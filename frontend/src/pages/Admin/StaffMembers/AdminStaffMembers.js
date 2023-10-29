import AdminNavbar from "../../../components/Admin/AdminNavbar";
import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "../../../assets/styles/Admin/StaffMembers/AdminStaffMembers.css";

function AdminStaffMembers() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const [staffMembers, setStaffMembers] = useState([]);

  const handleDeleteStaffMember = (staffMemberId) => {
    if (!token) {
      navigate("/login");
      return;
    }

    const confirmDelete = window.confirm(
      "Êtes-vous sûr de vouloir supprimer ce membre ?"
    );

    if (confirmDelete) {
      fetch(`https://localhost:7144/api/staffMember/${staffMemberId}`, {
        method: "DELETE",
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
          if (response.status === 204) {
            // Update list of staff members
            setStaffMembers(
              staffMembers.filter(
                (staffMember) => staffMember.id !== staffMemberId
              )
            );
          } else {
            throw new Error(
              `Erreur lors de la suppression du membre du staff avec l'ID ${staffMemberId}`
            );
          }
        })
        .catch((error) =>
          console.error(
            "Erreur lors de la suppression du membre du staff:",
            error
          )
        );
    }
  };

  const handleEditStaffMember = (staffMemberId) => {
    navigate(`/admin/staff/edit/${staffMemberId}`);
  };

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }

    const teamId = 1;

    fetch(`https://localhost:7144/api/staffMembers?TeamId=${teamId}`, {
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
      .then((data) => setStaffMembers(data))
      .catch((error) =>
        console.error(
          "Erreur lors de la récupération des membres du staff: ",
          error
        )
      );
  }, []);

  return (
    <div>
      <AdminNavbar />
      <div className="admin-staff-content-container">
        <div className="admin-staff-top-container">
          <div id="admin-staff-title-container">
            <h2>Gestion du staff</h2>
          </div>
          <div id="admin-staff-add-button-container">
            <button
              id="admin-staff-add-button"
              onClick={() => navigate("/admin/staff/add")}
            >
              Ajouter un nouveau membre
            </button>
          </div>
        </div>

        <ul className="staff-list">
          {staffMembers.map((staffMember) => (
            <li key={staffMember.id} className="staff-item">
              <div className="staff-info">
                <div className="staff-name">
                  {staffMember.surname} {staffMember.name}
                </div>
                <div className="staff-job">{staffMember.job.name}</div>
              </div>
              <div className="staff-actions">
                <button onClick={() => handleEditStaffMember(staffMember.id)}>
                  Modifier
                </button>
                <button onClick={() => handleDeleteStaffMember(staffMember.id)}>
                  Supprimer
                </button>
              </div>
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
}

export default AdminStaffMembers;
