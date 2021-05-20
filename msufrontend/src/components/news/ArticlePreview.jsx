import React from 'react'
import { Link } from 'react-router-dom';

function ArticlePreview({ id, title, previewImageUrl, publicationDate, author, editor }) {
    return (
        <Link to={`/articles/${id}`}>
            <div>
                <h3>{title}</h3>
                <div>
                    <span>{publicationDate}</span>
                    <span>{author}</span>
                    <span>{editor}</span>
                </div>
                <img src={previewImageUrl} alt="превью фото" />
            </div>
        </Link>
    )
}

export default ArticlePreview
