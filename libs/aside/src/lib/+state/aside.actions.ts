import { Action } from '@ngrx/store';

export enum AsideActionTypes {
  ToggleAside = '[Aside] Toggle Aside',
  AsideToggled = '[Aside] Aside Toggled',
  AsideToggleError = '[Aside] Aside Toggle Error'
}

export class ToggleAside implements Action {
  readonly type = AsideActionTypes.ToggleAside;
}

export class AsideToggleError implements Action {
  readonly type = AsideActionTypes.AsideToggleError;
  constructor(public paytoggle: any) {}
}

export class AsideToggled implements Action {
  readonly type = AsideActionTypes.AsideToggled;
}

export type AsideAction = ToggleAside | AsideToggled | AsideToggleError;

export const fromAsideActions = {
  ToggleAside,
  AsideToggled,
  AsideToggleError
};
