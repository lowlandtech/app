import { createFeatureSelector, createSelector } from '@ngrx/store';
import { ASIDE_FEATURE_KEY, AsideState } from './aside.reducer';

// Lookup the 'Aside' feature state managed by NgRx
const getAsideState = createFeatureSelector<AsideState>(ASIDE_FEATURE_KEY);

const getToggled = createSelector(
  getAsideState,
  (state: AsideState) => state.showing
);
const getError = createSelector(
  getAsideState,
  (state: AsideState) => state.error
);

export const asideQuery = {
  getToggled,
  getError
};
