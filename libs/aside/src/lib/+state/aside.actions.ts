import { Action } from '@ngrx/store';
import { Entity } from './aside.reducer';

export enum AsideActionTypes {
  LoadAside = '[Aside] Load Aside',
  AsideLoaded = '[Aside] Aside Loaded',
  AsideLoadError = '[Aside] Aside Load Error'
}

export class LoadAside implements Action {
  readonly type = AsideActionTypes.LoadAside;
}

export class AsideLoadError implements Action {
  readonly type = AsideActionTypes.AsideLoadError;
  constructor(public payload: any) {}
}

export class AsideLoaded implements Action {
  readonly type = AsideActionTypes.AsideLoaded;
  constructor(public payload: Entity[]) {}
}

export type AsideAction = LoadAside | AsideLoaded | AsideLoadError;

export const fromAsideActions = {
  LoadAside,
  AsideLoaded,
  AsideLoadError
};
