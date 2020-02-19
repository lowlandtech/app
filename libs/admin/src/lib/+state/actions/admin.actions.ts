import { Action } from '@ngrx/store';
import { Entity } from '../reducers/admin.reducer';
import { AsideListItem } from '../../aside';

export enum AdminStateActionTypes {
  LoadAdminState = '[admin] LOAD_ADMINSTATE',
  AdminStateLoaded = '[admin] LOAD_ADMINSTATE_SUCCESS',
  AdminStateLoadError = '[admin] LOAD_ADMINSTATE_ERROR',
  SidebarMaximized =  '[admin] MAXIMIZE_SIDEBAR_SUCCESS',
  SidebarMinimized =  '[admin] MINIMIZE_SIDEBAR_SUCCESS',
  SidebarHidden =  '[admin] HIDE_SIDEBAR_SUCCESS',
  ProfileHidden =  '[admin] HIDE_PROFILE_SUCCESS',
  ProfileShown =  '[admin] SHOW_PROFILE_SUCCESS',
  AsideShown =  '[admin] SHOW_ASIDE_SUCCESS',
  AsideHidden =  '[admin] HIDE_ASIDE_SUCCESS',
  AsideListItemAdd =  '[admin] ADD_ASIDE_LIST_ITEM',
  AsideListItemRemove =  '[admin] REMOVE_ASIDE_LIST_ITEM',
}

export class LoadAdminState implements Action {
  readonly type = AdminStateActionTypes.LoadAdminState;
}

export class AdminStateLoadError implements Action {
  readonly type = AdminStateActionTypes.AdminStateLoadError;
  constructor(public payload: any) {}
}

export class AdminStateLoaded implements Action {
  readonly type = AdminStateActionTypes.AdminStateLoaded;
  constructor(public payload: Entity[]) {}
}

export class SidebarMaximized implements Action {
  readonly type = AdminStateActionTypes.SidebarMaximized;
  constructor(public payload: string = 'MAXIMIZED') {}
}

export class SidebarMinimized implements Action {
  readonly type = AdminStateActionTypes.SidebarMinimized;
  constructor(public payload: string = 'MINIMIZED') {}
}

export class SidebarHidden implements Action {
  readonly type = AdminStateActionTypes.SidebarHidden;
  constructor(public payload: string = 'HIDDEN') {}
}

export class ProfileHidden implements Action {
  readonly type = AdminStateActionTypes.ProfileHidden;
  constructor(public payload: boolean = true) {}
}

export class ProfileShown implements Action {
  readonly type = AdminStateActionTypes.ProfileShown;
  constructor(public payload: boolean = false) {}
}

export class AsideShown implements Action {
  readonly type = AdminStateActionTypes.AsideShown;
  constructor(public payload: boolean = false) {}
}

export class AsideHidden implements Action {
  readonly type = AdminStateActionTypes.AsideHidden;
  constructor(public payload: boolean = true) {}
}

export class AsideListItemAdd implements Action {
  readonly type = AdminStateActionTypes.AsideListItemAdd;
  constructor(public payload: AsideListItem) {}
}

export class AsideListItemRemove implements Action {
  readonly type = AdminStateActionTypes.AsideListItemRemove;
  constructor(public payload: number ) {}
}

export type AdminStateAction = LoadAdminState |
                             AdminStateLoaded |
                             AdminStateLoadError |
                             AsideHidden |
                             AsideShown |
                             ProfileHidden |
                             ProfileShown |
                             SidebarHidden |
                             SidebarMaximized |
                             SidebarMinimized |
                             AsideListItemAdd |
                             AsideListItemRemove;

export const fromAdminStateActions = {
  LoadAdminState: LoadAdminState,
  AdminStateLoaded,
  AdminStateLoadError: AdminStateLoadError,
  SidebarMaximized,
  SidebarMinimized,
  SidebarHidden,
  ProfileHidden,
  ProfileShown,
  AsideHidden,
  AsideShown,
  AsideListItemAdd,
  AsideListItemRemove
};
