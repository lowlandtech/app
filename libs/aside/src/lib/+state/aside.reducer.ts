import { AsideAction, AsideActionTypes } from './aside.actions';

export const ASIDE_FEATURE_KEY = 'aside';

/**
 * Interface for the 'Aside' data used in
 *  - AsideState, and the reducer function
 *
 *  Note: replace if already defined in another module
 */

/* tslint:disable:no-empty-interface */
// export interface Entity {}

export interface AsideState {
  showing: boolean; // has the Aside list been toggled
  error?: any; // last none error (if any)
}

export interface AsidePartialState {
  readonly [ASIDE_FEATURE_KEY]: AsideState;
}

export const initialState: AsideState = {
  showing: false
};

export function reducer(
  state: AsideState = initialState,
  action: AsideAction
): AsideState {
  switch (action.type) {
    case AsideActionTypes.ToggleAside: {
      state = {
        ...state,
        showing: !state.showing
      };
      break;
    }
  }
  return state;
}
