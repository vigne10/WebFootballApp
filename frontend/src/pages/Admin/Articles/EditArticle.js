import React, { useState, useEffect } from "react";
import AdminNavbar from "../../../components/Admin/AdminNavbar";
import { useNavigate, useParams } from "react-router-dom";
import "../../../assets/styles/Admin/Articles/EditArticle.css";

function EditArticle() {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const { articleId } = useParams();
  const [articleInfo, setArticleInfo] = useState({
    title: "",
    content: "",
    image: null,
  });

  useEffect(() => {
    if (!token) {
      navigate("/login");
      return;
    }
    // Call to the backend to get article details
    fetch(`https://localhost:7144/api/article/${articleId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((data) => {
        setArticleInfo({
          title: data.title,
          content: data.content,
        });
      })
      .catch((error) =>
        console.error(
          "Erreur lors de la récupération des détails de l'article:",
          error
        )
      );
  }, [articleId]);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setArticleInfo((prevInfo) => ({ ...prevInfo, [name]: value }));
  };

  const handleImageUpload = (event) => {
    const selectedImage = event.target.files[0];
    setArticleInfo((prevInfo) => ({ ...prevInfo, image: selectedImage }));
  };

  const handleUpdateArticle = async () => {
    try {
      if (!token) {
        navigate("/login");
        return;
      }
      const response = await fetch(
        `https://localhost:7144/api/article/${articleId}`,
        {
          method: "PUT",
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            id: articleId,
            title: articleInfo.title,
            content: articleInfo.content,
          }),
        }
      );

      if (!response.ok) {
        console.error("Erreur lors de la modification de l'article");
        return;
      }

      // Call to backend to upload updated image
      if (articleInfo.image) {
        const pictureFormData = new FormData();
        pictureFormData.append("image", articleInfo.image);
        await fetch(`https://localhost:7144/api/article/${articleId}/image`, {
          method: "POST",
          headers: {
            Authorization: `Bearer ${token}`,
          },
          body: pictureFormData,
        });
      }

      console.log("Article modifié avec succès !");
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
      <div className="edit-article-content-container">
        <div className="edit-article-form-container">
          <h2>Modifier l'article</h2>
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
              id="edit-article-content-input"
              value={articleInfo.content}
              onChange={handleInputChange}
              placeholder="Contenu"
              required
            />
            <input type="file" accept="image/*" onChange={handleImageUpload} />
            <div className="edit-article-buttons-container">
              <button type="button" onClick={handleUpdateArticle}>
                Modifier
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

export default EditArticle;
