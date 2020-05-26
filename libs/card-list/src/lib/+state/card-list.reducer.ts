import { CardData } from '@lowlandtech/api';
import { Action, createReducer, on, ActionReducer, MetaReducer } from '@ngrx/store';
import * as CardListActions from './card-list.actions';
import { localStorageSync } from 'ngrx-store-localstorage';

export const   CARDLIST_STATE_FEATURE_KEY = 'cardList';
const STORE_KEYS_TO_PERSIST = ['listConfig', 'cards'];

export interface CardList {
  listConfig: CardListConfig;
  cards: Cards;
}

export interface CardListState {
  readonly [CARDLIST_STATE_FEATURE_KEY]: CardList;
}

export interface CardListConfig {
  type: ListType;
  currentPage: number;
  filters: Filters;
}

export interface Filters {
  tag?: string;
  author?: string;
  favorited?: string;
  limit?: number;
  offset?: number;
}

export type ListType = 'ALL' | 'FEED';

export interface Cards {
  entities: CardData[];
  cardsCount: number;
  loaded: boolean;
  loading: boolean;
}

export const cardListInitialState: CardList = {
  listConfig: {
    type: 'ALL',
    currentPage: 1,
    filters: {
      limit: 10,
    },
  },
  cards: {
    entities: [],
    cardsCount: 0,
    loaded: false,
    loading: false,
  },
};

const reducer = createReducer(
  cardListInitialState,
  on(CardListActions.setListPage, (state, action) => {
    const filters = {
      ...state.listConfig.filters,
      offset: state.listConfig.filters.limit * (action.page - 1),
    };
    const listConfig = {
      ...state.listConfig,
      currentPage: action.page,
      filters,
    };
    return { ...state, listConfig };
  }),
  on(CardListActions.setListConfig, (state, action) => ({
    ...state,
    listConfig: action.config,
  })),
  on(CardListActions.loadCards, (state, _) => {
    const cards = { ...state.cards, loading: true };
    return { ...state, cards };
  }),
  on(CardListActions.loadCardsSuccess, (state, action) => {
    const cards = {
      ...state.cards,
      entities: action.cards,
      cardsCount: action.cardsCount,
      loading: false,
      loaded: true,
    };
    return { ...state, cards };
  }),
  on(CardListActions.loadCardsFail, (state, _) => {
    const cards = {
      ...state.cards,
      entities: [],
      cardsCount: 0,
      loading: false,
      loaded: true,
    };
    return { ...state, cards };
  }),
  on(CardListActions.unFavoriteSuccess, CardListActions.favoriteSuccess, (state, action) => ({
    ...state,
    cards: replaceCard(state.cards, action.card),
  })),
);

function replaceCard(cards: Cards, payload: CardData): Cards {
  const cardIndex = cards.entities.findIndex(a => a.slug === payload.slug);
  const entities = [
    ...cards.entities.slice(0, cardIndex),
    Object.assign({}, cards.entities[cardIndex], payload),
    ...cards.entities.slice(cardIndex + 1),
  ];
  return { ...cards, entities, loading: false, loaded: true };
}

export function cardListReducer(state: CardList | undefined, action: Action): CardList {
  return reducer(state, action);
}

export function localStorageSyncReducer(_reducer: ActionReducer<CardList>): ActionReducer<CardList> {
  return localStorageSync({
    keys: STORE_KEYS_TO_PERSIST,
    rehydrate: true
  })(_reducer);
}

export const metaReducers: Array<MetaReducer<CardList, Action>> = [
  localStorageSyncReducer
];
