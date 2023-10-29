import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import "../../../assets/styles/Admin/StaffMembers/AddStaffMember.css";

function AddStaffMember() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const [jobs, setJobs] = useState([]);
  const [teams, setTeams] = useState([]);
  const [staffMemberInfo, setStaffMemberInfo] = useState({
    name: "",
    surname: "",
    birthday: "",
    job: "",
    team: "",
    picture: null,
  });

  const isFormValid = () => {
    return (
      staffMemberInfo.name &&
      staffMemberInfo.surname &&
      staffMemberInfo.job &&
      staffMemberInfo.team
    );
  };

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }
    // Call to the backend to get the list of jobs
    fetch("https://localhost:7144/api/jobs", {
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
        setJobs(data);
        if (data.length > 0) {
          setStaffMemberInfo((prevInfo) => ({ ...prevInfo, job: data[0].id }));
        }
      })
      .catch((error) =>
        console.error("Erreur lors de la récupération des positions:", error)
      );

    // Call to the backend to get the list of teams
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
          setStaffMemberInfo((prevInfo) => ({
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
    setStaffMemberInfo((prevInfo) => ({ ...prevInfo, [name]: value }));
  };

  const handleImageUpload = (event) => {
    // Manage image upload
    const selectedImage = event.target.files[0];
    setStaffMemberInfo((prevInfo) => ({ ...prevInfo, picture: selectedImage }));
  };

  const handleAddStaffMember = async () => {
    try {
      if (!token) {
        navigate("/login");
        return;
      }
      // Call to backend to add staff member and get the added staff member's ID
      const response = await fetch("https://localhost:7144/api/staffMember", {
        method: "POST",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          name: staffMemberInfo.name,
          surname: staffMemberInfo.surname,
          birthday: staffMemberInfo.birthday,
        }),
      });

      if (!response.ok) {
        console.error("Erreur lors de l'ajout du membre du staff");
        return;
      }

      const addedStaffMember = await response.json();
      const staffMemberId = addedStaffMember.id;

      // Call to backend to upload picture
      const pictureFormData = new FormData();
      pictureFormData.append("picture", staffMemberInfo.picture);
      await fetch(
        `https://localhost:7144/api/staffMember/${staffMemberId}/picture`,
        {
          method: "POST",
          headers: {
            Authorization: `Bearer ${token}`,
          },
          body: pictureFormData,
        }
      );

      // Call to backend to set job
      if (staffMemberInfo.job) {
        await fetch(
          `https://localhost:7144/api/staffMember/${staffMemberId}/job?jobId=${staffMemberInfo.job}`,
          {
            method: "PATCH",
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
      }

      // Call to backend to set team
      if (staffMemberInfo.team) {
        await fetch(
          `https://localhost:7144/api/staffMember/${staffMemberId}/team?teamId=${staffMemberInfo.team}`,
          {
            method: "PATCH",
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
      }

      console.log("Membre du staff ajouté avec succès !");
      navigate("/admin/staff");
    } catch (error) {
      console.error("Une erreur s'est produite:", error);
    }
  };

  const handleCancel = () => {
    navigate("/admin/staff");
  };

  return (
    <div>
      <AdminNavbar />
      <div className="add-staff-content-container">
        <div className="add-staff-form-container">
          <h2>Ajouter un membre au staff</h2>
          <form>
            <input
              type="text"
              name="name"
              value={staffMemberInfo.name}
              onChange={handleInputChange}
              placeholder="Nom"
              required
            />
            <input
              type="text"
              name="surname"
              value={staffMemberInfo.surname}
              onChange={handleInputChange}
              placeholder="Prénom"
              required
            />
            <input
              type="date"
              name="birthday"
              value={staffMemberInfo.birthday}
              onChange={handleInputChange}
            />
            <select
              name="job"
              value={staffMemberInfo.job}
              onChange={handleInputChange}
            >
              {jobs.map((job) => (
                <option key={job.id} value={job.id}>
                  {job.name}
                </option>
              ))}
            </select>
            <select
              name="team"
              value={staffMemberInfo.team}
              onChange={handleInputChange}
            >
              {teams.map((team) => (
                <option key={team.id} value={team.id}>
                  {team.name}
                </option>
              ))}
            </select>
            <input type="file" accept="image/*" onChange={handleImageUpload} />
            <div className="add-staff-buttons-container">
              <button
                type="button"
                onClick={handleAddStaffMember}
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

export default AddStaffMember;
