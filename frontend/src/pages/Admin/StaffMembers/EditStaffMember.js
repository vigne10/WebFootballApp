import React, { useState, useEffect } from "react";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import { useNavigate, useParams } from "react-router-dom";
import "../../../assets/styles/Admin/StaffMembers/EditStaffMember.css";

function EditStaffMember() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const { staffMemberId } = useParams();
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

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }
    // Call to the backend to get staff member details
    fetch(`https://localhost:7144/api/staffMember/${staffMemberId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((data) => {
        setStaffMemberInfo({
          name: data.name,
          surname: data.surname,
          birthday: data.birthday ? data.birthday.substring(0, 10) : "",
          job: data.job.id,
          team: data.team.id,
        });
      })
      .catch((error) =>
        console.error(
          "Erreur lors de la récupération des détails du membre du staff:",
          error
        )
      );

    // Call to the backend to get the list of jobs
    fetch("https://localhost:7144/api/jobs", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((data) => setJobs(data))
      .catch((error) =>
        console.error("Erreur lors de la récupération des jobs:", error)
      );

    // Call to the backend to get the list of teams
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
  }, [staffMemberId]);

  const handleInputChange = (event) => {
    const { name, value } = event.target;

    setStaffMemberInfo((prevInfo) => ({ ...prevInfo, [name]: value }));
  };

  const handleImageUpload = (event) => {
    const selectedImage = event.target.files[0];
    setStaffMemberInfo((prevInfo) => ({ ...prevInfo, picture: selectedImage }));
  };

  const handleUpdateStaffMember = async () => {
    try {
      if (!token) {
        navigate("/login");
        return;
      }
      const response = await fetch(
        `https://localhost:7144/api/staffMember/${staffMemberId}`,
        {
          method: "PUT",
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            id: staffMemberId,
            name: staffMemberInfo.name,
            surname: staffMemberInfo.surname,
            birthday: staffMemberInfo.birthday,
          }),
        }
      );

      if (!response.ok) {
        console.error("Erreur lors de la modification du membre du staff");
        return;
      }

      // Call to backend to upload updated picture
      if (staffMemberInfo.picture) {
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
      }

      // Call to backend to update job
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

      // Call to backend to update team
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

      console.log("Membre du staff modifié avec succès !");
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
      <div className="edit-staff-content-container">
        <div className="edit-staff-form-container">
          <h2>Modifier le membre du staff</h2>
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
            <div className="edit-staff-buttons-container">
              <button type="button" onClick={handleUpdateStaffMember}>
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

export default EditStaffMember;
