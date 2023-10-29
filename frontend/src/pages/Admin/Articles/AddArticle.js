import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import "../../../assets/styles/Admin/Articles/AddArticle.css";

function AddArticle() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const [articleInfo, setArticleInfo] = useState({
    title: "",
    content: "",
    image: "",
  });

  const isFormValid = () => {
    return articleInfo.title && articleInfo.content;
  };

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setArticleInfo((prevInfo) => ({ ...prevInfo, [name]: value }));
  };

  const handleImageUpload = (event) => {
    // Manage image upload
    const selectedImage = event.target.files[0];
    setArticleInfo((prevInfo) => ({ ...prevInfo, image: selectedImage }));
  };

  const handleAddArticle = async () => {
    try {
      if (!token) {
        navigate("/login");
        return;
      }
      // Call to backend to add article and get the added article's ID
      const response = await fetch("https://localhost:7144/api/article", {
        method: "POST",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          title: articleInfo.title,
          content: articleInfo.content,
        }),
      });

      if (!response.ok) {
        console.error("Erreur lors de l'ajout de l'article");
        return;
      }

      const addedArticle = await response.json();
      const articleId = addedArticle.id;

      // Call to backend to upload picture
      const pictureFormData = new FormData();
      pictureFormData.append("image", articleInfo.image);
      await fetch(`https://localhost:7144/api/article/${articleId}/image`, {
        method: "POST",
        headers: {
          Authorization: `Bearer ${token}`,
        },
        body: pictureFormData,
      });

      console.log("Article ajouté avec succès !");
      navigate("/admin/articles");
    } catch (error) {
      console.error("Une erreur s'est produite:", error);
    }
  };

  const handleCancel = () => {
    navigate("/admin/articles");
  };

  return (
    <div>
      <AdminNavbar />
      <div className="add-article-content-container">
        <div className="add-article-form-container">
          <h2>Ajouter un article</h2>
          <form>
            <input
              type="text"
              name="title"
              value={articleInfo.title}
              onChange={handleInputChange}
              placeholder="Titre"
              required
            />
            <textarea
              type="text"
              name="content"
              id="add-article-content-input"
              value={articleInfo.content}
              onChange={handleInputChange}
              placeholder="Contenu"
              required
            />
            <input type="file" accept="image/*" onChange={handleImageUpload} />
            <div className="add-article-buttons-container">
              <button
                type="button"
                onClick={handleAddArticle}
                disabled={!isFormValid()}
              >
                Ajouter
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

export default AddArticle;
