import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Navbar from "../../components/Guest/Navbar";
import "../../assets/styles/Guest/Article.css";

function Article() {
  const { articleId } = useParams(); // Get articleId from URL
  const [article, setArticle] = useState({});
  const navigate = useNavigate();

  useEffect(() => {
    // Get article from backend API
    fetch(`https://localhost:7144/api/article/${articleId}`)
      .then((response) => response.json())
      .then((data) => setArticle(data))
      .catch((error) =>
        console.error("Erreur lors de la récupération de l'article : ", error)
      );
  }, [articleId]);

  return (
    <div>
      <Navbar />
      <div className="article-page-container">
        {article.image && (
          <img
            src={`${article.image}`}
            alt={`Logo de ${article.title}`}
            className="article-page-image"
          />
        )}
        <h1 className="article-page-article-title">{article.title}</h1>
        <div className="article-page-article-content">{article.content}</div>
        <div className="article-page-back-button-container">
          <button
            className="article-page-back-button"
            onClick={() => navigate(-1)}
          >
            Retour
          </button>
        </div>
      </div>
    </div>
  );
}

export default Article;
