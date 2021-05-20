import React from 'react'
import { useDispatch, useSelector } from 'react-redux';
import ArticlePreview from '../components/news/ArticlePreview';
import { fetchNews } from '../redux/actions/news';

export default function NewsContainer() {
    const dispatch = useDispatch();
    const news = useSelector(({ news }) => news.items);

    React.useEffect(() => {
        dispatch(fetchNews());
    }, [news, dispatch])

    return (
        <div>
            <h3>Новости</h3>
            <div>
                {news.map((item) => 
                <ArticlePreview props={item}/>)}
            </div>
        </div>
    )
}
