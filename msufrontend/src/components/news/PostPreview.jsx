import React from "react";
import { Link } from "react-router-dom";
import { Image } from "react-bootstrap";

function PostPreview({
  id,
  title,
  previewImageUrl,
  publicationDate,
  author,
  editor,
}) {
  return (
    <div className="post__preview">
      <Link to={`/post/${id}`} className="post-link">
        <h3 className="post__title">{title}</h3>
      </Link>
      <span className="post__date">
        {new Date(publicationDate).toLocaleString("ru")}
      </span>
      <Link to={`/post/${id}`}>
        <Image
          src={previewImageUrl}
          alt="превью фото"
          className="post__previewImage"
          fluid
        />
      </Link>
      <div>
        <span>{author}</span>
        <span>{editor}</span>
      </div>
    </div>
  );
}

export default PostPreview;
