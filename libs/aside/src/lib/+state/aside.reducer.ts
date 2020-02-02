import { AsideAction, AsideActionTypes } from './aside.actions';

export const ASIDE_FEATURE_KEY = 'aside';

/**
 * Interface for the 'Aside' data used in
 *  - AsideState, and the reducer function
 *
 *  Note: replace if already defined in another module
 */

/* tslint:disable:no-empty-interface */
export interface Entity {}

export interface AsideState {
  list: Entity[]; // list of Aside; analogous to a sql normalized table
  selectedId?: string | number; // which Aside record has been selected
  loaded: boolean; // has the Aside list been loaded
  error?: any; // last none error (if any)
}

export interface AsidePartialState {
  readonly [ASIDE_FEATURE_KEY]: AsideState;
}

export const initialState: AsideState = {
  list: [],
  loaded: false
};

export function reducer(
  state: AsideState = initialState,
  action: AsideAction
): AsideState {
  switch (action.type) {
    case AsideActionTypes.AsideLoaded: {
      state = {
        ...state,
        list: action.payload,
        loaded: true
      };
      break;
    }
  }
  return state;
}
