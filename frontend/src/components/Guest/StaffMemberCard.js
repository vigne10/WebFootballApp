import React from "react";
import { format, parseISO } from "date-fns";
import { fr } from "date-fns/locale";
import "../../assets/styles/Common/Guest/StaffMemberCard.css";

function StaffMemberCard({ staffMember }) {
  if (staffMember.birthday) {
    const birthdayDate = parseISO(staffMember.birthday);

    const formattedBirthday = format(birthdayDate, "d MMMM yyyy", {
      locale: fr,
    });

    if (staffMember.picture !== null) {
      return (
        <div className="staff-member-card">
          <img
            src={`https://localhost:7144/Images/StaffMembers/${staffMember.picture}`}
            alt={`Photo de ${staffMember.name}`}
            className="staff-member-card-image"
          />
          <h3 className="staff-member-card-name">
            {staffMember.surname} {staffMember.name}
          </h3>
          <p className="staff-member-card-birthday">{formattedBirthday}</p>
        </div>
      );
    } else {
      return (
        <div className="staff-member-card">
          <h3 className="staff-member-card-name">
            {staffMember.surname} {staffMember.name}
          </h3>
          <p className="staff-member-card-birthday">{formattedBirthday}</p>
        </div>
      );
    }
  } else {
    if (staffMember.picture !== null) {
      return (
        <div className="staff-member-card">
          <img
            src={`https://localhost:7144/Images/StaffMembers/${staffMember.picture}`}
            alt={`Photo de ${staffMember.name}`}
            className="staff-member-card-image"
          />
          <h3 className="staff-member-card-name">
            {staffMember.surname} {staffMember.name}
          </h3>
          <p className="staff-member-card-birthday">
            Date de naissance inconnue
          </p>
        </div>
      );
    } else {
      return (
        <div className="staff-member-card">
          <h3 className="staff-member-card-image">
            {staffMember.surname} {staffMember.name}
          </h3>
          <p className="staff-member-card-birthday">
            Date de naissance inconnue
          </p>
        </div>
      );
    }
  }
}

export default StaffMemberCard;
