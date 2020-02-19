import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AdminState, } from '../reducers';
import { ADMINSTATE_FEATURE_KEY } from '../reducers';
import { AsideState } from '../reducers/admin.reducer';

export const getAdmin = createFeatureSelector<AdminState>(ADMINSTATE_FEATURE_KEY);
export const getSidebar = createSelector(getAdmin, (admin: AdminState) => admin.sidebar);
export const getProfile = createSelector(getAdmin, (admin: AdminState) => admin.profile);
export const getAside = createSelector(getAdmin, (admin: AdminState) => admin.aside);
export const getGroups = createSelector(getAside, (aside: AsideState)=> aside.groups)
export const getLogo = createSelector(getAdmin, (admin: AdminState) => admin.logo);

export const adminQuery = {
  getAdmin,
  getSidebar,
  getProfile,
  getAside,
  getGroups,
  getLogo
};
