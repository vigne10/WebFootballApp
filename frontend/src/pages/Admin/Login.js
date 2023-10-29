import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "../../assets/styles/Common/Admin/Login.css";

function Login() {
  const navigate = useNavigate();
  const [mail, setMail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      navigate("/admin");
    }
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch("https://localhost:7144/api/auth/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ mail, password }),
      });

      if (response.ok) {
        const data = await response.json();

        // Save token in local storage
        localStorage.setItem("token", data.token);

        // Redirect to admin page
        navigate("/admin");
      } else {
        setError("Identifiants incorrects");
      }
    } catch (error) {
      console.error("Une erreur s'est produite:", error);
    }
  };

  return (
    <div className="login-container">
      <div className="login-logo-container">
        <div className="login-title-container">
          <h2 className="login-title">Connexion</h2>
        </div>
        <img className="login-logo" src="icon.png" alt="Logo du club"></img>
      </div>
      <form onSubmit={handleSubmit}>
        <label>
          Email:
          <input
            type="email"
            value={mail}
            onChange={(e) => setMail(e.target.value)}
            required
          />
        </label>
        <label>
          Mot de passe:
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </label>
        <button type="submit">Se connecter</button>
      </form>
    </div>
  );
}

export default Login;
