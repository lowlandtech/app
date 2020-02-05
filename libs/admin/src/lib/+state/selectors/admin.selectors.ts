import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AdminState, } from '../reducers';

export const getAdmin = createFeatureSelector<AdminState>('adminState');
export const getSidebar = createSelector(getAdmin, (admin: AdminState) => admin.sidebar);
export const getProfile = createSelector(getAdmin, (admin: AdminState) => admin.profile);
export const getAside = createSelector(getAdmin, (admin: AdminState) => admin.aside);
export const getLogo = createSelector(getAdmin, (admin: AdminState) => admin.logo);

export const adminQuery = {
  getAdmin,
  getSidebar,
  getProfile,
  getAside,
  getLogo
};
