import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Home, HOMESTATE_FEATURE_KEY } from './home.reducer';

export const getHome = createFeatureSelector<Home>(HOMESTATE_FEATURE_KEY);
export const getTags = createSelector(getHome,(state: Home) => state.tags);
export const getSelectedTag = createSelector(getHome, (state: Home) => state.selectedTag);
export const homeQuery = {
  getTags,
  getHome,
  getSelectedTag,
};
