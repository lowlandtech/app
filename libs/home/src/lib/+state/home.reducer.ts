import { Action, createReducer, on } from '@ngrx/store';
import * as HomeActions from './home.actions';

export const HOMESTATE_FEATURE_KEY = 'home';

export interface HomeState {
  readonly [HOMESTATE_FEATURE_KEY]: Home;
}

export interface Home {
  tags: string[];
  selectedTag?: string;
}

export const homeInitialState: Home = {
  tags: [],
  selectedTag: ""
};

const reducer = createReducer(
  homeInitialState,
  on(HomeActions.loadTagsSuccess, (state, action) => ({ ...state, tags: action.tags })),
  on(HomeActions.loadTagsFail, (state, action) => ({ ...state, tags: [] })),
  on(HomeActions.selectTag, (state, action) => ({...state, selectedTag: action.tag}))
);

export function homeReducer(state: Home | undefined, action: Action): Home {
  return reducer(state, action);
}
