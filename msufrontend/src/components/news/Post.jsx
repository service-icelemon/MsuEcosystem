import React from "react";
import { useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { fetchPost } from "../../redux/actions/news";
import ReactHtmlParser from "react-html-parser";

function Post() {
  const dispatch = useDispatch();
  const { previewImageUrl, title, text, publicationDate, author, editor } =
    useSelector(({ news }) => news.currentPost);
  const { id } = useParams();

  React.useEffect(() => {
    dispatch(fetchPost(id));
  }, []);

  return (
    <div className="post__container">
      <h2 className="post__title">{title}</h2>
      <span className="post__date">{new Date(publicationDate).toLocaleString("ru")}</span>
      <span>{author}</span>
      <span>{editor}</span>
      <img src={previewImageUrl} alt="" className="post__previewImage"/>
      <p>{ReactHtmlParser(text)}</p>
    </div>
  );
}

export default Post;
