import { Action, createReducer, on } from '@ngrx/store';
import { UUID } from 'angular2-uuid';
import * as CardActions from './card.actions';
import { Card } from '../card.model';

export const cardFeatureKey = 'card';

export interface CardState {
  card: Card;
  state: CardStates;
}

export enum CardStates {
  NORMAL,
  EXPANDED,
  HIDDEN,
  MINIMIZED
}
export const initialState: CardState = {
  card: {
    id: UUID.UUID(),
    title: '(new card)',
    type: 'TASK',
    key: 'new-card'
  },
  state: CardStates.NORMAL
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
