import { createFeatureSelector, createSelector } from '@ngrx/store';
import { ASIDE_FEATURE_KEY, AsideState } from './aside.reducer';

// Lookup the 'Aside' feature state managed by NgRx
const getAsideState = createFeatureSelector<AsideState>(ASIDE_FEATURE_KEY);

const getLoaded = createSelector(
  getAsideState,
  (state: AsideState) => state.loaded
);
const getError = createSelector(
  getAsideState,
  (state: AsideState) => state.error
);

const getAllAside = createSelector(
  getAsideState,
  getLoaded,
  (state: AsideState, isLoaded) => {
    return isLoaded ? state.list : [];
  }
);
const getSelectedId = createSelector(
  getAsideState,
  (state: AsideState) => state.selectedId
);
const getSelectedAside = createSelector(
  getAllAside,
  getSelectedId,
  (aside, id) => {
    const result = aside.find(it => it['id'] === id);
    return result ? Object.assign({}, result) : undefined;
  }
);

export const asideQuery = {
  getLoaded,
  getError,
  getAllAside,
  getSelectedAside
};
