import React, { useState, useEffect } from "react";
import Navbar from "../../components/Guest/Navbar";
import StaffMemberCard from "../../components/Guest/StaffMemberCard";
import "../../assets/styles/Guest/StaffMembers.css";

function StaffMembers() {
  const [staffMembers, setStaffMembers] = useState([]);

  useEffect(() => {
    fetch("https://localhost:7144/api/staffMembers?TeamId=1")
      .then((response) => response.json())
      .then((data) => setStaffMembers(data))
      .catch((error) =>
        console.error(
          "Erreur lors de la récupération des membres du staff : ",
          error
        )
      );
  }, []);

  const jobTitles = [
    { singular: "Médecin", plural: "Médecins" },
    { singular: "Entraîneur", plural: "Entraîneurs" },
    { singular: "Entraîneur adjoint", plural: "Entraîneurs adjoints" },
    { singular: "Kinésithérapeute", plural: "Kinésithérapeutes" },
    { singular: "Entraîneur des gardiens", plural: "Entraîneurs des gardiens" },
    { singular: "Nutritionniste", plural: "Nutritionnistes" },
    { singular: "Ostéopathe", plural: "Ostéopathes" },
    { singular: "Préparateur physique", plural: "Préparateurs physiques" },
    { singular: "Analyste vidéo", plural: "Analystes vidéo" },
    { singular: "Cuisinier", plural: "Cuisiniers" },
  ];

  const groupedStaffMembers = {};

  staffMembers.forEach((staffMember) => {
    const jobName = staffMember.job.name;
    if (!groupedStaffMembers[jobName]) {
      groupedStaffMembers[jobName] = [];
    }
    groupedStaffMembers[jobName].push(staffMember);
  });

  // Define the order of the jobs
  const order = [
    "Entraîneur",
    "Entraîneur adjoint",
    "Entraîneur des gardiens",
    "Préparateur physique",
    "Médecin",
    "Kinésithérapeute",
    "Nutritionniste",
    "Ostéopathe",
    "Cuisinier",
    "Analyste vidéo",
  ];

  return (
    <div>
      <Navbar />
      <div className="staff-members-page-container">
        {order.map((jobName) => {
          const jobMembers = groupedStaffMembers[jobName];
          if (jobMembers && jobMembers.length > 0) {
            const title =
              jobMembers.length === 1
                ? jobName
                : jobTitles.find((job) => job.singular === jobName)?.plural;
            return (
              <div key={jobName} className="staff-members-job-section">
                <h2 className="staff-members-job-title">{title}</h2>
                <div className="staff-members-job-cards">
                  {jobMembers.map((staffMember, index) => (
                    <StaffMemberCard key={index} staffMember={staffMember} />
                  ))}
                </div>
              </div>
            );
          }
          return null;
        })}
      </div>
    </div>
  );
}

export default StaffMembers;
