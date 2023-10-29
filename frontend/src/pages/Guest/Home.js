import React, { useState, useEffect } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import Navbar from "../../components/Guest/Navbar";
import ArticleCard from "../../components/Guest/ArticleCard";
import "../../assets/styles/Guest/Home.css";

function Home() {
  const { page } = useParams(); // Get the page number from the URL
  const [articles, setArticles] = useState([]);
  const [currentPage, setCurrentPage] = useState(page ? parseInt(page) : 1); // Use the page number from the URL or 1 if not provided
  const pageSize = 6;

  const navigate = useNavigate(); // Used to navigate to another page

  useEffect(() => {
    // Get articles from the backend endpoint with pagination
    fetch(
      `https://localhost:7144/api/articles?PageNumber=${currentPage}&PageSize=${pageSize}`
    )
      .then((response) => response.json())
      .then((data) => setArticles(data.data))
      .catch((error) =>
        console.error("Erreur lors de la récupération des articles : ", error)
      );

    // Update the URL with the current page number
    if (currentPage === 1) {
      navigate("/", { replace: true });
    } else {
      navigate(`/home/page/${currentPage}`, { replace: true });
    }
  }, [currentPage, navigate]);

  return (
    <div>
      <Navbar />
      <div className="home-articles-container">
        <div className="home-article-card-list">
          {articles.map((article) => (
            <Link
              key={article.id}
              to={`/article/${article.id}`}
              className="home-article-link"
            >
              <ArticleCard key={article.id} article={article} />
            </Link>
          ))}
          {currentPage > 1 && (
            <div className="home-pagination">
              <button
                className="home-pagination-button"
                onClick={() => setCurrentPage(currentPage - 1)}
              >
                {"< Articles plus récents"}
              </button>
              <span>Page {currentPage}</span>
            </div>
          )}
          {articles.length >= pageSize && (
            <div className="home-pagination">
              <span>Page {currentPage}</span>
              <button
                className="home-pagination-button"
                onClick={() => setCurrentPage(currentPage + 1)}
                disabled={articles.length < pageSize}
              >
                {"Articles plus anciens >"}
              </button>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default Home;
