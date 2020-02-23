import { createAction, props } from '@ngrx/store';
import { CardStateData } from './card.models';

export const expand = createAction('[Card] EXPAND_CARD',props<{ payload: CardStateData }>());
export const collapse = createAction('[Card] COLLAPSE_CARD',props<{ payload: CardStateData }>());
export const hide = createAction('[Card] HIDE_CARD',props<{ payload: CardStateData }>());
