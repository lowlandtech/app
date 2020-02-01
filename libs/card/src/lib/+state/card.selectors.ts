import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Card } from './card.reducer';

const getCard = createFeatureSelector<Card>('card');
export const getCardData = createSelector(
  getCard,
  (state: Card) => state.data,
);
export const getComments = createSelector(
  getCard,
  (state: Card) => state.comments,
);
export const getCardLoaded = createSelector(
  getCard,
  (state: Card) => state.loaded,
);
export const getAuthorUsername = createSelector(
  getCard,
  (state: Card) => state.data.author.username,
);

export const cardQuery = {
  getCardData,
  getComments,
  getCardLoaded,
  getAuthorUsername,
};
