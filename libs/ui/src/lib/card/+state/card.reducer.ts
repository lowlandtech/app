import { Action, createReducer, on } from '@ngrx/store';
import * as CardActions from './card.actions';
import { Card } from '../card.model';

export const cardFeatureKey = 'card';

export interface CardState {
  card: Card;
  
}

export const initialState: CardState = {

};

const cardReducer = createReducer(
  initialState,

  on(CardActions.loadCards, state => state),
  on(CardActions.loadCardsSuccess, (state, action) => state),
  on(CardActions.loadCardsFailure, (state, action) => state),

);

export function reducer(state: CardState | undefined, action: Action) {
  return cardReducer(state, action);
}
