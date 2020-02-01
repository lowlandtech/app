import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Card } from './card.reducer';

const getArticle = createFeatureSelector<Card>('card');
export const getArticleData = createSelector(
  getArticle,
  (state: Card) => state.data,
);
export const getComments = createSelector(
  getArticle,
  (state: Card) => state.comments,
);
export const getArticleLoaded = createSelector(
  getArticle,
  (state: Card) => state.loaded,
);
export const getAuthorUsername = createSelector(
  getArticle,
  (state: Card) => state.data.author.username,
);

export const articleQuery = {
  getArticleData,
  getComments,
  getArticleLoaded,
  getAuthorUsername,
};
