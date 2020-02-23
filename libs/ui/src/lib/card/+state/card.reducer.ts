import { createReducer, on, Action } from '@ngrx/store';
import * as CardActions from './card.actions';
import { CardStateData } from './card.models';

export const CARD_FEATURE_KEY = 'cardStates';

export interface CardState {
  readonly cards: CardStateData[];
}

export const initialState: CardState = {
  cards: []
};

const cardReducer = createReducer(
  initialState,
  on(CardActions.expand,
     CardActions.hide,
     CardActions.collapse, (state, action) => {
    return replaceCard(state, action.payload);
  }),
);

function replaceCard(states: CardState, payload: CardStateData): CardState {
  const cardIndex = states.cards.findIndex(_card => _card.id === payload.id);
  const cards = [
    ...states.cards.slice(0, cardIndex),
    Object.assign({}, states[cardIndex], payload),
    ...states.cards.slice(cardIndex + 1),
  ];
  return { ...states, cards };
}

export function reducer(state: CardState | undefined, action: Action) {
  return cardReducer(state, action);
}
