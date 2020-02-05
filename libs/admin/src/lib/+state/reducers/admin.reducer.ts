import { AdminStateAction, AdminStateActionTypes, ProfileHidden } from '../actions/admin.actions';
import { ActionReducer, MetaReducer, Action } from '@ngrx/store';
import { localStorageSync } from 'ngrx-store-localstorage';

export const ADMINSTATE_FEATURE_KEY = 'adminState';

// tslint:disable-next-line: no-empty-interface
export interface Entity {}

export interface SidebarState {
  state: string;
  button: boolean;
}
export interface AsideState {
  hidden: boolean;
}
export interface ProfileState {
  hidden: boolean;
  button: boolean;
}
export interface AdminState {
  logo: boolean,
  sidebar: SidebarState,
  aside: AsideState,
  profile: ProfileState,
}

export interface AdminStatePartialState {
  readonly [ADMINSTATE_FEATURE_KEY]: AdminState;
}
const STORE_KEYS_TO_PERSIST = ['sidebar', 'aside', 'profile', 'logo'];

export const initialAdminState: AdminState = {
  logo: true,
  sidebar : {
    state: "MAXIMIZED",
    button: true
  },
  aside: {
    hidden: true
  },
  profile: {
    hidden: false,
    button: true
  }
};

export function adminStateReducer(state: AdminState = initialAdminState, action: AdminStateAction): AdminState {
  switch (action.type) {
    case AdminStateActionTypes.SidebarMaximized: {
      return {
        ...state,
        logo: true,
        sidebar: {
          state: action.payload,
          button: true
        },
        profile: {
          button: true,
          hidden: state.profile.hidden
        }
      };
    }
    case AdminStateActionTypes.SidebarMinimized: {
      return {
        ...state,
        logo: true,
        sidebar: {
          state: action.payload,
          button: false
        },
        profile: {
          button: false,
          hidden: state.profile.hidden
        }
      };
    }
    case AdminStateActionTypes.SidebarHidden: {
      return {
        ...state,
        sidebar: {
          state: action.payload,
          button: true
        },
        profile: {
          button: false,
          hidden: state.profile.hidden
        }
      };
    }
    case AdminStateActionTypes.ProfileHidden:
    case AdminStateActionTypes.ProfileShown: {
      return {
        ...state,
        profile: {
          button: state.profile.button,
          hidden: action.payload
        }
      };
    }
    case AdminStateActionTypes.AsideHidden:
    case AdminStateActionTypes.AsideShown:{
      return {
        ...state,
        aside: {
          hidden: action.payload
        }
      };
    }
    default: {
      return state;
    }
  }
}

export function localStorageSyncReducer(reducer: ActionReducer<AdminState>): ActionReducer<AdminState> {
  return localStorageSync({
    keys: STORE_KEYS_TO_PERSIST,
    rehydrate: true,
  })(reducer);
}

export const metaReducers: Array<MetaReducer<AdminState, Action>> = [localStorageSyncReducer];
