import React from "react";
import "../../assets/styles/Common/Guest/ArticleCard.css";

function ArticleCard({ article }) {
  return (
    <div className="home-article-card">
      {article.image && (
        <img
          src={`https://localhost:7144/Images/Articles/${article.image}`}
          alt={`Logo de ${article.title}`}
          className="home-article-image"
        />
      )}
      <h3 className="home-article-title">{article.title}</h3>
      <p className="home-article-content">
        {article.content.substring(0, 100)}[...]
      </p>
    </div>
  );
}

export default ArticleCard;
