import { createFeatureSelector, createSelector } from '@ngrx/store';
import { CARD_FEATURE_KEY, CardsState } from './card.reducer';

export const getCardState = createFeatureSelector<CardsState>(CARD_FEATURE_KEY);
export const getStates = createSelector( getCardState, (state) => state.states );
export const getSelectedCardId = createSelector( getCardState, (state) => state.selectedCardId );
export const getCard = createSelector( getStates, getSelectedCardId, (states, selectedCardId: string) => states[selectedCardId] );
export const getCardById = createSelector(getStates, (state, props) => state[props.cardId] );

export const cardStateQuery = {
  getStates,
  getCard,
  getCardById,
  getSelectedCardId
};

