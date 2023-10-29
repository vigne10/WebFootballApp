import React from "react";
import { Link, useLocation } from "react-router-dom";
import "../../assets/styles/Common/Guest/Navbar.css";

function Navbar() {
  const location = useLocation();

  return (
    <div className="navbar-container">
      <div className="navbar-logo-container">
        <a href="/">
          <img className="navbar-logo" src="icon.png" alt="Logo du club"></img>
        </a>
      </div>
      <div className="navbar-links-container">
        <ul className="navbar-links">
          <li>
            <Link
              to="/"
              className={`navbar-link ${
                window.location.pathname === "/" ||
                /^\/home\/page\/\d+$/.test(window.location.pathname)
                  ? "active-link"
                  : ""
              }`}
              onClick={(e) => {
                if (/^\/home\/page\/\d+$/.test(window.location.pathname)) {
                  e.preventDefault();
                  window.location.href = "/";
                }
              }}
            >
              ACCUEIL
            </Link>
          </li>
          <li>
            <Link
              to="/players"
              className={`navbar-link ${
                location.pathname === "/players" ? "active-link" : ""
              }`}
            >
              JOUEURS
            </Link>
          </li>
          <li>
            <Link
              to="/staff"
              className={`navbar-link ${
                location.pathname === "/staff" ? "active-link" : ""
              }`}
            >
              STAFF
            </Link>
          </li>
          <li>
            <Link
              to="/matches"
              className={`navbar-link ${
                location.pathname === "/matches" ? "active-link" : ""
              }`}
            >
              MATCHS
            </Link>
          </li>
          <li>
            <Link
              to="/table"
              className={`navbar-link ${
                location.pathname === "/table" ? "active-link" : ""
              }`}
            >
              CLASSEMENT
            </Link>
          </li>
          <li>
            <Link
              to="/history"
              className={`navbar-link ${
                location.pathname === "/history" ? "active-link" : ""
              }`}
            >
              HISTOIRE
            </Link>
          </li>
          <li>
            <Link
              to="/trophies"
              className={`navbar-link ${
                location.pathname === "/trophies" ? "active-link" : ""
              }`}
            >
              PALMARÈS
            </Link>
          </li>
        </ul>
      </div>
      <div className="social-icons-container">
        <a
          href="https://www.facebook.com/raecmons44.be"
          target="_blank"
          rel="noopener noreferrer"
        >
          <img
            src="/images/social-media/facebook-64.png"
            alt="Facebook"
            className="social-icon"
          />
        </a>
        <a
          href="https://www.instagram.com/raec_mons/"
          target="_blank"
          rel="noopener noreferrer"
        >
          <img
            src="/images/social-media/instagram-64.png"
            alt="Instagram"
            className="social-icon"
          />
        </a>
        <a
          href="https://twitter.com/RAEC_Mons"
          target="_blank"
          rel="noopener noreferrer"
        >
          <img
            src="/images/social-media/twitter-64.png"
            alt="Twitter"
            className="social-icon"
          />
        </a>
      </div>
    </div>
  );
}

export default Navbar;
