import { createReducer, on, Action, MetaReducer, ActionReducer, StateObservable } from '@ngrx/store';
import * as CardActions from './card.actions';
import { localStorageSync } from 'ngrx-store-localstorage';
import { CardStateData } from './card.models';

export const CARD_FEATURE_KEY = 'cardStates';
export const STORE_KEYS_TO_PERSIST = ['states','selectedCardId']

export interface States {
  [id: string]: CardStateData;
}
export interface CardsState {
  readonly selectedCardId: string;
  readonly states: States;
}

export const initialState: CardsState = {
  selectedCardId: '',
  states: {}
};

const cardReducer = createReducer(
  initialState,
  on(CardActions.selectCard,(state, action) => {
    const cards = {...state };
    if(!cards.states.hasOwnProperty(action.payload)) {
      cards.states[action.payload] = {
        id: action.payload,
        normal: true,
        hidden: false,
        minimized: false,
        expanded: false,
      }
    }
    return {
      ...state,
      states: cards.states,
      selectedCardId: action.payload
    }
  }),
  on(CardActions.normalize,(state, action) => {
    const cards = {
      ...state
    }
    cards.states[action.payload] = {
      id: action.payload,
      normal: true,
      hidden: false,
      minimized: false,
      expanded: false,
    }
    return {
      ...state,
      states: cards.states,
      selectedCardId: action.payload
    }
  }),
  on(CardActions.expand,(state, action) => {
    const cards = {
      ...state
    }
    cards.states[action.payload] = {
      id: action.payload,
      normal: state.states[action.payload].expanded,
      hidden: false,
      minimized: false,
      expanded: !state.states[action.payload].expanded,
    }
    return {
      ...state,
      states: cards.states,
      selectedCardId: action.payload
    }
  }),
  on(CardActions.hide,(state, action) => {
    const cards = {
      ...state
    }
    cards.states[action.payload] = {
      id: action.payload,
      normal: state.states[action.payload].hidden,
      hidden: !state.states[action.payload].hidden,
      minimized: false,
      expanded: false,
    }
    return {
      ...state,
      states: cards.states,
      selectedCardId: action.payload
    }
  }),
  on(CardActions.collapse, (state, action) => {
    const cards = {
      ...state
    }
    cards.states[action.payload] = {
      id: action.payload,
      normal: state.states[action.payload].minimized,
      hidden: false,
      minimized: !state.states[action.payload].minimized,
      expanded: false,
    }
    return {
      ...state,
      states: cards.states,
      selectedCardId: action.payload
    }
  }),
);

export function reducer(state: CardsState | undefined, action: Action) {
  return cardReducer(state, action);
}

export function localStorageSyncReducer(_reducer: ActionReducer<CardsState>): ActionReducer<CardsState> {
  return localStorageSync({
    keys: STORE_KEYS_TO_PERSIST,
    rehydrate: true
  })(_reducer);
}

export const metaReducers: Array<MetaReducer<CardsState, Action>> = [
  localStorageSyncReducer
];
