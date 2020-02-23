import { createFeatureSelector, createSelector } from '@ngrx/store';
import { CARD_FEATURE_KEY, CardState } from './card.reducer';

export const getCardState = createFeatureSelector<CardState>(CARD_FEATURE_KEY);
export const getCards = createSelector( getCardState, (state) => state.states );

export const vcardStateQuery = {
  getCards
};

