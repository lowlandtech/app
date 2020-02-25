import { createAction, props } from '@ngrx/store';

export const normalize = createAction('[Card] NORMALIZE_CARD', props<{ payload: string }>());
export const expand = createAction('[Card] EXPAND_CARD', props<{ payload: string }>());
export const collapse = createAction('[Card] COLLAPSE_CARD', props<{ payload: string }>());
export const hide = createAction('[Card] HIDE_CARD', props<{ payload: string }>());
export const selectCard = createAction('[Card] SELECT_CARD', props<{ payload: string }>());
