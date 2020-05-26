import { createFeatureSelector, createSelector } from '@ngrx/store';
import { CardList, CARDLIST_STATE_FEATURE_KEY } from './card-list.reducer';

const getCardList = createFeatureSelector<CardList>(CARDLIST_STATE_FEATURE_KEY);
export const getListConfig = createSelector(
  getCardList,
  (state: CardList) => state.listConfig,
);
export const getCards = createSelector(
  getCardList,
  (state: CardList) => state.cards.entities,
);
export const getCardsCount = createSelector(
  getCardList,
  (state: CardList) => state.cards.cardsCount,
);
export const isLoading = createSelector(
  getCardList,
  (state: CardList) => state.cards.loading,
);

export const cardListQuery = {
  getListConfig,
  getCards,
  getCardsCount,
  isLoading,
};
