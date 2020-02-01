import { createAction, props } from '@ngrx/store';
import { ArticleData } from '@spotacard/api';

import { ArticleListConfig } from './card-list.reducer';

export const setListPage = createAction('[card-list] SET_LIST_PAGE', props<{ page: number }>());

export const setListConfig = createAction('[card-list] SET_LIST_CONFIG', props<{ config: ArticleListConfig }>());

export const loadArticles = createAction('[card-list] LOAD_ARTICLES');

export const loadArticlesSuccess = createAction(
  '[card-list] LOAD_ARTICLES_SUCCESS',
  props<{ articles: ArticleData[]; articlesCount: number }>(),
);

export const loadArticlesFail = createAction('[card-list] LOAD_ARTICLES_FAIL', props<{ error: Error }>());

export const favorite = createAction('[card-list] FAVORITE', props<{ slug: string }>());

export const favoriteSuccess = createAction('[card-list] FAVORITE_SUCCESS', props<{ card: ArticleData }>());

export const favoriteFail = createAction('[card-list] FAVORITE_FAIL', props<{ error: Error }>());

export const unFavorite = createAction('[card-list] UNFAVORITE', props<{ slug: string }>());

export const unFavoriteSuccess = createAction('[card-list] UNFAVORITE_SUCCESS', props<{ card: ArticleData }>());

export const unFavoriteFail = createAction('[card-list] UNFAVORITE_FAIL', props<{ error: Error }>());
