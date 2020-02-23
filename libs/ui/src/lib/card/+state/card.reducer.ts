import { createReducer, on, Action, MetaReducer, ActionReducer } from '@ngrx/store';
import * as CardActions from './card.actions';
import { CardStateData } from './card.models';
import { localStorageSync } from 'ngrx-store-localstorage';

export const CARD_FEATURE_KEY = 'cardStates';
export const STORE_KEYS_TO_PERSIST = ['states']
export interface CardState {
  readonly states: CardStateData[];
}

export const initialState: CardState = {
  states: []
};

const cardReducer = createReducer(
  initialState,
  on(CardActions.normalize,
     CardActions.expand,
     CardActions.hide,
     CardActions.collapse, (state, action) => {
    return replaceCard(state, action.payload);
  }),
);

function replaceCard(states: CardState, payload: CardStateData): CardState {
  const cardIndex = states.states.findIndex(_card => _card.id === payload.id);
  const cards = [
    ...states.states.slice(0, cardIndex),
    Object.assign({}, states[cardIndex], payload),
    ...states.states.slice(cardIndex + 1),
  ];
  return { ...states, states: cards };
}

export function reducer(state: CardState | undefined, action: Action) {
  return cardReducer(state, action);
}

export function localStorageSyncReducer(_reducer: ActionReducer<CardState>): ActionReducer<CardState> {
  return localStorageSync({
    keys: STORE_KEYS_TO_PERSIST,
    rehydrate: true
  })(_reducer);
}

export const metaReducers: Array<MetaReducer<CardState, Action>> = [
  localStorageSyncReducer
];
