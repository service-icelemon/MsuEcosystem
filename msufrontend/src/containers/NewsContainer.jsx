import React from "react";
import { useDispatch, useSelector } from "react-redux";
import PostPreview from "../components/news/PostPreview";
import { fetchPosts } from "../redux/actions/news";

export default function NewsContainer() {
  const dispatch = useDispatch();
  const news = useSelector(({ news }) => news.posts);

  React.useEffect(() => {
    dispatch(fetchPosts());
  }, [dispatch]);

  return (
    <div>
      <div className="news__container">
        {news.map((item) => (
          <PostPreview
            key={item.id}
            id={item.id}
            title={item.title}
            previewImageUrl={item.previewImageUrl}
            publicationDate={item.publicationDate}
            author={item.author}
            editor={item.editor}
          />
        ))}
      </div>
    </div>
  );
}
