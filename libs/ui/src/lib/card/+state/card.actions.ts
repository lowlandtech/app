import { createAction, props } from '@ngrx/store';

export const loadCards = createAction(
  '[Card] Load Cards'
);

export const loadCardsSuccess = createAction(
  '[Card] Load Cards Success',
  props<{ data: any }>()
);

export const loadCardsFailure = createAction(
  '[Card] Load Cards Failure',
  props<{ error: any }>()
);
