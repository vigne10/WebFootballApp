import React, { useState, useEffect } from "react";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import { useNavigate } from "react-router-dom";
import "../../../assets/styles/Admin/Articles/AdminArticles.css";
import { format, parseISO } from "date-fns";
import { fr } from "date-fns/locale";

function AdminArticles() {
  const [articles, setArticles] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const pageSize = 20;
  const navigate = useNavigate();
  const token = localStorage.getItem("token");

  const handleDeleteArticle = (articleId) => {
    if (!token) {
      navigate("/login");
      return;
    }
    const confirmDelete = window.confirm(
      "Êtes-vous sûr de vouloir supprimer cet article ?"
    );

    if (confirmDelete) {
      fetch(`https://localhost:7144/api/article/${articleId}`, {
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
            // Update list of articles
            setArticles(articles.filter((article) => article.id !== articleId));
          } else {
            throw new Error(
              `Erreur lors de la suppression de l'article avec l'ID ${articleId}`
            );
          }
        })
        .catch((error) =>
          console.error("Erreur lors de la suppression de l'article:", error)
        );
    }
  };

  const handleEditArticle = (articleId) => {
    navigate(`/admin/articles/edit/${articleId}`);
  };

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }

    // Call to backend to get articles with pagination
    fetch(
      `https://localhost:7144/api/articles?pageNumber=${currentPage}&pageSize=${pageSize}`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    )
      .then((response) => {
        if (response.status === 401) {
          localStorage.removeItem("token");
          navigate("/login");
          throw new Error("Token expiré ou invalide");
        }
        return response.json();
      })
      .then((data) => {
        setArticles(data.data);
        setTotalPages(data.totalPages);
      })
      .catch((error) =>
        console.error("Erreur lors de la récupération des articles:", error)
      );
  }, [currentPage]);

  return (
    <div>
      <AdminNavbar />
      <div className="admin-articles-content-container">
        <div className="admin-articles-top-container">
          <div id="admin-articles-title-container">
            <h2>Gestion des articles</h2>
          </div>
          <div id="admin-articles-add-button-container">
            <button
              id="admin-articles-add-button"
              onClick={() => navigate("/admin/articles/add")}
            >
              Ajouter un nouvel article
            </button>
          </div>
        </div>

        <ul className="articles-list">
          {articles.map((article) => (
            <li key={article.id} className="article-item">
              <div className="article-info">
                <div className="article-title">{article.title}</div>
                <div className="author-date">
                  {article.user.surname} {article.user.name} |{" "}
                  {format(parseISO(article.date), "dd MMMM yyyy 'à' HH'h'mm", {
                    locale: fr,
                  })}
                </div>
              </div>
              <div className="article-actions">
                <button onClick={() => handleEditArticle(article.id)}>
                  Modifier
                </button>
                <button onClick={() => handleDeleteArticle(article.id)}>
                  Supprimer
                </button>
              </div>
            </li>
          ))}
        </ul>
        <div className="articles-pagination-buttons">
          {currentPage > 1 && totalPages > 3 && (
            <button onClick={() => setCurrentPage(1)}>Première</button>
          )}
          {currentPage > 1 && (
            <button onClick={() => setCurrentPage(currentPage - 1)}>
              Précédente
            </button>
          )}
          {currentPage > 3 && (
            <button onClick={() => setCurrentPage(1)}>1</button>
          )}
          {currentPage > 4 && <span className="pagination-ellipsis">...</span>}
          {Array.from({ length: totalPages }, (_, index) => {
            if (
              index === 0 ||
              index === totalPages - 1 ||
              Math.abs(index - currentPage) <= 2
            ) {
              return (
                <button
                  key={index + 1}
                  onClick={() => setCurrentPage(index + 1)}
                  className={currentPage === index + 1 ? "active" : ""}
                >
                  {index + 1}
                </button>
              );
            }
            return null;
          })}
          {currentPage < totalPages - 3 && (
            <span className="pagination-ellipsis">...</span>
          )}
          {currentPage < totalPages - 2 && (
            <button onClick={() => setCurrentPage(totalPages)}>Dernière</button>
          )}
          {currentPage < totalPages && (
            <button onClick={() => setCurrentPage(currentPage + 1)}>
              Suivante
            </button>
          )}
        </div>
      </div>
    </div>
  );
}

export default AdminArticles;
