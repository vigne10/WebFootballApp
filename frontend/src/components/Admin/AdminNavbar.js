import React from "react";
import { Link, useLocation } from "react-router-dom";
import "../../assets/styles/Common/Admin/AdminNavbar.css";

function AdminNavbar() {
  const location = useLocation();

  return (
    <div className="admin-navbar-container">
      <div className="admin-navbar-links-container">
        <ul className="admin-navbar-links">
          <li className="admin-navbar-link-spacing">
            <Link
              to="/admin/articles"
              className={`admin-navbar-link ${
                location.pathname === "/admin/articles" ? "active-link" : ""
              }`}
            >
              Gestion des articles
            </Link>
          </li>
          <li className="admin-navbar-link-spacing">
            <Link
              to="/admin/players"
              className={`admin-navbar-link ${
                location.pathname === "/admin/players" ? "active-link" : ""
              }`}
            >
              Gestion des joueurs
            </Link>
          </li>
          <li className="admin-navbar-link-spacing">
            <Link
              to="/admin/staff"
              className={`admin-navbar-link ${
                location.pathname === "/admin/staff" ? "active-link" : ""
              }`}
            >
              Gestion du staff
            </Link>
          </li>
          <li className="admin-navbar-link-spacing">
            <Link
              to="/admin/teams"
              className={`admin-navbar-link ${
                location.pathname === "/admin/teams" ? "active-link" : ""
              }`}
            >
              Gestion des équipes
            </Link>
          </li>
          <li className="admin-navbar-link-spacing">
            <Link
              to="/admin/competitions"
              className={`admin-navbar-link ${
                location.pathname === "/admin/competitions" ? "active-link" : ""
              }`}
            >
              Gestion des compétitions
            </Link>
          </li>
          <li className="admin-navbar-link-spacing">
            <Link
              to="/admin/history"
              className={`admin-navbar-link ${
                location.pathname === "/admin/history" ? "active-link" : ""
              }`}
            >
              Gestion de l'histoire
            </Link>
          </li>
          <li className="admin-navbar-link-spacing">
            <Link
              to="/admin/trophies"
              className={`admin-navbar-link ${
                location.pathname === "/admin/trophies" ? "active-link" : ""
              }`}
            >
              Gestion du palmarès
            </Link>
          </li>
        </ul>
      </div>
      <div className="admin-navbar-logo-container">
        <a href="/" target="_blank">
          <img
            className="admin-navbar-logo"
            src="/icon.png"
            alt="Logo du club"
          ></img>
        </a>
      </div>
    </div>
  );
}

export default AdminNavbar;
